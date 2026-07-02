"""
CORS validation logic for Moonbeam API.
"""
import re

# Allowed origins - exact matches (str) and wildcard domains (regex)
ALLOWED_ORIGINS = [
    "http://localhost:8000",
    "http://127.0.0.1:8000",
    re.compile(r"^https://([a-z0-9-]+\.)*gamejolt\.com$"),
    re.compile(r"^https://([a-z0-9-]+\.)*gamejolt\.net$"),
    re.compile(r"^https://([a-z0-9-]+\.)*workers\.dev$"),
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
