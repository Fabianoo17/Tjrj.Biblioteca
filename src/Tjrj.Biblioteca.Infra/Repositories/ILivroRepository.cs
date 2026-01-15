using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Repositories.Base;

namespace Tjrj.Biblioteca.Infra.Repositories
{
    public interface ILivroRepository : IRepositoryBase<Livro>
    {
        Task<Livro?> GetByIdAsync(int codl, bool asNoTracking = true, CancellationToken cancellationToken = default);
        Task<List<Livro>> GetAllWithDetailsAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}
