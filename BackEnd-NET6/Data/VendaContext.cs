using BackEnd_NET6.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_NET6.Data
{
    public class VendaContext : DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> options) : base(options)
        {
        }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<VendaStatus> VendaStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venda>()
                .HasOne(v => v.Vendedor)
                .WithMany(v => v.Vendas)
                .HasForeignKey(v => v.VendedorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venda>()
                .HasOne(v => v.Plano)
                .WithMany(v => v.Vendas)
                .HasForeignKey(v => v.PlanoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venda>()
                .HasMany(v => v.Status)
                .WithOne(s => s.Venda)
                .HasForeignKey(s => s.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
