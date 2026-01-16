using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.Livros;

namespace Tjrj.Biblioteca.Application.Interfaces
{
    public interface IRelatorioService
    {
        Task<ServiceResult<List<LivrosPorAutorDto>>> LivrosPorAutorAsync(CancellationToken ct = default);
    }
}
