from rpa_worker import RPAWorker
import logging

logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

class RPAProcess:
    def __init__(self, queue_manager):
        self.queue_manager = queue_manager

    def process_queue(self):
        """Processa a fila de vendas com um único login."""
        worker = RPAWorker()
        try:
            worker.login()
            vendas = self.queue_manager.fetch_queue()

            for venda in vendas:
                venda_id = venda["id"]
                try:
                    # Atualiza o status para "Em processamento"
                    self.queue_manager.update_status(venda_id, "Em processamento")
                    
                    # Processa a venda
                    worker.processar_venda(venda)
                    
                    # Atualiza o status para "Concluído"
                    self.queue_manager.update_status(venda_id, "Concluído")
                except Exception as e:
                    # Atualiza o status para "Erro" em caso de falha
                    logging.error(f"Erro ao processar venda {venda_id}: {e}")
                    self.queue_manager.update_status(venda_id, "Erro", detalhes=str(e))

        finally:
            worker.finalizar()  # Finaliza o navegador após processar todas as vendas
