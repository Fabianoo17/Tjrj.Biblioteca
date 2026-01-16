using Tjrj.Biblioteca.Application.Dtos.Assuntos;
using Tjrj.Biblioteca.Application.Dtos.Autores;

namespace Tjrj.Biblioteca.Application.Dtos.Livros
{
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


    public class LivroListItemDto
    {
        public int Codl { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; } = string.Empty;
    }

    public class LivroDetailsDto
    {
        public int Codl { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; } = string.Empty;

        public List<AutorDto> Autores { get; set; } = new();
        public List<AssuntoDto> Assuntos { get; set; } = new();
        public List<LivroPrecoDto> Precos { get; set; } = new();
    }
}
