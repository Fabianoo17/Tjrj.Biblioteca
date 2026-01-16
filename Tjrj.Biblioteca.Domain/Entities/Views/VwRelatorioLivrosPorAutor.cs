using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjrj.Biblioteca.Domain.Entities.Views
{
    public class VwRelatorioLivrosPorAutor
    {
        public int CodAu { get; set; }
        public string AutorNome { get; set; } = string.Empty;

        public int Codl { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; } = string.Empty;

        public string Assuntos { get; set; } = string.Empty;
    }
}
