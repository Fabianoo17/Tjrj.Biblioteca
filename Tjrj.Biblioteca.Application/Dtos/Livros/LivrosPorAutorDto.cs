namespace Tjrj.Biblioteca.Application.Dtos.Livros
{
    public class LivrosPorAutorDto
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
