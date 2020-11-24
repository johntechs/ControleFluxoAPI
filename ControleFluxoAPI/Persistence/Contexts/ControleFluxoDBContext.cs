using ControleFluxoAPI.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ControleFluxoAPI.Persistence.Contexts
{
    public class ControleFluxoDBContext:DbContext
    {
        public ControleFluxoDBContext()
        {
        }

        public ControleFluxoDBContext(DbContextOptions<ControleFluxoDBContext> options):base(options)
        {

        }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Fornecedor>().ToTable("Fornecedores");
            builder.Entity<Fornecedor>().HasKey(p => p.Id);
            builder.Entity<Fornecedor>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Fornecedor>().Property(p => p.Nome).IsRequired().HasMaxLength(100);

            builder.Entity<Fornecedor>().HasData(
                new Fornecedor { Id = 1, Nome = "Fornecedor de Peças" },
                new Fornecedor { Id = 2, Nome = "Fornecedor de Frutas" }
            );

            builder.Entity<Estoque>().ToTable("Estoques");
            builder.Entity<Estoque>().HasKey(p => p.Id);
            builder.Entity<Estoque>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Estoque>().HasData(
                new Estoque { Id = 100, nome = "Estoque Principal" }
            );


            builder.Entity<Vaga>().ToTable("Vagas");
            builder.Entity<Vaga>().HasKey(p => p.Id);
            builder.Entity<Vaga>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Vaga>().Property(p => p.IsOcupada).IsRequired();
            builder.Entity<Vaga>().HasData(
                new Vaga { Id = 1, IsOcupada = false, EstoqueId = 100 },
                new Vaga { Id = 2, IsOcupada = false, EstoqueId = 100 },
                new Vaga { Id = 3, IsOcupada = false, EstoqueId = 100 }
            );

            builder.Entity<Vaga>()
              .HasOne<Estoque>(s => s.Estoque)
                .WithMany(g => g.Vagas)
                   .HasForeignKey(s => s.EstoqueId);

            builder.Entity<Agendamento>().ToTable("Agendamentos");
            builder.Entity<Agendamento>().HasKey(p => p.Id);
            builder.Entity<Agendamento>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Agendamento>().Property(p => p.DataInicio).IsRequired();
            builder.Entity<Agendamento>().Property(p => p.DataFim).IsRequired();
            builder.Entity<Agendamento>().Property(p => p.PlacaVeiculo).IsRequired().HasMaxLength(7);

            builder.Entity<Agendamento>()
              .HasOne(s => s.Fornecedor)
                .WithMany(g => g.Agendamentos)
                   .HasForeignKey(s => s.FornecedorId);

            builder.Entity<Agendamento>()
              .HasOne(s => s.Vaga)
                .WithMany(g => g.Agendamentos)
                   .HasForeignKey(s => s.VagaId);

        }
    }
}
