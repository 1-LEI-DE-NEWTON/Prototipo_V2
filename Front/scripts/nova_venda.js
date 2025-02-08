document.addEventListener('DOMContentLoaded', function() {
    let vendedoresData = []; 
    let planosData = [];      

    async function popularVendedores() {
        try {
            console.log("Buscando vendedores da API...");
            vendedoresData = await apiRequest('vendedores/listar');
            if (vendedoresData && Array.isArray(vendedoresData)) { 
                populateSelect('nome_vendedor', vendedoresData, 'Selecionar Vendedor', 'vendedorId');
            } else {
                console.warn("Dados de vendedores inválidos recebidos da API.");
                alert('Erro ao carregar vendedores: \n' + 'Dados inválidos recebidos da API.');
            }
        } catch (error) {
            console.error('Erro ao carregar vendedores:', error);
            alert('Erro ao carregar vendedores: \n' + error.message);
        }
    }

    async function popularPlanos() {
        try {
            console.log("Buscando planos da API...");
            planosData = await apiRequest('planos/listar');
            if (planosData && Array.isArray(planosData)) { 
                populateSelect('plano', planosData, 'Selecionar Plano', 'planoId');
            } else {
                console.warn("Dados de planos inválidos recebidos da API.");
                alert('Erro ao carregar planos: \n' + 'Dados inválidos recebidos da API.');
            }
        } catch (error) {
            console.error('Erro ao carregar planos:', error);
            alert('Erro ao carregar planos: \n' + error.message);
        }
    }

    function populateSelect(elementId, data, defaultOptionText, idKey) {
        const selectElement = document.getElementById(elementId);
        if (!selectElement) {
            console.warn(`Elemento ${elementId} não encontrado.`);
            return;
        }

        selectElement.innerHTML = ''; // Limpa as opções existentes

        // Adiciona a opção padrão
        const defaultOption = document.createElement('option');
        defaultOption.value = '';
        defaultOption.textContent = defaultOptionText;
        selectElement.appendChild(defaultOption);

        data.forEach(item => {
            const option = document.createElement('option');
            option.value = item[idKey]; 
            option.textContent = item.nome;
            selectElement.appendChild(option);
        });

        console.log(`Opções carregadas para ${elementId}:`, data);
    }

    popularVendedores();
    popularPlanos();

    document.getElementById('nome_vendedor').addEventListener('change', function() {
        console.log("Vendedor selecionado:", this.value);
    });

    document.getElementById('plano').addEventListener('change', function() {
        console.log("Plano selecionado:", this.value);
    });
});

document.getElementById("nova-venda-form").addEventListener("submit", async (event) => {
    event.preventDefault();
    
    const isWhatsapp = document.getElementById('is-whatsapp').checked;

    // Capturar IDs dos seletores
    const selectedVendedorId = document.getElementById('nome_vendedor').value;
    const selectedPlanoId = document.getElementById('plano').value;
    
    const venda = {
        nomeCliente: document.getElementById('nome_cliente').value,
        telefone: document.getElementById('telefone').value,
        isWhatsapp: isWhatsapp,        
        email: document.getElementById('email').value,
        cpf: document.getElementById('cpf').value,
        rg: document.getElementById('rg').value,
        dataNascimento: document.getElementById('data_nascimento').value,
        cep: document.getElementById('cep').value,
        endereco: document.getElementById('endereco').value,
        numero: document.getElementById('numero').value,
        complemento: document.getElementById('complemento').value,
        dataVencimento: document.getElementById('data_vencimento').value,
        iccidInicial: document.getElementById('iccid').value,
        planoId: selectedPlanoId,  
        vendedorId: selectedVendedorId,  
        pdv: "Teste"
    };

    // Validar os dados do formulário
    if (Object.values(venda).some(value => value === "")) {
        alert("Preencha todos os campos!");
        return;
    }
    
    // Enviar os dados para a API
    try {
        await apiRequest("vendas/adicionar", "POST", venda);
        alert("Venda cadastrada com sucesso!");
        window.location.href = "menu_inicial.html";                        
    } catch (error) {
        if (error.message.includes("CPF inválido")) {
            alert("CPF inválido! Por favor, verifique o CPF informado.");
        }
        else{
            alert("Erro ao cadastrar a venda! " + error.message);
        }
    }
});
