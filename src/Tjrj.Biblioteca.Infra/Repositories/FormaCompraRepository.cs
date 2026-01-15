using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Contexts;
using Tjrj.Biblioteca.Infra.Repositories.Base;

namespace Tjrj.Biblioteca.Infra.Repositories
{
    public class FormaCompraRepository : RepositoryBase<FormaCompra>, IFormaCompraRepository
    {
        public FormaCompraRepository(BibliotecaDbContext context) : base(context)
        {
        }
    }
}
