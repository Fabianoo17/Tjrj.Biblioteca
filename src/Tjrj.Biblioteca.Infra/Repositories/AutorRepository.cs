using Microsoft.EntityFrameworkCore;
using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Contexts;
using Tjrj.Biblioteca.Infra.Repositories.Base;

namespace Tjrj.Biblioteca.Infra.Repositories
{
    public class AutorRepository : RepositoryBase<Autor>, IAutorRepository
    {
        public AutorRepository(BibliotecaDbContext context) : base(context) { }

        public async Task<Autor?> GetByIdAsync(int codAu, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<Autor> query = _context.Autores;
            if (asNoTracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x => x.CodAu == codAu, cancellationToken);
        }
    }
}
