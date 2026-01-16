using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjrj.Biblioteca.Application.Dtos
{

    public class LivroPrecoDto
    {
        public int FormaCompraId { get; set; }
        public decimal Valor { get; set; }
    }

    public class LivroCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; } = string.Empty;

        public List<int> AutorIds { get; set; } = new();
        public List<int> AssuntoIds { get; set; } = new();
        public List<LivroPrecoDto> Precos { get; set; } = new();
    }

    public class LivroUpdateDto : LivroCreateDto
    {
        public int Codl { get; set; }
    }
}
