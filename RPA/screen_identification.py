import logging
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from config import WAIT_CONFIG
from selenium.common.exceptions import TimeoutException

logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

class ScreenIdentifier:
    def __init__(self, driver):        
        self.driver = driver

    def identificar_cpf_ja_cadastrado(self):        
        try:
            logging.info("Verificando se o CPF já está cadastrado.")
            cpf_duplicado_element = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.presence_of_element_located((By.CSS_SELECTOR, 'mat-error'))
            )
            if "Já existe cadastro vinculado ao documento informado" in cpf_duplicado_element.text:
                logging.warning("CPF já cadastrado detectado.")
                return True
            else:
                logging.info("CPF não cadastrado.")
                return False
        except Exception as e:
            logging.error(f"Erro ao verificar CPF cadastrado: {e}")
            return False
    
    def identificar_cpf_invalido(self):
        try:
            logging.info("Verificando se o CPF é inválido.")
            if "CPF inválido" in self.driver.page_source:
                logging.warning("CPF inválido detectado.")
                return True
            else:
                logging.info("CPF válido.")
                return False
        except Exception as e:
            logging.error(f"Erro ao verificar CPF inválido: {e}")            

    def verificar_pop_up(self):
        try:            
            pop_up_positivo = self._is_element_present(By.CSS_SELECTOR, '.alert.alert-success')
            if pop_up_positivo:
                mensagem = self.driver.find_element(By.CSS_SELECTOR, '.alert.alert-success span:nth-of-type(2)').text
                logging.info(f"Pop-up positivo identificado: {mensagem}")
                return True
            
            pop_up_negativo = self._is_element_present(By.CSS_SELECTOR, '.alert.alert-danger')
            if pop_up_negativo:
                mensagem = self.driver.find_element(By.CSS_SELECTOR, '.alert.alert-danger span:nth-of-type(2)').text
                logging.warning(f"Pop-up negativo identificado: {mensagem}")
                return False
            
            logging.info("Nenhum pop-up relevante encontrado.")
            return None
        except Exception as e:
            logging.error(f"Erro ao verificar pop-ups: {e}")
            return None

    def selecionar_iccid_inserido(self):
        try:
            logging.info("Aguardando a lista de itens carregar.")
                        
            primeiro_item = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.element_to_be_clickable((By.CSS_SELECTOR, 'tr.mat-row:first-child mat-radio-button'))
            )

            primeiro_item.click()
            logging.info("ICCId selecionado com sucesso.")
            return True
        
        except Exception as e:
            logging.error(f"Erro ao selecionar o ICCID: {e}")
            raise

    def selecionar_ddd(self, ddd="85"):
        try:
            logging.info(f"Selecionando o DDD {ddd} na lista.")
                
            lista_ddd = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.element_to_be_clickable((By.CSS_SELECTOR, '.mat-select-arrow-wrapper'))
            )

            lista_ddd.click()                        

            # Aguarda e localiza o item correspondente ao DDD
            opcao_ddd = WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
            EC.element_to_be_clickable((By.XPATH, f'//mat-option//span[contains(text(), "{ddd}")]'))
        )            

            opcao_ddd.click()

            logging.info(f"DDD {ddd} selecionado com sucesso.")
        except Exception as e:
            logging.error(f"Erro ao selecionar o DDD {ddd}: {e}")
            raise

    def _is_element_present(self, by, identifier):
        try:
            WebDriverWait(self.driver, WAIT_CONFIG["default_wait"]).until(
                EC.presence_of_element_located((by, identifier))
            )
            return True
        except:
            return False

    def corrigir_erro(self):
        """
        Executa ações para corrigir erros específicos, se possível.
        Exemplo: Navegar para uma tela alternativa ou preencher dados faltantes.
        """
        try:
            logging.info("Tentando corrigir o erro detectado.")
            
            # Exemplo: Clicar no botão para acessar tela alternativa
            if self._is_element_present(By.ID, "botao_corrigir"):
                self.driver.find_element(By.ID, "botao_corrigir").click()
                logging.info("Correção iniciada com sucesso.")
                return True
            else:
                logging.error("Botão de correção não encontrado.")
                return False
        except Exception as e:
            logging.error(f"Erro ao tentar corrigir: {e}")
            return False
