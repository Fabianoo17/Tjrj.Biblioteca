using Microsoft.AspNetCore.Mvc;
using Tjrj.Biblioteca.Application.Dtos.Autores;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Application.Commom;

namespace Tjrj.Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutoresController : ControllerBase
{
    private readonly IAutorService _service;

    public AutoresController(IAutorService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AutorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _service.GetAllAsync(ct);
        return this.ToActionResult(result);
    }

    [HttpGet("{codAu:int}")]
    [ProducesResponseType(typeof(AutorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int codAu, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(codAu, ct);
        return this.ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] AutorCreateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await _service.CreateAsync(dto, ct);

        if (result.Success)
            return CreatedAtAction(nameof(GetById), new { codAu = result.Data }, result.Data);

        return this.ToActionResult(result);
    }

    [HttpPut("{codAu:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update([FromRoute] int codAu, [FromBody] AutorCreateDto body, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var dto = new AutorUpdateDto
        {
            CodAu = codAu,
            Nome = body.Nome
        };

        var result = await _service.UpdateAsync(dto, ct);
        return this.ToActionResult(result);
    }

    [HttpDelete("{codAu:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete([FromRoute] int codAu, CancellationToken ct)
    {
        var result = await _service.DeleteAsync(codAu, ct);
        return this.ToActionResult(result);
    }
}
