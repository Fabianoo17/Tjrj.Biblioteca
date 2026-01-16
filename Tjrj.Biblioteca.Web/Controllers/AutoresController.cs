using Microsoft.AspNetCore.Mvc;
using Tjrj.Biblioteca.Application.Dtos.Autores;
using Tjrj.Biblioteca.Web.Services;

namespace Tjrj.Biblioteca.Web.Controllers;

public class AutoresController : Controller
{
    private readonly AutoresApiService _service;

    public AutoresController(AutoresApiService service) => _service = service;

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var autores = await _service.GetAllAsync(ct) ?? new List<AutorDto>();
        return View(autores);
    }

    public IActionResult Create() => View(new AutorCreateDto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AutorCreateDto dto, CancellationToken ct)
    {
        var result = await _service.CreateAsync(dto, ct);

        if (result.Success)
            return RedirectToAction(nameof(Index));

        ApplyApiErrorsToModelState(result);
        return View(dto);
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var autor = await _service.GetByIdAsync(id, ct);
        if (autor is null) return NotFound();

        return View(new AutorCreateDto { Nome = autor.Nome });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AutorCreateDto dto, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, dto, ct);

        if (result.Success)
            return RedirectToAction(nameof(Index));

        ApplyApiErrorsToModelState(result);
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _service.DeleteAsync(id, ct);

        if (!result.Success)
            TempData["Error"] = result.ErrorMessage ?? "Não foi possível remover o autor.";

        return RedirectToAction(nameof(Index));
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
