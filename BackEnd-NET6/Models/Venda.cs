using System;


namespace BackEnd_NET6.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public string Email { get; set; }
        public string NomeCliente { get; set; }    
        public string Telefone { get; set; }
        public bool IsWhatsApp { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }    
        public DateTime DataNascimento { get; set; }                 
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int DataVencimento { get; set; }
        public int IccidInicial { get; set; }
        public int IccidFinal { get; set; }
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
        public int PlanoId { get; set; }
        public Plano Plano { get; set; }
        public string Pdv { get; set; }                
        public List<VendaStatus> Status { get; set; }
    }
}
