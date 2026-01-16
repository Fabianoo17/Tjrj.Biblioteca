using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Domain.Entities;

namespace Tjrj.Biblioteca.Infra.Configurations
{

    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");

            builder.HasKey(x => x.Codl);
            builder.Property(x => x.Codl).HasColumnName("Codl");

            builder.Property(x => x.Titulo).HasColumnName("Titulo").HasMaxLength(40).IsRequired();
            builder.Property(x => x.Editora).HasColumnName("Editora").HasMaxLength(40).IsRequired();
            builder.Property(x => x.Edicao).HasColumnName("Edicao").IsRequired();
            builder.Property(x => x.AnoPublicacao).HasColumnName("AnoPublicacao").HasMaxLength(4).IsRequired();
        }
    }

    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor");

            builder.HasKey(x => x.CodAu);
            builder.Property(x => x.CodAu).HasColumnName("CodAu");

            builder.Property(x => x.Nome).HasColumnName("Nome").HasMaxLength(40).IsRequired();
        }
    }

    public class AssuntoConfiguration : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> builder)
        {
            builder.ToTable("Assunto");

            builder.HasKey(x => x.CodAs);
            builder.Property(x => x.CodAs).HasColumnName("CodAs");

            builder.Property(x => x.Descricao).HasColumnName("Descricao").HasMaxLength(20).IsRequired();
        }
    }

    public class LivroAutorConfiguration : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.ToTable("Livro_Autor");

            builder.HasKey(x => new { x.Livro_Codl, x.Autor_CodAu });

            builder.Property(x => x.Livro_Codl).HasColumnName("Livro_Codl");
            builder.Property(x => x.Autor_CodAu).HasColumnName("Autor_CodAu");

            builder.HasOne(x => x.Livro)
                .WithMany(x => x.Autores)
                .HasForeignKey(x => x.Livro_Codl)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Autor)
                .WithMany(x => x.Livros)
                .HasForeignKey(x => x.Autor_CodAu)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class LivroAssuntoConfiguration : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> builder)
        {
            builder.ToTable("Livro_Assunto");

            builder.HasKey(x => new { x.Livro_Codl, x.Assunto_CodAs });

            builder.Property(x => x.Livro_Codl).HasColumnName("Livro_Codl");
            builder.Property(x => x.Assunto_CodAs).HasColumnName("Assunto_CodAs");

            builder.HasOne(x => x.Livro)
                .WithMany(x => x.Assuntos)
                .HasForeignKey(x => x.Livro_Codl)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Assunto)
                .WithMany(x => x.Livros)
                .HasForeignKey(x => x.Assunto_CodAs)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }


    public class FormaCompraConfiguration : IEntityTypeConfiguration<FormaCompra>
    {
        public void Configure(EntityTypeBuilder<FormaCompra> builder)
        {
            builder.ToTable("Forma_Compra");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Descricao).HasColumnName("Descricao").HasMaxLength(30).IsRequired();
        }
    }


    public class LivroPrecoConfiguration : IEntityTypeConfiguration<LivroPreco>
    {
        public void Configure(EntityTypeBuilder<LivroPreco> builder)
        {
            builder.ToTable("Livro_Preco");

            builder.HasKey(x => new { x.Livro_Codl, x.FormaCompra_Id });

            builder.Property(x => x.Livro_Codl).HasColumnName("Livro_Codl");
            builder.Property(x => x.FormaCompra_Id).HasColumnName("FormaCompra_Id");

            builder.Property(x => x.Valor)
                .HasColumnName("Valor")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(x => x.Livro)
                .WithMany(x => x.Precos)
                .HasForeignKey(x => x.Livro_Codl)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FormaCompra)
                .WithMany(x => x.Precos)
                .HasForeignKey(x => x.FormaCompra_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
