using Microsoft.AspNetCore.Mvc;
using Tjrj.Biblioteca.Application.Dtos.Livros;
using Tjrj.Biblioteca.Web.Services;

namespace Tjrj.Biblioteca.Web.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly RelatoriosApiService _service;

        public RelatoriosController(RelatoriosApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(LivrosPorAutor));

        [HttpGet]
        public async Task<IActionResult> LivrosPorAutor(CancellationToken ct)
        {
            var rows = await _service.LivrosPorAutorAsync(ct) ?? new List<LivrosPorAutorDto>();
            return View(rows);
        }
    }
}
