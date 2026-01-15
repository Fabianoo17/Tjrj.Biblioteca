namespace Tjrj.Biblioteca.Domain.Entities
{
    public class FormaCompra
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public ICollection<LivroPreco> Precos { get; set; } = new List<LivroPreco>();
    }
}
