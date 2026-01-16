using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjrj.Biblioteca.Domain.Entities
{
    public class Livro
    {
        public int Codl { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; } = string.Empty;

        public ICollection<LivroAutor> Autores { get; set; } = new List<LivroAutor>();
        public ICollection<LivroAssunto> Assuntos { get; set; } = new List<LivroAssunto>();
        public ICollection<LivroPreco> Precos { get; set; } = new List<LivroPreco>();
    }
}
