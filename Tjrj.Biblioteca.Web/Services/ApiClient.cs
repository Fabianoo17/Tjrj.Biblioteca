namespace Tjrj.Biblioteca.Web.Services
{
    public class ApiClient
    {
        private readonly IHttpClientFactory _factory;

        public ApiClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        private HttpClient Client => _factory.CreateClient("BibliotecaApi");

        public async Task<T?> GetAsync<T>(string url, CancellationToken ct = default)
            => await Client.GetFromJsonAsync<T>(url, ct);

        public async Task<ApiResponse<T>> PostAsync<T>(string url, object body, CancellationToken ct = default)
        {
            var response = await Client.PostAsJsonAsync(url, body, ct);
            return await ApiResponse<T>.FromHttpResponse(response, ct);
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string url, object body, CancellationToken ct = default)
        {
            var response = await Client.PutAsJsonAsync(url, body, ct);
            return await ApiResponse<T>.FromHttpResponse(response, ct);
        }

        public async Task<ApiResponse<object>> DeleteAsync(string url, CancellationToken ct = default)
        {
            var response = await Client.DeleteAsync(url, ct);
            return await ApiResponse<object>.FromHttpResponse(response, ct);
        }
    }
}
