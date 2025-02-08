namespace BackEnd_NET6.Models.DTOs{
    public class VendaStatusDTO
    {
        public string Status { get; set; }
        public string Mensagem { get; set; }
        public string StatusCadastroCliente { get; set; }
        public string StatusValidacaoCpf { get; set; }
        public string StatusCpfJaCliente { get; set; }
        public string StatusCadastroVenda { get; set; }
        public string ValidacaoIccid { get; set; }
    }
}