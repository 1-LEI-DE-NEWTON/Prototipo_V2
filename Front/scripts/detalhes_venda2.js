
// Expande ou recolhe o conteúdo do tópico
function toggleExpand(topicId) {
    const content = document.getElementById(topicId);
    if (content.style.display === "block") {
        content.style.display = "none";
    } else {
        content.style.display = "block";
    }
}

// Exibe informações detalhadas no modal
function showDetails(statusId) {
    const modal = document.getElementById("modal");
    const modalText = document.getElementById("modal-text");

    // Define mensagens personalizadas para cada status
    const messages = {
        validacaoCPF: "A validação do CPF foi realizada com sucesso.",
        cpfJaCliente: "Erro: O CPF informado já está registrado como cliente.",
        validacaoICCID: "A validação do ICCID está em andamento. Aguarde."
    };

    modalText.textContent = messages[statusId] || "Informações não disponíveis.";
    modal.classList.remove("hidden");
}

// Fecha o modal
function closeModal(event) {
    const modal = document.getElementById("modal");
    modal.classList.add("hidden");
    event.stopPropagation();
}

// Polling function to get status updates from the API
function pollStatus(vendaId) {
    setInterval(async () => {
        try {
            const response = await fetch(`/api/vendas/${vendaId}/status`);
            const data = await response.json();

            // Update the status elements
            updateStatus("status-cadastroCliente", data.statusCadastroCliente, data.statusClassCadastroCliente);
            updateStatus("status-validacaoCPF", data.statusValidacaoCPF, data.statusClassValidacaoCPF);
            updateStatus("status-cpfJaCliente", data.statusCpfJaCliente, data.statusClassCpfJaCliente);
            updateStatus("status-cadastroVenda", data.statusCadastroVenda, data.statusClassCadastroVenda);
            updateStatus("status-validacaoICCID", data.statusValidacaoICCID, data.statusClassValidacaoICCID);
        } catch (error) {
            console.error("Erro ao buscar status da venda:", error);
        }
    }, 5000); // Polling every 5 seconds
}

// Atualiza o status do tópico ou subtópico
function updateStatus(elementId, statusText, statusClass) {
    const element = document.getElementById(elementId);
    element.textContent = statusText;
    element.className = `status ${statusClass}`;
}

// Iniciar o polling para a venda específica (substitua com o ID real da venda)
pollStatus('12345');  // Exemplo de ID da venda











// Alterna a visibilidade do menu suspenso
function toggleMenu() {
    const menu = document.getElementById("menu-dropdown");
    menu.classList.toggle("hidden");
}

// Fecha o menu ao clicar fora dele
document.addEventListener("click", function (event) {
    const menu = document.getElementById("menu-dropdown");
    const button = document.querySelector(".menu-button");

    if (!menu.contains(event.target) && !button.contains(event.target)) {
        menu.classList.add("hidden");
    }
});

// Ação do botão "Cancelar Envio"
function cancelarEnvio() {
    alert("O envio foi cancelado.");
    document.getElementById("menu-dropdown").classList.add("hidden");
}

// Ação do botão "Editar Envio"
function editarEnvio() {
    alert("O envio foi editado.");
    document.getElementById("menu-dropdown").classList.add("hidden");
}