using Microsoft.AspNetCore.Mvc;
using Tjrj.Biblioteca.Application.Dtos.Livros;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Application.Commom;

namespace Tjrj.Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _service;

    public RelatoriosController(IRelatorioService service)
    {
        _service = service;
    }

    [HttpGet("livros-por-autor")]
    [ProducesResponseType(typeof(List<LivrosPorAutorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> LivrosPorAutor(CancellationToken ct)
    {
        var result = await _service.LivrosPorAutorAsync(ct);
        return this.ToActionResult(result);
    }
}
