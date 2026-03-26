# Local AI API Setup Guide

Follow these instructions to set up the environment and run the local AI microservice. 

## Prerequisites
Ensure you have **Python 3** installed on your system before beginning.

## Step 1: Install and Enable Ollama
First, install Ollama to run the local language models. 

**Arch Linux Example:**
If you are on Arch Linux and have an NVIDIA GPU, you can install the CUDA-accelerated version and enable the background service:
```bash
sudo pacman -S ollama-cuda
sudo systemctl enable --now ollama
```

## Step 2: Download the Model
Once Ollama is running, pull the required Gemma 3 model weights to your machine:
```bash
ollama pull gemma3:4b
```

## Step 3: Set Up the Python Environment
Create an isolated virtual environment and install the required dependencies directly into it. This approach avoids needing to manually activate the environment.

```bash
# Create the virtual environment
python3 -m venv venv

# Install the required packages
venv/bin/pip install fastapi uvicorn pydantic langchain-ollama pypdf langchain_community python-multipart
```

## Step 4: Run the API
Start the FastAPI server using Uvicorn. The `--reload` flag will automatically restart the server whenever you save changes to your code.

```bash
venv/bin/python -m uvicorn main:app --reload
```

The server should now be running locally. You can access the interactive API documentation by navigating to `http://127.0.0.1:8000/docs` in your web browser.