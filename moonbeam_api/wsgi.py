from main import app

if __name__ == "__main__":
    # For local debug runs
    app.run(host="0.0.0.0", port=5000, debug=True)