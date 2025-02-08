using BackEnd_NET6.Data;
using BackEnd_NET6.Models;
using BackEnd_NET6.Models.DTOs;
using BackEnd_NET6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            //Adiciona vendastatus
            var vendaStatus = new VendaStatus
            {
                Status = "Na fila",
                Mensagem = "Venda aguardando processamento",
                StatusCadastroCliente = "Pendente",
                StatusValidacaoCpf = "Pendente",
                StatusCpfJaCliente = "Pendente",
                StatusCadastroVenda = "Pendente",
                ValidacaoIccid = "Pendente",
                DataAtualizacao = DateTime.Now
            };

            venda.Status = new List<VendaStatus>
            {
                vendaStatus
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
                return _context.Vendas
                .Include(v => v.Status)
                .Select(v => new Venda
                {
                    Id = v.Id,
                    NomeCliente = v.NomeCliente,
                    Email = v.Email,
                    Telefone = v.Telefone,
                    IsWhatsApp = v.IsWhatsApp,
                    CPF = v.CPF,
                    RG = v.RG,
                    DataNascimento = v.DataNascimento,
                    CEP = v.CEP,
                    Endereco = v.Endereco,
                    Numero = v.Numero,
                    Complemento = v.Complemento,
                    DataVencimento = v.DataVencimento,
                    DataVenda = v.DataVenda,
                    Pdv = v.Pdv,
                    IccidInicial = v.IccidInicial,
                    VendedorId = v.VendedorId,
                    Vendedor = v.Vendedor,
                    Plano = v.Plano,                    
                    PlanoId = v.PlanoId,
                    Status = v.Status.Select(s => new VendaStatus
                    {
                        Id = s.Id,
                        Mensagem = s.Mensagem,
                        StatusCadastroCliente = s.StatusCadastroCliente,
                        StatusValidacaoCpf = s.StatusValidacaoCpf,
                        StatusCpfJaCliente = s.StatusCpfJaCliente,
                        StatusCadastroVenda = s.StatusCadastroVenda,
                        ValidacaoIccid = s.ValidacaoIccid,
                        DataAtualizacao = s.DataAtualizacao,
                        VendaId = s.VendaId                        
                    }).ToList()
                })
                .ToList();
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
                               .Include(v => v.Status)
                               .Select(v => new Venda
                {
                    Id = v.Id,
                    NomeCliente = v.NomeCliente,
                    Email = v.Email,
                    Telefone = v.Telefone,
                    IsWhatsApp = v.IsWhatsApp,
                    CPF = v.CPF,
                    RG = v.RG,
                    DataNascimento = v.DataNascimento,
                    CEP = v.CEP,
                    Endereco = v.Endereco,
                    Numero = v.Numero,
                    Complemento = v.Complemento,
                    DataVencimento = v.DataVencimento,
                    DataVenda = v.DataVenda,
                    Pdv = v.Pdv,
                    IccidInicial = v.IccidInicial,
                    VendedorId = v.VendedorId,
                    Vendedor = v.Vendedor,
                    Plano = v.Plano,                    
                    PlanoId = v.PlanoId,
                    Status = v.Status.Select(s => new VendaStatus
                    {
                        Id = s.Id,
                        Mensagem = s.Mensagem,
                        StatusCadastroCliente = s.StatusCadastroCliente,
                        StatusValidacaoCpf = s.StatusValidacaoCpf,
                        StatusCpfJaCliente = s.StatusCpfJaCliente,
                        StatusCadastroVenda = s.StatusCadastroVenda,
                        ValidacaoIccid = s.ValidacaoIccid,
                        DataAtualizacao = s.DataAtualizacao,
                        VendaId = s.VendaId                        
                    }).ToList()
                })
                .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar vendas por nome: " + e.Message);
            }               
        }

        public Venda PesquisarVendaPorId(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            try
            {
                var response = _context.Vendas
                                 .Include(v => v.Status)
                                 .Select(v => new Venda
                {
                    Id = v.Id,
                    NomeCliente = v.NomeCliente,
                    Email = v.Email,
                    Telefone = v.Telefone,
                    IsWhatsApp = v.IsWhatsApp,
                    CPF = v.CPF,
                    RG = v.RG,
                    DataNascimento = v.DataNascimento,

                    CEP = v.CEP,
                    Endereco = v.Endereco,
                    Numero = v.Numero,
                    Complemento = v.Complemento,
                    DataVencimento = v.DataVencimento,
                    DataVenda = v.DataVenda,
                    Pdv = v.Pdv,
                    IccidInicial = v.IccidInicial,
                    VendedorId = v.VendedorId,
                    Vendedor = v.Vendedor,
                    Plano = v.Plano,
                    PlanoId = v.PlanoId,
                    Status = v.Status.Select(s => new VendaStatus
                    {
                        Id = s.Id,
                        Mensagem = s.Mensagem,
                        StatusCadastroCliente = s.StatusCadastroCliente,
                        StatusValidacaoCpf = s.StatusValidacaoCpf,
                        StatusCpfJaCliente = s.StatusCpfJaCliente,
                        StatusCadastroVenda = s.StatusCadastroVenda,
                        ValidacaoIccid = s.ValidacaoIccid,
                        DataAtualizacao = s.DataAtualizacao,
                        VendaId = s.VendaId
                    }).ToList()
                                 })
                               .FirstOrDefault(v => v.Id == id);

                if (response == null)
                {
                    return null;
                }

                return response;
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
                var response = _context.Vendas
                               .FirstOrDefault(v => v.CPF == cpf);
                if (response == null)
                {
                    return null;
                }
                return response;
                
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
                var response = _context.Vendas
                               .FirstOrDefault(v => v.Telefone == telefone);

                if (response == null)
                {
                    return null;
                }

                return response;
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

        public List<Venda> ListarVendasNaFila()
        {
            try
            {
                return _context.Vendas
                               .Where(v => v.Status.Any(s => s.Status == "Na fila"))
                               .Include(v => v.Status)
                               .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar vendas na fila: " + e.Message);
            }   
        }
    }               
}
