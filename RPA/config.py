from dotenv import load_dotenv
import os

# Carrega as variáveis de ambiente do arquivo .env
load_dotenv()

# Configurações do Site de Terceiros
SITE_CONFIG = {
    "url": os.getenv("SITE_URL"),  
    "login_endpoint": os.getenv("SITE_LOGIN_URL"),
    "cadastro_cliente_endpoint": os.getenv("SITE_CADASTRO_CLIENTE_URL"),
    "entrada_venda_endpoint": os.getenv("SITE_ENTRADA_VENDA_URL")
}

# Credenciais de Acesso
CREDENTIALS = {
    "username": os.getenv("SITE_LOGIN_USER"),
    "password": os.getenv("SITE_LOGIN_PASSWORD"),                    
}

# Configurações de Tempo de Espera
WAIT_CONFIG = {
    "default_wait": 10,  
    "max_wait": 30,      
    "retry_interval": 2  
}

API_CONFIG = {
    "base_url": "https://localhost:7223/api/",    
    "get_queue_endpoint": "rpa/obter-fila-vendas",    
    "update_status_endpoint": "rpa/atualizar-status-venda/{id}"    
}

# Configurações do RPA
RPA_CONFIG = {
    "headless": True,              
    "screenshot_on_error": True,   
    "log_path": "logs/rpa.log",    
}

# Configurações de Logs
LOG_CONFIG = {
    "log_level": "INFO",           
    "log_file": "logs/rpa.log",    
}

# Outras Configurações Gerais
GENERAL_CONFIG = {
    "retry_attempts": 3,  
}
