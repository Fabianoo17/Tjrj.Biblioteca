using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace Tjrj.Biblioteca.Web.Services
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public T? Data { get; private set; }
        public Dictionary<string, string[]>? ValidationErrors { get; private set; }
        public string? ErrorMessage { get; private set; }

        public static async Task<ApiResponse<T>> FromHttpResponse(HttpResponseMessage response, CancellationToken ct)
        {
            var result = new ApiResponse<T>
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                // quando API retorna "int" ou DTO
                if (response.Content.Headers.ContentLength > 0)
                    result.Data = await response.Content.ReadFromJsonAsync<T>(cancellationToken: ct);

                return result;
            }

            // Se for ValidationProblemDetails do ASP.NET
            try
            {
                var vpd = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(cancellationToken: ct);
                if (vpd?.Errors?.Count > 0)
                {
                    result.ValidationErrors = vpd.Errors.ToDictionary(k => k.Key, v => v.Value);
                    return result;
                }
            }
            catch { /* ignore */ }

            // Se API devolve { error = "..." }
            try
            {
                var errorObj = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>(cancellationToken: ct);
                if (errorObj is not null && errorObj.TryGetValue("error", out var msg))
                    result.ErrorMessage = msg;
            }
            catch { /* ignore */ }

            return result;
        }
    }
}
