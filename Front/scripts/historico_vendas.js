document.addEventListener("DOMContentLoaded", async () => {
    const historicoContainer = document.getElementById("historico-container");

    if (!historicoContainer) {
        console.error("Elemento 'historico-container' não encontrado no DOM.");
        return;
    }

    try {
        const vendas = await apiRequest("servicos/listar");
        renderVendas(vendas);
    } catch (error) {
        alert("Erro ao carregar histórico. Redirecionando para página de login.\n" + error.message);
    }

    const searchInput = document.getElementById('search-input');
    const searchBtn = document.getElementById('search-btn');

    searchBtn.addEventListener('click', async () => {
        const query = searchInput.value.trim();
        if (query) {
            try {
                let vendas;
                const cleanedQuery = query.replace(/\D/g, ''); 
                if (cleanedQuery === "") {                    
                    vendas = await apiRequest(`search/nome/${encodeURIComponent(query)}`);
                } else if (cleanedQuery.length === 11) {                    
                    vendas = await apiRequest(`search/cpf/${encodeURIComponent(cleanedQuery)}`);
                } else {
                    alert("Formato de pesquisa inválido. Use nome ou CPF.");
                    return;
                }
                renderVendas(vendas);
            } catch (error) {
                alert("Erro ao realizar pesquisa: " + error.message);
            }
        }
    });
});

function renderVendas(vendas) {
    const historicoContainer = document.getElementById("historico-container");
    historicoContainer.innerHTML = ''; 

    if (!Array.isArray(vendas)) {
        vendas = [vendas];
    }

    vendas.forEach((venda) => {
        const vendaBox = document.createElement("div");
        vendaBox.className = "sales-box";

        const dataVenda = new Date(venda.dataVenda);            

        const dataFormatada = dataVenda.toLocaleString('pt-BR', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });

        vendaBox.innerHTML = `
            <p class="client-name"> ${venda.nomeCliente}</p>
            <p class="sale-date"><strong>Data da venda:</strong> ${dataFormatada}</p>
        `;
        vendaBox.addEventListener("click", () => {
            verDetalhes(venda.id);
        });
        historicoContainer.appendChild(vendaBox);
    });
}

function verDetalhes(id) {
    window.location.href = `detalhes_venda2.html?id=${id}`;
}