using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Repositories.Base;

namespace Tjrj.Biblioteca.Infra.Repositories
{
    public interface IAutorRepository : IRepositoryBase<Autor>
    {
        Task<Autor?> GetByIdAsync(int codAu, bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}
