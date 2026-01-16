using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Domain.Entities.Views;

namespace Tjrj.Biblioteca.Infra.Contexts
{

    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options) { }

        public DbSet<Livro> Livros => Set<Livro>();
        public DbSet<Autor> Autores => Set<Autor>();
        public DbSet<Assunto> Assuntos => Set<Assunto>();

        public DbSet<FormaCompra> FormasCompra => Set<FormaCompra>();
        public DbSet<LivroPreco> LivrosPrecos => Set<LivroPreco>();

        public DbSet<VwRelatorioLivrosPorAutor> VwRelatorioLivrosPorAutor => Set<VwRelatorioLivrosPorAutor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibliotecaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VwRelatorioLivrosPorAutor>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_relatorio_livros_por_autor");
            });

            // ... seus outros mapeamentos
        }




    }

}
