using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Domain.Entities;

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

        // VIEW do relatório (somente leitura)
        //public DbSet<RelatorioLivrosPorAutorRow> RelatorioLivrosPorAutor => Set<RelatorioLivrosPorAutorRow>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibliotecaDbContext).Assembly);

            //// Mapeamento da VIEW (sem key)
            //modelBuilder.Entity<RelatorioLivrosPorAutorRow>(entity =>
            //{
            //    entity.HasNoKey();
            //    entity.ToView("vw_relatorio_livros_por_autor");
            //});

            base.OnModelCreating(modelBuilder);
        }


    }

}
