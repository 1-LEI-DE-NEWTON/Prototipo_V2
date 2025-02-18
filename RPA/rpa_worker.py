import logging
import os
import time
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from screen_identification import ScreenIdentifier
from config import SITE_CONFIG, WAIT_CONFIG
from dotenv import load_dotenv


# centralizar tudo no config. atualizar arquivos para remover o env
load_dotenv()

logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

class RPAWorker:
    def __init__(self):        
        self.driver = webdriver.Chrome()
        self.driver.maximize_window()
        self.screen_identifier = ScreenIdentifier(self.driver)

    def login(self):        
        try:
            logging.info("Iniciando o login no site de terceiros.")                        

            self.driver.get(SITE_CONFIG["login_endpoint"])
            self.driver.implicitly_wait(10)
            
            
            usernameField = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.presence_of_element_located((By.CSS_SELECTOR, '[formcontrolname="userId"]'))
            )                                                                                                                    
            
            time.sleep(2)

            usernameField.send_keys(os.getenv("SITE_LOGIN_USER"))

            passwordField = self.driver.find_element(By.CSS_SELECTOR, '[formcontrolname="password"]')

            passwordField.send_keys(os.getenv("SITE_LOGIN_PASSWORD"))
            
            self.driver.implicitly_wait(10)

            self.driver.find_element(By.CSS_SELECTOR, 'button[type="submit"]').click()

            WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.presence_of_element_located((By.CSS_SELECTOR, os.getenv("OK_LOGIN_SCREEN")))
            )
            logging.info("Login realizado com sucesso.")
        except Exception as e:
            logging.error(f"Erro durante o login: {e}")
            raise

    def cadastro_cliente(self, venda):        
        try:
            logging.info(f"Preenchendo dados cliente para venda ID {venda['id']}.")
            
            self.driver.get(os.getenv("SITE_CADASTRO_CLIENTE_URL"))                                    

            self.driver.implicitly_wait(10)
                        
            time.sleep(2)
            
            self._preencher_campo(By.CSS_SELECTOR, '[formcontrolname="document"]', venda["cpf"])
                        
            #Clica em algum lugar fora da textbox para que o campo de CPF seja validado
            self.driver.find_element(By.CSS_SELECTOR, 'body').click() #ok

            time.sleep(3)
                        
            if self.screen_identifier.identificar_cpf_invalido():
                logging.warning("CPF inválido.")
                return            
            
            #CONSERTAR ABAIXO, RPA QUEBROU

        
            self.driver.find_element(By.CSS_SELECTOR, 'button.mat-raised-button.mat-primary').click()

            time.sleep(2)
            
            if self.screen_identifier.identificar_cpf_ja_cadastrado():
                logging.warning("CPF já cadastrado. Pulando para cadastro de venda.")
                self.entrada_venda(venda)
                return
                                    
        except Exception as e:
            logging.error(f"Erro ao processar venda {venda['id']}: {e}")
            raise            

    def entrada_venda(self, venda):                
        try:
            logging.info(f"Processando cadastro de venda, ID {venda['id']}.")
            self.driver.get(os.getenv("SITE_ENTRADA_VENDAS_URL"))

            self._preencher_campo(By.CSS_SELECTOR, '[aria-label="CPF"]', venda["cpf"])                        
                       
            resultados = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.presence_of_element_located((By.CSS_SELECTOR, '.client-identification__results')))
            
            primeiro_item = WebDriverWait(resultados, WAIT_CONFIG["default_wait"]).until(
                EC.element_to_be_clickable((By.CSS_SELECTOR, '.client-identification__results button.d-flex.flex-column')))
                        
            time.sleep(3)

            primeiro_item.click()     
            
            self.driver.find_element(By.CSS_SELECTOR, 'button.mat-raised-button.mat-primary').click()            
            
            WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.url_contains(os.getenv("SITE_VENDA_URL"))
            )                                    
                        
            time.sleep(2)

            self.driver.find_element(By.CSS_SELECTOR, 'button.mat-raised-button.mat-primary').click()                                    

            WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.url_contains(os.getenv("SITE_VENDA_CHIP_URL"))
            )
            
            self._preencher_campo(By.CSS_SELECTOR, '[aria-label="ICCID inicial"]', venda["iccid_inicial"])
                                              
            inserir_button = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.element_to_be_clickable((By.CSS_SELECTOR, 'button.mat-stroked-button.mat-primary')))
            inserir_button.click()            

            if not self.screen_identifier.verificar_pop_up():
                logging.info(f"Não foi possível validar o ICCID {venda['iccid_inicial']}")
                return
            
            if not self.screen_identifier.selecionar_iccid_inserido():
                logging.info(f"Não foi possível selecionar o ICCID na lista.")                
                return
                        
            gerar_linha_button = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.element_to_be_clickable((By.CSS_SELECTOR, 'button.mat-stroked-button.mat-primary.mr-2')))
            gerar_linha_button.click()
            
            self.screen_identifier.selecionar_ddd("85")

            # Aguarda a linha reservada ser gerada
            time.sleep(4)
            
            reservar_button = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.element_to_be_clickable((By.CSS_SELECTOR, 'button.mat-raised-button.mat-primary[type="submit"]'))
            )
            reservar_button.click()            

            proximo_button = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.element_to_be_clickable((By.CSS_SELECTOR, 'button.mat-raised-button.mat-primary'))
            )
            proximo_button.click()

            #Falta a parte dos planos

            logging.info(f"Venda ID {venda['id']} processada com sucesso.")


        except Exception as e:
            logging.error(f"Erro ao processar venda {venda['id']}: {e}")
            raise

    def _preencher_campo(self, by, identifier, value):
        """Preenche um campo do formulário."""
        campo = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
            EC.presence_of_element_located((by, identifier))
        )
        campo.clear()
        campo.send_keys(value)
        logging.info(f"Campo {identifier} preenchido com: {value}")

    def _corrigir_erro(self):
        """Lida com erros identificados durante o processamento."""
        try:
            # Implementa a lógica para corrigir erros, se possível
            logging.info("Tentando corrigir o erro.")
            self.driver.find_element(By.ID, "botao_corrigir").click()
        except Exception as e:
            logging.error(f"Erro ao tentar corrigir: {e}")
            raise

    def finalizar(self):
        """Encerra o navegador e libera os recursos."""
        self.driver.quit()
        logging.info("Navegador fechado.")

# Inicializa o RPA Worker
if __name__ == "__main__":
    worker = RPAWorker()
    worker.login()
    worker.cadastro_cliente({
        "id": 1,
        "nomeCliente": "João da Silva",
        "telefone": "11999999999",
        "cpf": os.getenv("CPF"),
        "iccid_inicial": os.getenv("ICCID_INICIAL"),
        "valor": 100.00
    })