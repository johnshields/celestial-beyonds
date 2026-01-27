"""
Moonbeam Flask application - main entry point.
"""
from flask import Flask, render_template, request, redirect
from cors import origin_allowed
from config import MODEL_CACHE
from model.trainer import train_if_needed, load_model_into_memory
from model.inference import get_reply

# Flask app setup
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
    """Redirect to The Celestial Beyonds game."""
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
    """Application factory pattern."""
    return app


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
