
function toggleExpand(topicId) {
    const content = document.getElementById(topicId);
    if (content.style.display === "block") {
        content.style.display = "none";
    } else {
        content.style.display = "block";
    }
}

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

function showDetails(statusId) {
    const modal = document.getElementById("modal");
    const modalText = document.getElementById("modal-text");
    
    const messages = {
        validacaoCPF: "A validação do CPF foi realizada com sucesso.",
        cpfJaCliente: "Erro: O CPF informado já está registrado como cliente.",
        validacaoICCID: "A validação do ICCID está em andamento. Aguarde."
    };

    modalText.textContent = messages[statusId] || "Informações não disponíveis.";
    modal.classList.remove("hidden");
}

function closeModal(event) {
    const modal = document.getElementById("modal");
    modal.classList.add("hidden");
    event.stopPropagation();
}
