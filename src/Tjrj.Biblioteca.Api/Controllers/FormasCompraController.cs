using Microsoft.AspNetCore.Mvc;
using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.FormaCompra;
using Tjrj.Biblioteca.Application.Interfaces;

namespace Tjrj.Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormasCompraController : ControllerBase
{
    private readonly IFormaCompraService _service;

    public FormasCompraController(IFormaCompraService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(List<FormaCompraDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _service.GetAllAsync(ct);
        return this.ToActionResult(result);
    }
}
