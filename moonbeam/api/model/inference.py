"""
Inference logic for generating chatbot replies.
"""
from sklearn.metrics.pairwise import cosine_similarity
from config import MODEL_CACHE, DEFAULT_THRESHOLD


def get_reply(text: str, threshold: float = DEFAULT_THRESHOLD) -> str:
    """Return best-matching reply or fallback message."""
    vec = MODEL_CACHE["vectorizer"]
    x = MODEL_CACHE["matrix"]
    res = MODEL_CACHE["responses"]
    
    sims = cosine_similarity(vec.transform([text]), x)[0]
    best_i = int(sims.argmax())
    best_score = float(sims[best_i])

    if best_score < threshold:
        return "Sorry, I'm not sure about that yet."

    return res[best_i]
