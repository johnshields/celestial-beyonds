"""
Model training logic for TF-IDF retrieval system.
"""
import os
import json
import joblib
from sklearn.feature_extraction.text import TfidfVectorizer
from config import VEC_PATH, MAT_PATH, RESP_PATH, DATA_FILES, MODEL_CACHE
from utils.data_loader import load_pairs


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
