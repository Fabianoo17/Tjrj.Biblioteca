using Microsoft.EntityFrameworkCore;
using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.Autores;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Repositories;

namespace Tjrj.Biblioteca.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _repo;

        public AutorService(IAutorRepository repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResult<List<AutorDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var autores = await _repo.GetListAsync(
                predicate: null,
                include: null,
                asNoTracking: true,
                cancellationToken: ct);

            var dto = autores
                .OrderBy(a => a.Nome)
                .Select(a => new AutorDto { CodAu = a.CodAu, Nome = a.Nome })
                .ToList();

            return ServiceResult<List<AutorDto>>.Ok(dto);
        }

        public async Task<ServiceResult<AutorDto>> GetByIdAsync(int codAu, CancellationToken ct = default)
        {
            var autor = await _repo.GetByIdAsync(codAu, asNoTracking: true, cancellationToken: ct);
            if (autor is null)
                return ServiceResult<AutorDto>.Fail(Errors.NotFound, "Autor não encontrado.");

            var dto = new AutorDto { CodAu = autor.CodAu, Nome = autor.Nome };
            return ServiceResult<AutorDto>.Ok(dto);
        }

        public async Task<ServiceResult<int>> CreateAsync(AutorCreateDto dto, CancellationToken ct = default)
        {
            // regra opcional (muito boa): impedir duplicidade por nome
            var nome = dto.Nome.Trim();
            var existe = await _repo.AnyAsync(a => a.Nome == nome, ct);
            if (existe)
                return ServiceResult<int>.Fail(Errors.Conflict, "Já existe um autor com esse nome.");

            var autor = new Autor { Nome = nome };

            try
            {
                await _repo.AddAsync(autor, ct);
                await _repo.SaveChangesAsync(ct);
                return ServiceResult<int>.Ok(autor.CodAu);
            }
            catch (DbUpdateException)
            {
                return ServiceResult<int>.Fail(Errors.Conflict, "Não foi possível salvar o autor por conflito de dados no banco.");
            }
        }

        public async Task<ServiceResult<bool>> UpdateAsync(AutorUpdateDto dto, CancellationToken ct = default)
        {
            var autor = await _repo.GetByIdAsync(dto.CodAu, asNoTracking: false, cancellationToken: ct);
            if (autor is null)
                return ServiceResult<bool>.Fail(Errors.NotFound, "Autor não encontrado.");

            var nome = dto.Nome.Trim();

            // impedir duplicidade por nome (ignorando ele mesmo)
            var existe = await _repo.AnyAsync(a => a.Nome == nome && a.CodAu != dto.CodAu, ct);
            if (existe)
                return ServiceResult<bool>.Fail(Errors.Conflict, "Já existe um autor com esse nome.");

            autor.Nome = nome;

            try
            {
                _repo.Update(autor);
                await _repo.SaveChangesAsync(ct);
                return ServiceResult<bool>.Ok(true);
            }
            catch (DbUpdateException)
            {
                return ServiceResult<bool>.Fail(Errors.Conflict, "Não foi possível atualizar o autor por conflito de dados no banco.");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int codAu, CancellationToken ct = default)
        {
            var autor = await _repo.GetByIdAsync(codAu, asNoTracking: false, cancellationToken: ct);
            if (autor is null)
                return ServiceResult<bool>.Fail(Errors.NotFound, "Autor não encontrado.");

            try
            {
                _repo.Remove(autor);
                await _repo.SaveChangesAsync(ct);
                return ServiceResult<bool>.Ok(true);
            }
            catch (DbUpdateException)
            {
                // geralmente ocorre se ele estiver relacionado a livros
                return ServiceResult<bool>.Fail(Errors.Conflict, "Não foi possível remover o autor pois existem livros relacionados.");
            }
        }
    }
}
