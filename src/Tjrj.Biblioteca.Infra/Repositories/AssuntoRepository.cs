using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Contexts;
using Tjrj.Biblioteca.Infra.Repositories.Base;

namespace Tjrj.Biblioteca.Infra.Repositories
{
    public class AssuntoRepository : RepositoryBase<Assunto>, IAssuntoRepository
    {
        public AssuntoRepository(BibliotecaDbContext context) : base(context)
        {
        }
    }
}
