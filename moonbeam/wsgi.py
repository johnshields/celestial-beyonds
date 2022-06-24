from flask import Flask, render_template, request
from chatterbot import ChatBot
from chatterbot.trainers import ListTrainer
import os

# Moonbeam Chatbot. python version -- 3.7.13
# https://chatterbot.readthedocs.io/en/stable/


# Set up Moonbeam as a Chatbot.
mb_bot = ChatBot("Moonbeam")  # local
# mb_bot = ChatBot(
#     'Moonbeam',
#     storage_adapter='chatterbot.storage.MongoDatabaseAdapter',
#     database_uri='mongodb+srv://hume:okaycool@moonbeam-db.ef6jbwo.mongodb.net/moonbeam-db')  # mongodb

# Train the bot.
trainer = ListTrainer(mb_bot)
# Responses for an introduction...
trainer.train(
    [
        "what is your name",  # question
        "My name is Moonbeam"  # response
    ]
)
print('[INFO] Training complete.')

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


# have the app run a localhost on the port 8000
port = int(os.environ.get("PORT", 8000))
if __name__ == "__main__":
    app.run(host='0.0.0.0', port=port, debug=True)
