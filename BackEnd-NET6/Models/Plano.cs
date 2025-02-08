namespace BackEnd_NET6.Models
{
    public class Plano
    {
        public int PlanoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public ICollection<Venda> Vendas { get; set; }        
    }
}
