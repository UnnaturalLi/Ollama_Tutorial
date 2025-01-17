# Ollama_Tutorial
Welcome to this tutorial, I hope you enjoy it!

Test Command:
curl -X POST -H 'Content-Type: application/json' \
     -d '{
           "model": "llama3.2:latest",
           "prompt": "Hello from curl!"
         }' \
     http://localhost:11434/v1/completions
     
