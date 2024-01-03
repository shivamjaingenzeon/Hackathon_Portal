import pickle, torch
from flask import Flask, render_template, request, jsonify
from flask_cors import CORS
with open('data.pkl', 'rb') as file:
    tokenizer = pickle.load(file)

with open('model.pkl', 'rb') as file:
    model = pickle.load(file)

app = Flask(__name__)
CORS(app, origins='*')

# @app.route("/") #decorators
# def hello():
#     return  jsoinfy("Hello")

@app.route("/chat", methods=['POST','GET'])
# Function to process user query and generate answer
def generate_answer():
    try:
        data = request.json  # Get the JSON data sent from the React app
        question = data.get('question')
        print(f"The question is -> {question}")
        hackathon_name = data.get('hackathon_name')
        print(f"The hackathon_name is {hackathon_name}")
        # Tokenize question and documents
        # print(question,document)
        # Example usage

        #Dummy
        documents = [['''
                        The name of the hackathon is "IoT Hackathon - Build the Future with IoT Innovations".

                        The description of the hackathon is "Get ready for the IoT Hackathon, where creativity meets the Internet of Things! Join us to harness the power of IoT technologies and build groundbreaking solutions that will shape the future. Whether you're an IoT expert or a curious enthusiast, this hackathon offers a platform to explore, create, and transform ideas into IoT prototypes."

                        The hackathon will start from 22/08/2023.
                        The hackathon will end on 30/08/2023.
                        The hackathon will begin at time 08:00 A.M.
                        The hackathon will continue for 36 hours
                        The venuePune
                        Entry Fee: Rs. 2500/-

                        Teams size is fixed.
                        Team size can be a group of 7 people.
                        Participants can form team of 7.
                        No, An individual cannot form a team.
                        There are no specific skill requirement for the hackathon.
                        People of all skills can come and participate.
                        
                        The hackathon is open to participants of all skill levels, from blockchain beginners to experienced developers.
                        There are workshops.
                        All participants can attend workshops.
                        Topics of the workshops are blockchain fundamentals, smart contracts, and decentralized applications.
                        
                        Attendees will have the opportunity to attend workshops covering blockchain fundamentals, smart contracts, and decentralized applications.
                        
                        Throughout the event, there will be experienced blockchain experts available to provide guidance and support to participants.
                        
                        Join the hackathon to connect with like-minded blockchain enthusiasts, entrepreneurs, and industry leaders.
                        Connect with like-minded blockchain enthusiasts.
                        Connect with entrepreneurs.
                        Connect with industry leaders.
                        The aim is to showcase your innovative blockchain projects to a panel of judges. 
                        There is recognition and valuable feedback.
                        Feedback is given.
                        Assistance is provided to teams.
                        
                        The judges are a panel of blockchain experts.
                    '''],
                     [''' 
                        The name of the hackathon is "Gen-Z-verse Hackathon".   
                        The description of the hackathon is "Step into the world of blockchain technology at the Blockchain Hackathon, where innovation and decentralization merge! Whether you're a blockchain expert or an enthusiast eager to explore distributed ledgers, this hackathon provides a platform to build transformative blockchain applications and redefine industries."
                        The hackathon will start from 19/09/1992.
                        The hackathon will end on 19/09/1992.
                        The hackathon will begin at time 10:00 A.M.
                        The hackathon will continue for 24 hours.
                        The venue of the hackathon is ' World Trade Centre Tower 3 '.
                        The entry fee for the hackathon is Rs 5500/-.
                        Team Information:
                        Teams size is not fixed.
                        Team can be an individual or a group of people.
                        Participants can form teams of any size or participate individually.
                        Yes, An individual can form a team.
                        There are no specific skill requirement for the hackathon.
                        People of all skills can come and participate.
                        
                        The hackathon is open to participants of all skill levels, from blockchain beginners to experienced developers.
                        There are workshops.
                        All participants can attend workshops.
                        Topics of the workshops are blockchain fundamentals, smart contracts, and decentralized applications.
                        
                        Attendees will have the opportunity to attend workshops covering blockchain fundamentals, smart contracts, and decentralized applications.
                        
                        Throughout the event, there will be experienced blockchain experts available to provide guidance and support to participants.
                        
                        Join the hackathon to connect with like-minded blockchain enthusiasts, entrepreneurs, and industry leaders.
                        Connect with like-minded blockchain enthusiasts.
                        Connect with entrepreneurs.
                        Connect with industry leaders.
                        The aim is to showcase your innovative blockchain projects to a panel of judges. 
                        There is recognition and valuable feedback.
                        Feedback is given.
                        Assistance is provided to teams.
                        Showcase your innovative blockchain projects to a panel of judges and receive recognition and valuable feedback.
                        The judges are a panel of blockchain experts.
                     ''']
        ]
        # print(f'Doc 1 is -> {documents[1]}')
        all_descriptions = {
            'iot hackathon': '''
                IoT Hackathon - Build the Future with IoT Innovations

                Description: Get ready for the IoT Hackathon, where creativity meets the Internet of Things! Join us to harness the power of IoT technologies and build groundbreaking solutions that will shape the future. Whether you're an IoT expert or a curious enthusiast, this hackathon offers a platform to explore, create, and transform ideas into IoT prototypes.

                Date: 22/08/2023
                Time: 08:00
                Venue: Pune
                Entry Fee: Rs. 2500/-

                Highlights:

                Team Formation: Participants can form teams of [Specify team size] or participate individually.
                Skill Levels: Open to all skill levels, from IoT beginners to experienced professionals.
                Workshops: Attend workshops covering IoT hardware, platforms, data analytics, and cloud integration.
                Expert Mentors: Experienced IoT professionals will provide guidance and support throughout the hackathon.
                Networking Opportunities: Connect with fellow IoT enthusiasts and potential collaborators.
                Project Showcasing: Showcase your IoT projects to a panel of judges and receive recognition and feedback.
                Prizes: Exciting prizes await outstanding IoT innovations.
            ''',
            'blockchain hackathon':
                ''' Blockchain Hackathon - Revolutionize with Blockchain Technology

                    Description:
                    Step into the world of blockchain technology at the Blockchain Hackathon, where innovation and decentralization merge! Whether you're a blockchain expert or an enthusiast eager to explore distributed ledgers, this hackathon provides a platform to build transformative blockchain applications and redefine industries.

                    Date: 19/09/1992
                    Time: 10:00 A.M.
                    Venue: Narsobachi Wadi
                    venue: Narsobachi Wadi
                    Entry Fee: Rs 5500/-

                    Highlights:

                    Team Formation: Participants can form teams of [Specify team size] or participate individually.
                    Skill Levels: Open to all skill levels, from blockchain beginners to experienced developers.
                    Workshops: Attend workshops covering blockchain fundamentals, smart contracts, and decentralized applications.
                    Expert Mentors: Blockchain experts will be available to provide guidance and support.
                    Networking Opportunities: Connect with fellow blockchain enthusiasts, entrepreneurs, and industry leaders.
                    Project Showcasing: Showcase your blockchain projects to a panel of judges and receive recognition and feedback.
                    Prizes: Exciting prizes await outstanding blockchain innovations.
            '''


        #     '''
            #     1. Python Hackathon - Innovate with Python Power
            #
            # Description:
            # Join us for the Python Hackathon, a thrilling event dedicated to unleashing the power of Python programming! Whether you're a seasoned Python developer or a beginner eager to explore its possibilities, this hackathon is your chance to create innovative projects using the versatile Python ecosystem.
            #
            # Python Hackathon Date: 22/08/2022
            # Time: 08:00 A.M.
            # Venue: Pune
            # Entry Fee: 20000
            #
            # Highlights:
            #
            # Team Formation: Participants can form teams of 6 or participate individually.
            # Skill Levels: All skill levels are welcome, from Python enthusiasts to experienced developers.
            # Workshops: Engage in interactive workshops covering various Python libraries, frameworks, and APIs.
            # Expert Mentors: Python experts will be available throughout the hackathon to provide guidance and support.
            # Networking Opportunities: Connect with fellow Python enthusiasts, expand your network, and foster collaborations.
            # Project Showcasing: Present your Python projects to a panel of judges and receive valuable feedback.
            # Prizes: Exciting prizes await outstanding projects and innovations.
            #     '''
        }
        if hackathon_name == "iot hackathon":
            document = documents[0]
        elif hackathon_name == 'blockchain hackathon':
            document = documents[1]

        # print(documents[0])
        inputs = tokenizer(question, " ".join(document), return_tensors="pt", padding=True, truncation=True)

        # Get model predictions
        with torch.no_grad():
            outputs = model(**inputs)
            start_scores = outputs.start_logits
            end_scores = outputs.end_logits

        # Find the start and end positions of the answer
        start_index = torch.argmax(start_scores)
        end_index = torch.argmax(end_scores)

        # Convert token indices to actual tokens
        all_tokens = tokenizer.convert_ids_to_tokens(inputs["input_ids"][0])
        answer = tokenizer.convert_tokens_to_string(all_tokens[start_index:end_index + 1])
        if answer == '<s>':
            return jsonify("Retry your query!!")
        return jsonify(answer)
    except Exception as e:
        return jsonify({'error': str(e)}), 400




if __name__ == "__main__":
    app.run(port=8000,debug = True)

