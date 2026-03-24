import os
import tempfile
from typing import List

from fastapi import FastAPI, UploadFile, File, HTTPException
from pydantic import BaseModel, Field

from langchain_core.prompts import PromptTemplate
from langchain_community.document_loaders import PyPDFLoader
from langchain_ollama import ChatOllama

class TechSkills(BaseModel):
    programming_languages: List[str] = Field(
        default_factory=list, 
        description="Specific programming languages (e.g., Python, Java, Dart, C++)"
    )
    frameworks: List[str] = Field(
        default_factory=list, 
        description="Frameworks and libraries (e.g., Flutter, React, FastAPI, Spring Boot)"
    )
    tools_and_infrastructure: List[str] = Field(
        default_factory=list, 
        description="Developer tools, databases, and infrastructure (e.g., Docker, Git, Kubernetes, PostgreSQL)"
    )
    broad_concepts: List[str] = Field(
        default_factory=list, 
        description="Broad technical domains, methodologies, or concepts (e.g., Backend, Virtualization, Containers, CI/CD, Agile)"
    )

app = FastAPI(title="Skill Extractor Microservice")

skill_extraction_template = """
You are an expert technical recruiter AI. Your ONLY job is to extract technical skills, tools, programming languages, frameworks, and broad technical concepts from the provided resume text.

CRITICAL INSTRUCTIONS:
- Ignore all personal information, names, emails, and phone numbers.
- Ignore all company names, dates, and educational institutions.
- Categorize the extracted technical terms exactly according to the provided schema.
- If a category has no matching skills, return an empty list. Do not invent skills.

Resume Context:
{resume_text}
"""

prompt_template = PromptTemplate(template=skill_extraction_template, input_variables=['resume_text'])

model = ChatOllama(model="llama3.2", temperature=0).with_structured_output(TechSkills)

@app.post("/extract-skills", response_model=TechSkills)
async def extract_skills_endpoint(file: UploadFile = File(...)):
    if not file.filename.endswith('.pdf'):
        raise HTTPException(status_code=400, detail="Only PDF files are supported.")
        
    try:
        with tempfile.NamedTemporaryFile(delete=False, suffix=".pdf") as tmp:
            tmp.write(await file.read())
            tmp_path = tmp.name

        loader = PyPDFLoader(tmp_path)
        docs = loader.load()
        resume_text = "\n".join([doc.page_content for doc in docs])

        
        #print("\n========== PDF TEXT EXTRACTED ==========")
        #print(f"Total characters: {len(resume_text)}")
        #print(resume_text[:500])
        #print("========================================\n")
        
        os.remove(tmp_path)
        
        prompt = prompt_template.invoke({'resume_text': resume_text})
        response = model.invoke(prompt)
        
        return response
        
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"An error occurred: {str(e)}")
