from flask import Flask, render_template, request
from chatterbot import ChatBot
from chatterbot.trainers import ChatterBotCorpusTrainer
import os
from dotenv import load_dotenv

# Moonbeam Chat bot.
# https://chatterbot.readthedocs.io/en/stable/

load_dotenv()
mongo_uri = os.getenv('URI')

# Set up Moonbeam as a chat bot.
mb_bot = ChatBot("Moonbeam", storage_adapter="chatterbot.storage.SQLStorageAdapter")  # local
# mb_bot = ChatBot(
#     'Moonbeam',
#     storage_adapter='chatterbot.storage.MongoDatabaseAdapter',
#     database='moonbeam-db',
#     database_uri=mongo_uri)  # mongodb

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
    print("Captain: " f'{str(user_input)}')
    print("Moonbeam: " f'{str(response)}')
    return str(response)


# have the app run a localhost on the port 5000
if __name__ == "__main__":
    print("Chat with Moonbeam! - " + "@ http://127.0.0.1:5000/")
    app.run(host='0.0.0.0', port=5000)
