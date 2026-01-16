using Tjrj.Biblioteca.Application.Dtos.FormaCompra;
namespace Tjrj.Biblioteca.Web.Services
{
    public class FormasCompraApiService
    {
        private readonly ApiClient _api;
        public FormasCompraApiService(ApiClient api) => _api = api;

        public Task<List<FormaCompraDto>?> GetAllAsync(CancellationToken ct = default)
            => _api.GetAsync<List<FormaCompraDto>>("api/formascompra", ct);
    }
}
