
namespace BackEnd_NET6.Models
{
    public class VendaStatus
    {
        public int Id { get; set; }        
        public string Status { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string StatusCadastroCliente { get; set; }
        public string StatusValidacaoCpf { get; set; }
        public string StatusCpfJaCliente { get; set; }
        public string StatusCadastroVenda { get; set; }
        public string ValidacaoIccid { get; set; }
        public Venda Venda { get; set; }
        public int VendaId { get; set; }
    }
}