import logging

logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

def log_info(msg):
    logging.info(msg)

def log_error(msg):
    logging.error(msg)

def log_warning(msg):
    logging.warning(msg)

def log_debug(msg):
    logging.debug(msg)

def log_critical(msg):
    logging.critical(msg)

def log_exception(msg):
    logging.exception(msg)

