using Tjrj.Biblioteca.Application.Commom;
using Microsoft.AspNetCore.Mvc;
using Tjrj.Biblioteca.Application.Dtos.Assuntos;
using Tjrj.Biblioteca.Application.Interfaces;

namespace Tjrj.Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssuntosController : ControllerBase
{
    private readonly IAssuntoService _service;

    public AssuntosController(IAssuntoService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AssuntoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _service.GetAllAsync(ct);
        return this.ToActionResult(result);
    }

    [HttpGet("{codAs:int}")]
    [ProducesResponseType(typeof(AssuntoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int codAs, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(codAs, ct);
        return this.ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] AssuntoCreateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await _service.CreateAsync(dto, ct);

        if (result.Success)
            return CreatedAtAction(nameof(GetById), new { codAs = result.Data }, result.Data);

        return this.ToActionResult(result);
    }

    [HttpPut("{codAs:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update([FromRoute] int codAs, [FromBody] AssuntoCreateDto body, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var dto = new AssuntoUpdateDto
        {
            CodAs = codAs,
            Descricao = body.Descricao
        };

        var result = await _service.UpdateAsync(dto, ct);
        return this.ToActionResult(result);
    }

    [HttpDelete("{codAs:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete([FromRoute] int codAs, CancellationToken ct)
    {
        var result = await _service.DeleteAsync(codAs, ct);
        return this.ToActionResult(result);
    }
}
