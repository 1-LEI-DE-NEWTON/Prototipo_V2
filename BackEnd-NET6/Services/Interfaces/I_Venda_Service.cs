using BackEnd_NET6.Models;
using BackEnd_NET6.Models.DTOs;

namespace BackEnd_NET6.Services.Interfaces
{
    public interface I_Venda_Service
    {
        void AdicionarVenda(VendaDTO vendaDTO);
        List<Venda> ListarVendas();
        List<Venda> PesquisarVendasPorNome(string nome);
        Venda PesquisarVendaPorCPF(string cpf);
        Venda PesquisarVendaPorTelefone(string telefone);
        Venda PesquisarVendaPorId(int id);
        void AtualizarVenda(int id, VendaDTO vendaDTO);
        void AtualizarStatusVenda(int id, StatusVenda status);        
        List<Venda> ListarVendasNaFila();        
    }
}
