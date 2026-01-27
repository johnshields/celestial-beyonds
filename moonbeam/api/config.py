"""
Configuration and constants for Moonbeam application.
"""
import os

# Paths / constants
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
DATA_DIR = os.path.join(BASE_DIR, "data")
MODEL_DIR = os.path.join(BASE_DIR, "model_cache")

# Ensure model cache directory exists
os.makedirs(MODEL_DIR, exist_ok=True)

# Training data files
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

# Model parameters
DEFAULT_THRESHOLD: float = 0.15

# In-memory cache to avoid disk reload per request
MODEL_CACHE = {"vectorizer": None, "matrix": None, "responses": None}
