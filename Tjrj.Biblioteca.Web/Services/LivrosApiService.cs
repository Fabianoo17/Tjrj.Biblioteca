using Tjrj.Biblioteca.Application.Dtos.Livros;
namespace Tjrj.Biblioteca.Web.Services
{
    public class LivrosApiService
    {
        private readonly ApiClient _api;

        public LivrosApiService(ApiClient api) => _api = api;

        public Task<List<LivroListItemDto>?> GetAllAsync(CancellationToken ct = default)
            => _api.GetAsync<List<LivroListItemDto>>("api/livros", ct);

        public Task<LivroDetailsDto?> GetByIdAsync(int codl, CancellationToken ct = default)
            => _api.GetAsync<LivroDetailsDto>($"api/livros/{codl}", ct);

        public Task<ApiResponse<int>> CreateAsync(LivroCreateDto dto, CancellationToken ct = default)
            => _api.PostAsync<int>("api/livros", dto, ct);

        public Task<ApiResponse<bool>> UpdateAsync(int codl, LivroCreateDto dto, CancellationToken ct = default)
            => _api.PutAsync<bool>($"api/livros/{codl}", dto, ct);

        public Task<ApiResponse<object>> DeleteAsync(int codl, CancellationToken ct = default)
            => _api.DeleteAsync($"api/livros/{codl}", ct);
    }
}
