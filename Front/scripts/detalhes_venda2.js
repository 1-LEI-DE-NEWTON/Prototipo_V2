
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

// Polling function
function pollStatus(vendaId) {
    setInterval(async () => {
        try {
            const data = await apiRequest(`search/statusvenda/${vendaId}`);                        
            console.log(data);
            
            updateStatus("status-cadastroCliente", data.statusCadastroCliente, data.statusClassCadastroCliente);
            updateStatus("status-validacaoCPF", data.statusValidacaoCpf, data.statusClassValidacaoCPF);
            updateStatus("status-cpfJaCliente", data.statusCpfJaCliente, data.statusClassCpfJaCliente);
            updateStatus("status-cadastroVenda", data.statusCadastroVenda, data.statusClassCadastroVenda);
            updateStatus("status-validacaoICCID", data.validacaoIccid, data.statusClassValidacaoICCID);
        } catch (error) {
            console.error("Erro ao buscar status da venda:", error);
        }
    }, 1000); // 1000 = 1 segundo
}

// Atualiza o status do tópico ou subtópico
function updateStatus(elementId, statusText, statusClass) {
    console.log(`Atualizando ${elementId} para ${statusText} (${statusClass})`);
    const element = document.getElementById(elementId);
    element.textContent = statusText;
    element.className = `status ${statusClass}`;
}

// Iniciar o polling quando a página for carregada
document.addEventListener("DOMContentLoaded", async () => {
    const params = new URLSearchParams(window.location.search);
    const vendaId = params.get("id");
    if (vendaId) {
        pollStatus(vendaId);
    } else {
        console.error("ID da venda não encontrado na URL.");
    }
});











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