using Microsoft.AspNetCore.Mvc.Rendering;
using Tjrj.Biblioteca.Application.Dtos.Livros;

namespace Tjrj.Biblioteca.Web.Models
{
    public class LivroFormVm
    {
        // dados do livro
        public int? Codl { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; } = string.Empty;

        // seleções
        public List<int> AutorIds { get; set; } = new();
        public List<int> AssuntoIds { get; set; } = new();

        // preços (vamos trabalhar com lista livre)
        public List<LivroPrecoDto> Precos { get; set; } = new();

        // combos
        public List<SelectListItem> Autores { get; set; } = new();
        public List<SelectListItem> Assuntos { get; set; } = new();
        public List<SelectListItem> FormasCompra { get; set; } = new();

    }
}
