using Tjrj.Biblioteca.Application.Dtos.Assuntos;
namespace Tjrj.Biblioteca.Web.Services
{
    public class AssuntosApiService
    {
        private readonly ApiClient _api;

        public AssuntosApiService(ApiClient api) => _api = api;

        public Task<List<AssuntoDto>?> GetAllAsync(CancellationToken ct = default)
            => _api.GetAsync<List<AssuntoDto>>("api/assuntos", ct);

        public Task<AssuntoDto?> GetByIdAsync(int id, CancellationToken ct = default)
            => _api.GetAsync<AssuntoDto>($"api/assuntos/{id}", ct);

        public Task<ApiResponse<int>> CreateAsync(AssuntoCreateDto dto, CancellationToken ct = default)
            => _api.PostAsync<int>("api/assuntos", dto, ct);

        public Task<ApiResponse<bool>> UpdateAsync(int id, AssuntoCreateDto dto, CancellationToken ct = default)
            => _api.PutAsync<bool>($"api/assuntos/{id}", dto, ct);

        public Task<ApiResponse<object>> DeleteAsync(int id, CancellationToken ct = default)
            => _api.DeleteAsync($"api/assuntos/{id}", ct);
    }
}
