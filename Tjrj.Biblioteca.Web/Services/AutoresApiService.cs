using Tjrj.Biblioteca.Application.Dtos.Autores;
namespace Tjrj.Biblioteca.Web.Services
{
    public class AutoresApiService
    {
        private readonly ApiClient _api;

        public AutoresApiService(ApiClient api) => _api = api;

        public Task<List<AutorDto>?> GetAllAsync(CancellationToken ct = default)
            => _api.GetAsync<List<AutorDto>>("api/autores", ct);

        public Task<AutorDto?> GetByIdAsync(int id, CancellationToken ct = default)
            => _api.GetAsync<AutorDto>($"api/autores/{id}", ct);

        public Task<ApiResponse<int>> CreateAsync(AutorCreateDto dto, CancellationToken ct = default)
            => _api.PostAsync<int>("api/autores", dto, ct);

        public Task<ApiResponse<bool>> UpdateAsync(int id, AutorCreateDto dto, CancellationToken ct = default)
            => _api.PutAsync<bool>($"api/autores/{id}", dto, ct);

        public Task<ApiResponse<object>> DeleteAsync(int id, CancellationToken ct = default)
            => _api.DeleteAsync($"api/autores/{id}", ct);
    }
}
