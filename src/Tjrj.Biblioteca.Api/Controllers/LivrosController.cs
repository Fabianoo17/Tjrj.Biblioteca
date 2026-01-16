using Microsoft.AspNetCore.Mvc;
using Tjrj.Biblioteca.Application.Dtos;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.Livros;

namespace Tjrj.Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly ILivroService _service;

    public LivrosController(ILivroService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria um livro com autores, assuntos e preços por forma de compra.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] LivroCreateDto dto, CancellationToken ct)
    {
        // FluentValidation roda automaticamente e preenche ModelState.
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await _service.CreateAsync(dto, ct);

        if (result.Success)
        {
            // Retorna 201 e aponta para um GET futuro (quando você criar o GetById)
            return CreatedAtAction(nameof(GetByIdPlaceholder), new { codl = result.Data }, result.Data);
        }

        return this.ToActionResult(result);
    }

    /// <summary>
    /// Atualiza um livro existente.
    /// </summary>
    [HttpPut("{codl:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update([FromRoute] int codl, [FromBody] LivroCreateDto body, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        // Como seu validator de Update usa Codl, montamos o DTO de update aqui
        var dto = new LivroUpdateDto
        {
            Codl = codl,
            Titulo = body.Titulo,
            Editora = body.Editora,
            Edicao = body.Edicao,
            AnoPublicacao = body.AnoPublicacao,
            AutorIds = body.AutorIds,
            AssuntoIds = body.AssuntoIds,
            Precos = body.Precos
        };

        var result = await _service.UpdateAsync(dto, ct);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Remove um livro existente.
    /// </summary>
    [HttpDelete("{codl:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete([FromRoute] int codl, CancellationToken ct)
    {
        var result = await _service.DeleteAsync(codl, ct);
        return this.ToActionResult(result);
    }

    // Placeholder apenas para o CreatedAtAction não quebrar.
    // Quando criarmos o GET /api/livros/{codl}, a gente substitui.
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("{codl:int}")]
    public IActionResult GetByIdPlaceholder(int codl) => Ok();
}
