namespace BackEnd_NET6.Models
{
    public class Vendedor
    {
        public int VendedorId { get; set; }
        public string Nome { get; set; }
        public ICollection<Venda> Vendas { get; set; }
    }
}
