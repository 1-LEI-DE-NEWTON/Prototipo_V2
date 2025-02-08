document.addEventListener("DOMContentLoaded", async () => {
    const params = new URLSearchParams(window.location.search);
    const vendaId = params.get("id");

    try {
        const venda = await apiRequest(`search/id/${vendaId}`);
        document.getElementById("nome_cliente").value = venda.nomeCliente;
        document.getElementById("telefone").value = venda.telefone;
        document.getElementById("email").value = venda.email;
        document.getElementById("cpf").value = venda.cpf;        
        document.getElementById("rg").value = venda.rg;
        
        const dataNascimento = new Date(venda.dataNascimento);
        const dataFormatada = dataNascimento.toLocaleDateString('pt-BR', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric'
        });
        document.getElementById("data_nascimento").value = dataFormatada;

        document.getElementById("cep").value = venda.cep;
        document.getElementById("endereco").value = venda.endereco;
        document.getElementById("numero").value = venda.numero;
        document.getElementById("complemento").value = venda.complemento;
        document.getElementById("data_vencimento").value = venda.dataVencimento;        
        
    } catch (error) {
        alert("Erro ao carregar detalhes da venda: \n" + error.message);
    }
});

/*
async function carregarStatusVenda(idVenda) {
    try {
        const response = await fetch(`/status-venda/${idVenda}`);
        const data = await response.json();
        document.getElementById("status").innerText = data.status; 
    } catch (error) {
        console.error("Erro ao carregar status:", error);
    }
}
    //Polling
setInterval(() => carregarStatusVenda(1), 5000);  // Substitua "1" pelo ID da venda real

*/

// Função para alternar entre modo de edição e somente-leitura
function toggleEditMode() {
    const inputs = document.querySelectorAll('.details-container input');
    const editButton = document.querySelector('.btn-editar');
    const isReadOnly = inputs[0].hasAttribute('readonly');

    inputs.forEach(input => {
        if (isReadOnly) {
            input.removeAttribute('readonly'); 
            input.style.backgroundColor = 'white'; 
        } else {
            input.setAttribute('readonly', 'readonly'); 
            input.style.backgroundColor = '#e9ecef'; 
        }
    });    
    editButton.textContent = isReadOnly ? 'Salvar' : 'Editar';
}