from transformers import AutoTokenizer, AutoModelForQuestionAnswering
import torch

# Load pre-trained QA model
model_name = "deepset/roberta-base-squad2"
tokenizer = AutoTokenizer.from_pretrained(model_name)
model = AutoModelForQuestionAnswering.from_pretrained(model_name)

import pickle
filename = 'data.pkl'

with open(filename, 'wb') as file:
    pickle.dump(tokenizer, file)

filename2 = 'model.pkl'

with open(filename2, 'wb') as file:
    pickle.dump(model, file)