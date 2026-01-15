namespace Tjrj.Biblioteca.Domain.Entities
{
    public class Assunto
    {
        public int CodAs { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public ICollection<LivroAssunto> Livros { get; set; } = new List<LivroAssunto>();
    }
}
