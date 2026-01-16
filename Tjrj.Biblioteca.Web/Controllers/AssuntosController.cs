using Microsoft.AspNetCore.Mvc;
using Tjrj.Biblioteca.Application.Dtos.Assuntos;
using Tjrj.Biblioteca.Web.Services;

namespace Tjrj.Biblioteca.Web.Controllers;

public class AssuntosController : Controller
{
    private readonly AssuntosApiService _service;

    public AssuntosController(AssuntosApiService service) => _service = service;

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var assuntos = await _service.GetAllAsync(ct) ?? new List<AssuntoDto>();
        return View(assuntos);
    }

    public IActionResult Create() => View(new AssuntoCreateDto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AssuntoCreateDto dto, CancellationToken ct)
    {
        var result = await _service.CreateAsync(dto, ct);

        if (result.Success)
            return RedirectToAction(nameof(Index));

        ApplyApiErrorsToModelState(result);
        return View(dto);
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var assunto = await _service.GetByIdAsync(id, ct);
        if (assunto is null) return NotFound();

        return View(new AssuntoCreateDto { Descricao = assunto.Descricao });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AssuntoCreateDto dto, CancellationToken ct)
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
            TempData["Error"] = result.ErrorMessage ?? "Não foi possível remover o assunto.";

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
