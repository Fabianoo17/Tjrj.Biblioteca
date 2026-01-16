using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.Livros;

namespace Tjrj.Biblioteca.Application.Interfaces
{


    public interface ILivroService
    {

        Task<ServiceResult<List<LivroListItemDto>>> GetAllAsync(CancellationToken ct = default);
        Task<ServiceResult<LivroDetailsDto>> GetByIdAsync(int codl, CancellationToken ct = default);
        Task<ServiceResult<int>> CreateAsync(LivroCreateDto dto, CancellationToken ct = default);
        Task<ServiceResult<bool>> UpdateAsync(LivroUpdateDto dto, CancellationToken ct = default);
        Task<ServiceResult<bool>> DeleteAsync(int codl, CancellationToken ct = default);
    }
}
