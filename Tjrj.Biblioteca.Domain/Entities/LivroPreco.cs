namespace Tjrj.Biblioteca.Domain.Entities
{
    public class LivroPreco
    {
        public int Livro_Codl { get; set; }
        public Livro? Livro { get; set; }

        public int FormaCompra_Id { get; set; }
        public FormaCompra? FormaCompra { get; set; }

        public decimal Valor { get; set; }
    }
}
