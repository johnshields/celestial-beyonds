"""
Data loading utilities for Q&A pairs.
"""
import os
import json
from typing import List, Tuple


def load_pairs(paths: List[str]) -> List[Tuple[str, str]]:
    """
    Load Q/A pairs from JSON files.
    
    Supports two formats:
    1. Object format: [{"q": "question", "a": "answer"}, ...]
    2. Alternating lines format: ["question", "answer", "question", "answer", ...]
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
