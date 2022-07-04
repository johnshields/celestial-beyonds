from flask import Flask, render_template, request
from chatterbot import ChatBot
from chatterbot.trainers import ListTrainer, ChatterBotCorpusTrainer
import os

# Moonbeam Chatbot. python version -- 3.7.13
# https://chatterbot.readthedocs.io/en/stable/


# Set up Moonbeam as a Chatbot.
mb_bot = ChatBot("Moonbeam")

# Train the bot.
print('[INFO] Training bot...')
trainer = ChatterBotCorpusTrainer(mb_bot)
trainer.train("chatterbot.corpus.english")
print('[INFO] Training complete!')

# create a new web app
app = Flask(__name__, static_folder="static", template_folder="templates")
app.config["DEBUG"] = True


# add root route
@app.route("/")
def index():
    return render_template('index.html')


@app.route("/api/chat")
def home():
    # have the home.html on home page
    return render_template('home.html')


@app.route('/api/chat', methods=['POST'])
def chat():
    user_input = request.form['value']
    response = mb_bot.get_response(user_input)
    print("User: " f'{str(user_input)}')
    print("Moonbeam: " f'{str(response)}')
    return str(response)


if __name__ == "__main__":
    app.run(host='0.0.0.0')
