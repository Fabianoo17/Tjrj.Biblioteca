namespace Tjrj.Biblioteca.Domain.Entities
{
    public class LivroAssunto
    {
        public int Livro_Codl { get; set; }
        public Livro? Livro { get; set; }

        public int Assunto_CodAs { get; set; }
        public Assunto? Assunto { get; set; }
    }
}
