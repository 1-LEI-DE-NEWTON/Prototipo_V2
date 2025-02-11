
using BackEnd_NET6.Data;
using BackEnd_NET6.Models;
using BackEnd_NET6.Models.DTOs;
using BackEnd_NET6.Services.Interfaces;

namespace BackEnd_NET6.Services
{
    public class VendaStatus_Service : I_VendaStatus_Service
    {
        private readonly VendaContext _context;

        public VendaStatus_Service(VendaContext context)
        {
            _context = context;
        }

        public void AdicionarVendaStatus(VendaStatusDTO vendaStatusDTO)
        {
            var vendaStatus = new VendaStatus
            {
                Status = vendaStatusDTO.Status,
                Mensagem = vendaStatusDTO.Mensagem,
                StatusCadastroCliente = vendaStatusDTO.StatusCadastroCliente,
                StatusValidacaoCpf = vendaStatusDTO.StatusValidacaoCpf,
                StatusCpfJaCliente = vendaStatusDTO.StatusCpfJaCliente,
                StatusCadastroVenda = vendaStatusDTO.StatusCadastroVenda,
                ValidacaoIccid = vendaStatusDTO.ValidacaoIccid,
                DataAtualizacao = DateTime.Now
            };

            try
            {
                _context.VendaStatus.Add(vendaStatus);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar status da venda: " + e.Message);
            }
        }

        public void AtualizarVendaStatus(int id, VendaStatusDTO vendaStatusDTO)
        {
            if (id <= 0)
            {
                throw new Exception("Id inválido");
            }

            var vendaStatus = _context.VendaStatus.FirstOrDefault(v => v.VendaId == id);

            if (vendaStatus == null)
            {
                throw new Exception("Status da venda não encontrado");
            }

            vendaStatus.Status = vendaStatusDTO.Status;
            vendaStatus.Mensagem = vendaStatusDTO.Mensagem;
            vendaStatus.StatusCadastroCliente = vendaStatusDTO.StatusCadastroCliente;
            vendaStatus.StatusValidacaoCpf = vendaStatusDTO.StatusValidacaoCpf;
            vendaStatus.StatusCpfJaCliente = vendaStatusDTO.StatusCpfJaCliente;
            vendaStatus.StatusCadastroVenda = vendaStatusDTO.StatusCadastroVenda;
            vendaStatus.ValidacaoIccid = vendaStatusDTO.ValidacaoIccid;
            vendaStatus.DataAtualizacao = DateTime.Now;

            try
            {
                _context.VendaStatus.Update(vendaStatus);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar status da venda: " + e.Message);
            }
        }

        public VendaStatus ObterVendaStatus(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id inválido");
            }

            var vendaStatus = _context.VendaStatus.FirstOrDefault(v => v.Id == id);

            if (vendaStatus == null)
            {
                throw new Exception("Status da venda não encontrado");
            }

            return vendaStatus;
        }

        public VendaStatus SearchByVendaId(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id inválido");
            }

            var vendaStatus = _context.VendaStatus.FirstOrDefault(v => v.VendaId == id);

            if (vendaStatus == null)
            {
                throw new Exception("Status da venda não encontrado");
            }

            return vendaStatus;
        }
    }
}