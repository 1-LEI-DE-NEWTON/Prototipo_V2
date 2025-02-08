using BackEnd_NET6.Data;
using BackEnd_NET6.Models;
using BackEnd_NET6.Models.DTOs;
using BackEnd_NET6.Services.Interfaces;

namespace BackEnd_NET6.Services
{
    public class Venda_service : I_Venda_Service
    {
        private readonly VendaContext _context;

        public Venda_service(VendaContext context)
        {
            _context = context;
        }

        public void AdicionarVenda(VendaDTO vendaDTO)
        {
            var venda = new Venda
            {
                NomeCliente = vendaDTO.NomeCliente,
                Email = vendaDTO.Email,
                Telefone = vendaDTO.Telefone,
                IsWhatsApp = vendaDTO.IsWhatsApp,
                CPF = vendaDTO.CPF,
                RG = vendaDTO.RG,                                
                DataNascimento = vendaDTO.DataNascimento.Date,
                CEP = vendaDTO.CEP,
                Endereco = vendaDTO.Endereco,
                Numero = vendaDTO.Numero,
                Complemento = vendaDTO.Complemento,
                DataVencimento = vendaDTO.DataVencimento,
                DataVenda = DateTime.Now,
                Pdv = vendaDTO.Pdv,
                IccidInicial = vendaDTO.IccidInicial,
                VendedorId = vendaDTO.VendedorId,
                PlanoId = vendaDTO.PlanoId
            };

            try 
            {
                _context.Vendas.Add(venda);
                _context.SaveChanges();
                
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar venda: " + e.Message);
            }
        }

        public List<Venda> ListarVendas()
        {
            try
            {
                return _context.Vendas.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar vendas: " + e.Message);
            }            
        }
        
        public List<Venda> PesquisarVendasPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return null;
            }
            
            try
            {
                return _context.Vendas
                               .Where(v => v.NomeCliente.Contains(nome))
                               .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar vendas por nome: " + e.Message);
            }               
        }

        public Venda PesquisarVendaPorId(int id)
        {
            if (id <= 0 || id == null)
            {
                return null;
            }
            try
            {
                return _context.Vendas
                               .FirstOrDefault(v => v.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar venda por id: " + e.Message);
            }
        }
        
        public Venda PesquisarVendaPorCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return null;
            }

            try
            {
                return _context.Vendas
                               .FirstOrDefault(v => v.CPF == cpf);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar venda por CPF: " + e.Message);
            }            
        }
                
        public Venda PesquisarVendaPorTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                return null;
            }
            try
            {
                return _context.Vendas
                               .FirstOrDefault(v => v.Telefone == telefone);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar venda por telefone: " + e.Message);
            }            
        }

        public void AtualizarVenda(int id, VendaDTO vendaDTO)
        {
            var venda = _context.Vendas.FirstOrDefault(v => v.Id == id);

            if (venda == null)
            {
                throw new Exception("Venda não encontrada");
            }

            venda.NomeCliente = vendaDTO.NomeCliente;
            venda.Email = vendaDTO.Email;
            venda.Telefone = vendaDTO.Telefone;
            venda.IsWhatsApp = vendaDTO.IsWhatsApp;
            venda.CPF = vendaDTO.CPF;
            venda.RG = vendaDTO.RG;
            venda.DataNascimento = vendaDTO.DataNascimento.Date;
            venda.CEP = vendaDTO.CEP;
            venda.Endereco = vendaDTO.Endereco;
            venda.Numero = vendaDTO.Numero;
            venda.Complemento = vendaDTO.Complemento;
            venda.DataVencimento = vendaDTO.DataVencimento;

            try
            {
                _context.Vendas.Update(venda);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar venda: " + e.Message);
            }
        }

        public void AtualizarStatusVenda(int id, StatusVenda status)
        {                        
            if (id <= 0)
            {
                throw new Exception("Id inválido");
            }

            if (string.IsNullOrEmpty(status.ToString()))
            {
                throw new Exception("Status inválido");
            }
        
            if (!Enum.IsDefined(typeof(StatusVenda), status))
            {
                throw new Exception("Status inválido");
            }

            try
            {
                var venda = _context.Vendas.FirstOrDefault(v => v.Id == id);

                if (venda == null)
                {
                    throw new Exception("Venda não encontrada");
                }

                venda.Status = status;

                _context.Vendas.Update(venda);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar status da venda: " + e.Message);
            }                        
        }

        public List<Venda> ListarVendasNaFila()
        {
            return _context.Vendas
                            .Where(v => v.Status == 0)
                            .ToList();                                                                                        
        }
    }        
}
