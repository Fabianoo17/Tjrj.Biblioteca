using Tjrj.Biblioteca.Application.Dtos.Livros;
namespace Tjrj.Biblioteca.Web.Services
{
    public class RelatoriosApiService
    {
        private readonly ApiClient _api;

        public RelatoriosApiService(ApiClient api) => _api = api;

        public Task<List<LivrosPorAutorDto>?> LivrosPorAutorAsync(CancellationToken ct = default)
            => _api.GetAsync<List<LivrosPorAutorDto>>("api/relatorios/livros-por-autor", ct);
    }
}
