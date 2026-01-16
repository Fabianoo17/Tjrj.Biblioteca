using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.Assuntos;

namespace Tjrj.Biblioteca.Application.Interfaces
{
    public interface IAssuntoService
    {
        Task<ServiceResult<List<AssuntoDto>>> GetAllAsync(CancellationToken ct = default);
        Task<ServiceResult<AssuntoDto>> GetByIdAsync(int codAs, CancellationToken ct = default);

        Task<ServiceResult<int>> CreateAsync(AssuntoCreateDto dto, CancellationToken ct = default);
        Task<ServiceResult<bool>> UpdateAsync(AssuntoUpdateDto dto, CancellationToken ct = default);
        Task<ServiceResult<bool>> DeleteAsync(int codAs, CancellationToken ct = default);
    }
}
