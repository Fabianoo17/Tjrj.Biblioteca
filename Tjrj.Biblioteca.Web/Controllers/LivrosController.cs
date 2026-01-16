using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tjrj.Biblioteca.Application.Dtos.Assuntos;
using Tjrj.Biblioteca.Application.Dtos.Autores;
using Tjrj.Biblioteca.Application.Dtos.Livros;
using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Web.Models;
using Tjrj.Biblioteca.Web.Services;

namespace Tjrj.Biblioteca.Web.Controllers;

public class LivrosController : Controller
{
    private readonly LivrosApiService _livros;
    private readonly AutoresApiService _autores;
    private readonly AssuntosApiService _assuntos;
    private readonly FormasCompraApiService _formasCompra;

    public LivrosController(
        LivrosApiService livros,
        AutoresApiService autores,
        AssuntosApiService assuntos,
        FormasCompraApiService formasCompra)
    {
        _livros = livros;
        _autores = autores;
        _assuntos = assuntos;
        _formasCompra = formasCompra;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var lista = await _livros.GetAllAsync(ct) ?? new List<LivroListItemDto>();
        return View(lista);
    }

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        var vm = new LivroFormVm();
        await LoadCombos(vm, ct);
        vm.Edicao = 1;
        vm.AnoPublicacao = DateTime.Now.Year.ToString();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LivroFormVm vm, CancellationToken ct)
    {
        await LoadCombos(vm, ct);

        var dto = new LivroCreateDto
        {
            Titulo = vm.Titulo,
            Editora = vm.Editora,
            Edicao = vm.Edicao,
            AnoPublicacao = vm.AnoPublicacao,
            AutorIds = vm.AutorIds,
            AssuntoIds = vm.AssuntoIds,
            Precos = vm.Precos ?? new List<LivroPrecoDto>()
        };

        var result = await _livros.CreateAsync(dto, ct);

        if (result.Success)
            return RedirectToAction(nameof(Index));

        ApplyApiErrorsToModelState(result);
        return View(vm);
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var livro = await _livros.GetByIdAsync(id, ct);
        if (livro is null) return NotFound();

        var vm = new LivroFormVm
        {
            Codl = livro.Codl,
            Titulo = livro.Titulo,
            Editora = livro.Editora,
            Edicao = livro.Edicao,
            AnoPublicacao = livro.AnoPublicacao,
            AutorIds = livro.Autores.Select(a => a.CodAu).ToList(),
            AssuntoIds = livro.Assuntos.Select(s => s.CodAs).ToList(),
            Precos = livro.Precos
        };

        await LoadCombos(vm, ct);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, LivroFormVm vm, CancellationToken ct)
    {
        await LoadCombos(vm, ct);

        var dto = new LivroCreateDto
        {
            Titulo = vm.Titulo,
            Editora = vm.Editora,
            Edicao = vm.Edicao,
            AnoPublicacao = vm.AnoPublicacao,
            AutorIds = vm.AutorIds,
            AssuntoIds = vm.AssuntoIds,
            Precos = vm.Precos ?? new List<LivroPrecoDto>()
        };

        var result = await _livros.UpdateAsync(id, dto, ct);

        if (result.Success)
            return RedirectToAction(nameof(Index));

        ApplyApiErrorsToModelState(result);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _livros.DeleteAsync(id, ct);

        if (!result.Success)
            TempData["Error"] = result.ErrorMessage ?? "Não foi possível remover o livro.";

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCombos(LivroFormVm vm, CancellationToken ct)
    {
        var autores = await _autores.GetAllAsync(ct) ?? new List<AutorDto>();
        vm.Autores = autores
            .OrderBy(a => a.Nome)
            .Select(a => new SelectListItem(a.Nome, a.CodAu.ToString()))
            .ToList();

        var assuntos = await _assuntos.GetAllAsync(ct) ?? new List<AssuntoDto>();
        vm.Assuntos = assuntos
            .OrderBy(a => a.Descricao)
            .Select(a => new SelectListItem(a.Descricao, a.CodAs.ToString()))
            .ToList();
        vm.Precos = vm.Precos ?? new List<LivroPrecoDto> { new() };

        var formas = await _formasCompra.GetAllAsync(ct) ?? new();
        vm.FormasCompra = formas
            .OrderBy(f => f.Descricao)
            .Select(f => new SelectListItem(f.Descricao, f.Id.ToString()))
            .ToList();

    }

    private void ApplyApiErrorsToModelState<T>(ApiResponse<T> result)
    {
        if (result.ValidationErrors is not null)
        {
            foreach (var kv in result.ValidationErrors)
                foreach (var msg in kv.Value)
                    ModelState.AddModelError(kv.Key, msg);
            return;
        }

        if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
    }
}
