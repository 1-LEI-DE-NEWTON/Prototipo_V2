using BackEnd_NET6.Models;
using BackEnd_NET6.Models.DTOs;

namespace BackEnd_NET6.Services.Interfaces
{
    public interface I_VendaStatus_Service
    {
        void AdicionarVendaStatus(VendaStatusDTO vendaStatusDTO);
        void AtualizarVendaStatus(int id, VendaStatusDTO vendaStatusDTO);
        VendaStatus ObterVendaStatus(int id);
        VendaStatus SearchByVendaId(int vendaId);
    }
}