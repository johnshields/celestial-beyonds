from flask import Flask, render_template, request
from chatterbot import ChatBot
from chatterbot.trainers import ChatterBotCorpusTrainer

# Set up Moonbeam as a chat bot.
moonbeam_bot = ChatBot('Moonbeam', storage_adapter='chatterbot.storage.SQLStorageAdapter',
                       logic_adapters=[
                           'chatterbot.logic.BestMatch',
                           'chatterbot.logic.MathematicalEvaluation',
                           'chatterbot.logic.TimeLogicAdapter'
                       ],
                       database_uri='sqlite:///database.db')

# Train the bot.
print('[INFO] Training bot...')
trainer = ChatterBotCorpusTrainer(moonbeam_bot)
# Train based on the english corpus
trainer.train("chatterbot.corpus.english")
# Train based on english greetings corpus
trainer.train("chatterbot.corpus.english.greetings")
# Train based on the english conversations corpus
trainer.train("chatterbot.corpus.english.conversations")
print('[INFO] Training complete!')

# create a new web app
app = Flask(__name__)
app = Flask(__name__, static_folder="static", template_folder="templates")
app.config["DEBUG"] = True

print("Chat with Moonbeam!")


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
    response = moonbeam_bot.get_response(user_input)
    print("Captain: " f'{str(user_input)}')
    print("Moonbeam: " f'{str(response)}')
    return str(response)


# have the app run a localhost on the port 5000
if __name__ == "__main__":
    app.run(host='0.0.0.0', port=5000)
