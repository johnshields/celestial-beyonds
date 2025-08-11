import os, re, json, joblib
from typing import List, Tuple
from flask import Flask, render_template, request, redirect
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity

# CORS: Allowed origins
# Exact matches (str) and wildcard domains (regex)
ALLOWED_ORIGINS = [
    "http://localhost:8000",
    "http://127.0.0.1:8000",
    re.compile(r"^https://([a-z0-9-]+\.)*gamejolt\.com$"),
    re.compile(r"^https://([a-z0-9-]+\.)*gamejolt\.net$"),
]


def origin_allowed(origin: str | None) -> bool:
    """Return True if request origin is allowlisted."""
    if not origin:
        return False

    for pat in ALLOWED_ORIGINS:
        if isinstance(pat, str) and origin == pat:
            return True
        if hasattr(pat, "match") and pat.match(origin):
            return True

    return False


# Paths / constants
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
DATA_DIR = os.path.join(BASE_DIR, "data")  # Training data folder
MODEL_DIR = os.path.join(BASE_DIR, "model_cache")  # Saved TF-IDF model/cache
os.makedirs(MODEL_DIR, exist_ok=True)

DATA_FILES = [
    os.path.join(DATA_DIR, "trappist.json"),
    os.path.join(DATA_DIR, "pcb.json"),
    os.path.join(DATA_DIR, "kepler.json"),
    os.path.join(DATA_DIR, "general.json"),
]

# Paths for persisted model/data
VEC_PATH = os.path.join(MODEL_DIR, "tfidf_vectorizer.joblib")
MAT_PATH = os.path.join(MODEL_DIR, "tfidf_matrix.joblib")
RESP_PATH = os.path.join(MODEL_DIR, "responses.json")

DEFAULT_THRESHOLD: float = 0.15  # similarity score cutoff

# In-memory cache to avoid disk reload per request
MODEL_CACHE = {"vectorizer": None, "matrix": None, "responses": None}


# Data loading / training
def load_pairs(paths: List[str]) -> List[Tuple[str, str]]:
    """
    Load Q/A pairs from JSON.
    """
    pairs = []
    for path in paths:
        if not os.path.isfile(path):
            print(f"[WARN] Missing data file: {path}")
            continue
        with open(path, "r", encoding="utf-8") as f:
            data = json.load(f)
        if not isinstance(data, list):
            continue

        # Object format
        if data and isinstance(data[0], dict) and "q" in data[0] and "a" in data[0]:
            for it in data:
                q = str(it.get("q", "")).strip()
                a = str(it.get("a", "")).strip()
                if q and a:
                    pairs.append((q, a))
        else:
            # Alternating lines format
            for i in range(len(data) - 1):
                q = str(data[i]).strip()
                a = str(data[i + 1]).strip()
                if q and a:
                    pairs.append((q, a))

    return pairs


def train_if_needed() -> None:
    """Train TF-IDF model once; skip if cached on disk."""
    if all(os.path.exists(p) for p in (VEC_PATH, MAT_PATH, RESP_PATH)):
        return

    print("[INFO] Training Moonbeam (TF-IDF retrieval)…")
    pairs = load_pairs(DATA_FILES)

    if not pairs:
        raise RuntimeError("[ERROR] No training data found in data/*.json")

    questions = [q for q, _ in pairs]
    responses = [a for _, a in pairs]
    vectorizer = TfidfVectorizer(
        lowercase=True,
        ngram_range=(1, 2),
        max_df=0.9,
        strip_accents="unicode"
    )
    x = vectorizer.fit_transform(questions)
    joblib.dump(vectorizer, VEC_PATH)
    joblib.dump(x, MAT_PATH)

    with open(RESP_PATH, "w", encoding="utf-8") as f:
        json.dump(responses, f, ensure_ascii=False)

    print(f"[INFO] Training complete. {len(pairs)} pairs saved.")


def load_model_into_memory() -> None:
    """Load trained artifacts into RAM for fast lookups."""
    if MODEL_CACHE["vectorizer"] is not None:
        return

    MODEL_CACHE["vectorizer"] = joblib.load(VEC_PATH)
    MODEL_CACHE["matrix"] = joblib.load(MAT_PATH)
    with open(RESP_PATH, "r", encoding="utf-8") as f:
        MODEL_CACHE["responses"] = json.load(f)

    print("[INFO] Model loaded into memory.")


def get_reply(text: str, threshold: float = DEFAULT_THRESHOLD) -> str:
    """Return best-matching reply or fallback message."""
    vec = MODEL_CACHE["vectorizer"]
    x = MODEL_CACHE["matrix"]
    res = MODEL_CACHE["responses"]
    sims = cosine_similarity(vec.transform([text]), x)[0]
    best_i = int(sims.argmax())
    best_score = float(sims[best_i])

    if best_score < threshold:
        return "Sorry, I’m not sure about that yet."

    return res[best_i]


# Flask app / routes
app = Flask(__name__, static_folder="static", template_folder="templates")
app.config["DEBUG"] = True


@app.before_request
def _warm_up() -> None:
    """Ensure model is trained and loaded before serving."""
    if MODEL_CACHE["vectorizer"] is None:
        train_if_needed()
        load_model_into_memory()


@app.after_request
def add_cors_headers(resp):
    """Attach CORS headers to API responses for allowed origins."""
    if request.path.startswith("/api/"):
        origin = request.headers.get("Origin")

        if origin_allowed(origin):
            resp.headers["Access-Control-Allow-Origin"] = origin
            resp.headers["Vary"] = "Origin"
            resp.headers["Access-Control-Allow-Methods"] = "GET,POST,OPTIONS"
            resp.headers["Access-Control-Allow-Headers"] = "Content-Type"

    return resp


@app.route("/")
def index():
    """Landing page with info or UI."""
    return render_template("index.html")


@app.route("/play")
def play():
    return redirect("https://gamejolt.com/games/celestial-beyonds/740687", code=302)


@app.route("/api/chat")
def home():
    """Basic chat HTML page."""
    return render_template("home.html")


@app.route("/api/chat", methods=["POST", "OPTIONS"])
def chat():
    """POST: get chatbot reply; OPTIONS: handle CORS preflight."""
    if request.method == "OPTIONS":
        return "", 204

    user_input = (request.form.get("value") or "").strip()
    if not user_input:
        return "Please say something."

    reply = get_reply(user_input)
    print(f"User: {user_input}")
    print(f"Moonbeam: {reply}")
    return reply


def create_app():
    return app


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
