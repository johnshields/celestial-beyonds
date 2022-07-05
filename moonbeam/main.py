from flask import Flask, render_template, request
from chatterbot import ChatBot
from chatterbot.trainers import ListTrainer, ChatterBotCorpusTrainer
import os
import json

# Moonbeam Chatbot. python version -- 3.7.13
# https://chatterbot.readthedocs.io/en/stable/


# Set up Moonbeam as a Chatbot.
mb_bot = ChatBot(
    'Moonbeam', storage_adapter='chatterbot.storage.SQLStorageAdapter',
    logic_adapters=['chatterbot.logic.BestMatch']
)
# Train the bot.
print('[INFO] Training bot...')
trainer = ListTrainer(mb_bot)

with open('data/mb_data.json') as f:
    data = json.load(f)

trainer.train(data)
print('[INFO] Training complete!' + '\n Chat with Moonbeam!' +
      '\n Local: http://0.0.0.0:5000/' + '\n Hosted: http://moonbeambot.live/' +
      '\n Awaiting request...')

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
