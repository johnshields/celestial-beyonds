from flask import Flask, render_template, request
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
import os, json, joblib
from typing import List, Tuple, Optional

# Paths & constants
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
DATA_DIR = os.path.join(BASE_DIR, "data")
MODEL_DIR = os.path.join(BASE_DIR, "model_cache")
os.makedirs(MODEL_DIR, exist_ok=True)

DATA_FILES = [
    os.path.join(DATA_DIR, "trappist.json"),
    os.path.join(DATA_DIR, "pcb.json"),
    os.path.join(DATA_DIR, "kepler.json"),
    os.path.join(DATA_DIR, "general.json"),
]

VEC_PATH = os.path.join(MODEL_DIR, "tfidf_vectorizer.joblib")
MAT_PATH = os.path.join(MODEL_DIR, "tfidf_matrix.joblib")
RESP_PATH = os.path.join(MODEL_DIR, "responses.json")

# Similarity threshold (raise/lower to be stricter/looser)
DEFAULT_THRESHOLD: float = 0.15

# In-process cache so not reloading for each request
MODEL_CACHE = {
    "vectorizer": None,  # type: Optional[TfidfVectorizer]
    "matrix": None,  # type: Optional[any]
    "responses": None  # type: Optional[List[str]]
}


# Data loading & training
def load_pairs(paths: List[str]) -> List[Tuple[str, str]]:
    """
    Load training pairs from JSON files.
    """
    pairs: List[Tuple[str, str]] = []
    for path in paths:
        if not os.path.isfile(path):
            print(f"[WARN] Missing data file: {path}")
            continue

        with open(path, "r", encoding="utf-8") as f:
            data = json.load(f)

        if not isinstance(data, list):
            continue

        if data and isinstance(data[0], dict) and "q" in data[0] and "a" in data[0]:
            # Q/A objects
            for it in data:
                q = str(it.get("q", "")).strip()
                a = str(it.get("a", "")).strip()
                if q and a:
                    pairs.append((q, a))
        else:
            # Alternating lines
            for i in range(len(data) - 1):
                q = str(data[i]).strip()
                a = str(data[i + 1]).strip()
                if q and a:
                    pairs.append((q, a))
    return pairs


def train_if_needed() -> None:
    """
    Train the TF-IDF model once and persist to disk.
    Skips if the saved artifacts already exist.
    """
    if all(os.path.exists(p) for p in (VEC_PATH, MAT_PATH, RESP_PATH)):
        print("[INFO] Using cached model on disk. Skipping training.")
        return

    print("[INFO] Training Moonbeam (TF-IDF retrieval)…")
    pairs = load_pairs(DATA_FILES)
    if not pairs:
        raise RuntimeError("[ERROR] No training pairs loaded from data/*.json")

    questions = [q for q, _ in pairs]
    responses = [a for _, a in pairs]

    vectorizer = TfidfVectorizer(
        lowercase=True,
        ngram_range=(1, 2),
        max_df=0.9,
        strip_accents="unicode",
    )
    x = vectorizer.fit_transform(questions)

    joblib.dump(vectorizer, VEC_PATH)
    joblib.dump(x, MAT_PATH)
    with open(RESP_PATH, "w", encoding="utf-8") as f:
        json.dump(responses, f, ensure_ascii=False)

    print(f"[INFO] Training complete. Pairs: {len(pairs)}")


def load_model_into_memory() -> None:
    """
    Load the trained artifacts from disk into the in-process MODEL_CACHE.
    """
    if MODEL_CACHE["vectorizer"] is not None:
        return  # already loaded

    MODEL_CACHE["vectorizer"] = joblib.load(VEC_PATH)
    MODEL_CACHE["matrix"] = joblib.load(MAT_PATH)
    with open(RESP_PATH, "r", encoding="utf-8") as f:
        MODEL_CACHE["responses"] = json.load(f)
    print("[INFO] Model loaded into memory.")


def get_reply(text: str, threshold: float = DEFAULT_THRESHOLD) -> str:
    """
    Return the best-matching response given user text.
    Falls back to a default message if similarity is below threshold.
    """
    vec = MODEL_CACHE["vectorizer"]
    x = MODEL_CACHE["matrix"]
    res = MODEL_CACHE["responses"]

    sims = cosine_similarity(vec.transform([text]), x)[0]
    best_i = int(sims.argmax())
    best_score = float(sims[best_i])

    if best_score < threshold:
        return "Sorry, I’m not sure about that yet."
    return res[best_i]


# Flask app & routes
app = Flask(__name__, static_folder="static", template_folder="templates")
app.config["DEBUG"] = True


@app.before_request
def _warm_up() -> None:
    train_if_needed()
    load_model_into_memory()


@app.route("/")
def index():
    """Landing page."""
    return render_template("index.html")


@app.route("/api/chat")
def home():
    """Chat UI page."""
    return render_template("home.html")


@app.route("/api/chat", methods=["POST"])
def chat():
    """
    Chat API endpoint.
    Expects form field 'value'. Returns plain text reply.
    """
    user_input = (request.form.get("value") or "").strip()
    if not user_input:
        return "Please say something."
    reply = get_reply(user_input)
    print(f"User: {user_input}")
    print(f"Moonbeam: {reply}")
    return reply


def create_app():
    return app


# Dev entrypoint
if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
