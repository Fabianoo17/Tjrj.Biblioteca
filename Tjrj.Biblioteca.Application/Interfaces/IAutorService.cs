using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.Autores;

namespace Tjrj.Biblioteca.Application.Interfaces
{
    public interface IAutorService
    {
        Task<ServiceResult<List<AutorDto>>> GetAllAsync(CancellationToken ct = default);
        Task<ServiceResult<AutorDto>> GetByIdAsync(int codAu, CancellationToken ct = default);

        Task<ServiceResult<int>> CreateAsync(AutorCreateDto dto, CancellationToken ct = default);
        Task<ServiceResult<bool>> UpdateAsync(AutorUpdateDto dto, CancellationToken ct = default);
        Task<ServiceResult<bool>> DeleteAsync(int codAu, CancellationToken ct = default);
    }
}
