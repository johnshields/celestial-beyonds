FROM python:3.12-slim

WORKDIR /app

COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY . .

# Train model at build time so cold-start serves instantly
RUN python -c "from model.trainer import train_if_needed; train_if_needed()"

EXPOSE 5000
CMD ["gunicorn", "-b", "0.0.0.0:5000", "-w", "1", "--timeout", "60", "wsgi:app"]
