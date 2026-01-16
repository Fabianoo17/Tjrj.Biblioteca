using Tjrj.Biblioteca.Application.Dtos.Livros;
namespace Tjrj.Biblioteca.Web.Services
{
    public interface ILivroApiService
    {

        Task CreateAsync(LivroCreateDto dto);
        Task UpdateAsync(Guid id, LivroUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
