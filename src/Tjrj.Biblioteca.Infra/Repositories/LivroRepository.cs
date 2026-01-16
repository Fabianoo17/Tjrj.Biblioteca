using Microsoft.EntityFrameworkCore;
using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Contexts;
using Tjrj.Biblioteca.Infra.Repositories.Base;

namespace Tjrj.Biblioteca.Infra.Repositories
{
    public class LivroRepository : RepositoryBase<Livro>, ILivroRepository
    {
        public LivroRepository(BibliotecaDbContext context) : base(context) { }

        public async Task<Livro?> GetByIdAsync(int codl, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<Livro> query = _context.Livros
                .Include(x => x.Autores).ThenInclude(x => x.Autor)
                .Include(x => x.Assuntos).ThenInclude(x => x.Assunto)
                .Include(x => x.Precos).ThenInclude(x => x.FormaCompra);

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Codl == codl, cancellationToken);
        }

        public async Task<List<Livro>> GetAllWithDetailsAsync(bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<Livro> query = _context.Livros
                .Include(x => x.Autores).ThenInclude(x => x.Autor)
                .Include(x => x.Assuntos).ThenInclude(x => x.Assunto)
                .Include(x => x.Precos).ThenInclude(x => x.FormaCompra);

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.OrderBy(x => x.Titulo).ToListAsync(cancellationToken);
        }
    }
}
