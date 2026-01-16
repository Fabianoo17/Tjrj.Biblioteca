using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.FormaCompra;

namespace Tjrj.Biblioteca.Application.Interfaces
{
    public interface IFormaCompraService
    {
        Task<ServiceResult<List<FormaCompraDto>>> GetAllAsync(CancellationToken ct = default);
    }
}
