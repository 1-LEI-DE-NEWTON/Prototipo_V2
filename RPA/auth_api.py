import logging
import os
from dotenv import load_dotenv
import requests
from config import API_CONFIG

load_dotenv()

logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

class AuthAPI:
    def __init__(self):
        self.base_url = API_CONFIG["base_url"]
        self.auth_token = self.authenticate()            
        
    def _get_headers(self):        
        return {
            "Authorization": f"Bearer {self.auth_token}",
            "Content-Type": "application/json"
        }

    def authenticate(self):        
        auth_url = f"{self.base_url}login"
        credentials = {
            "username": os.getenv("API_USER"),
            "password": os.getenv("API_PASSWORD")
        }        
        try:
            response = requests.post(auth_url, json=credentials, verify=False)
            response.raise_for_status()
            token = response.json().get("token")
            logging.info("Autenticação bem-sucedida.")
            return token
        except requests.RequestException as e:
            logging.error(f"Erro ao autenticar: {e}")
            return None