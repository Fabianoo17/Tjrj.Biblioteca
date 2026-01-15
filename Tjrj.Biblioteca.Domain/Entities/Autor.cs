namespace Tjrj.Biblioteca.Domain.Entities
{
    public class Autor
    {
        public int CodAu { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<LivroAutor> Livros { get; set; } = new List<LivroAutor>();
    }
}
