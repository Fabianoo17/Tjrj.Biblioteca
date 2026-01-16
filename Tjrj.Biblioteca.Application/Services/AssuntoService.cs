using Microsoft.EntityFrameworkCore;
using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.Assuntos;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Repositories;

namespace Tjrj.Biblioteca.Application.Services
{
    public class AssuntoService : IAssuntoService
    {
        private readonly IAssuntoRepository _repo;

        public AssuntoService(IAssuntoRepository repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResult<List<AssuntoDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var assuntos = await _repo.GetListAsync(
                predicate: null,
                include: null,
                asNoTracking: true,
                cancellationToken: ct);

            var dto = assuntos
                .OrderBy(a => a.Descricao)
                .Select(a => new AssuntoDto { CodAs = a.CodAs, Descricao = a.Descricao })
                .ToList();

            return ServiceResult<List<AssuntoDto>>.Ok(dto);
        }

        public async Task<ServiceResult<AssuntoDto>> GetByIdAsync(int codAs, CancellationToken ct = default)
        {
            var assunto = await _repo.GetFirstOrDefaultAsync(x=>x.CodAs == codAs);
            if (assunto is null)
                return ServiceResult<AssuntoDto>.Fail(Errors.NotFound, "Assunto não encontrado.");

            return ServiceResult<AssuntoDto>.Ok(new AssuntoDto
            {
                CodAs = assunto.CodAs,
                Descricao = assunto.Descricao
            });
        }

        public async Task<ServiceResult<int>> CreateAsync(AssuntoCreateDto dto, CancellationToken ct = default)
        {
            var descricao = dto.Descricao.Trim();

            // regra opcional (boa): impedir duplicidade
            var existe = await _repo.AnyAsync(a => a.Descricao == descricao, ct);
            if (existe)
                return ServiceResult<int>.Fail(Errors.Conflict, "Já existe um assunto com essa descrição.");

            var assunto = new Assunto { Descricao = descricao };

            try
            {
                await _repo.AddAsync(assunto, ct);
                await _repo.SaveChangesAsync(ct);
                return ServiceResult<int>.Ok(assunto.CodAs);
            }
            catch (DbUpdateException)
            {
                return ServiceResult<int>.Fail(Errors.Conflict, "Não foi possível salvar o assunto por conflito de dados no banco.");
            }
        }

        public async Task<ServiceResult<bool>> UpdateAsync(AssuntoUpdateDto dto, CancellationToken ct = default)
        {
            var assunto = await _repo.GetFirstOrDefaultAsync(x => x.CodAs == dto.CodAs);
            if (assunto is null)
                return ServiceResult<bool>.Fail(Errors.NotFound, "Assunto não encontrado.");

            var descricao = dto.Descricao.Trim();

            // impedir duplicidade ignorando ele mesmo
            var existe = await _repo.AnyAsync(a => a.Descricao == descricao && a.CodAs != dto.CodAs, ct);
            if (existe)
                return ServiceResult<bool>.Fail(Errors.Conflict, "Já existe um assunto com essa descrição.");

            assunto.Descricao = descricao;

            try
            {
                _repo.Update(assunto);
                await _repo.SaveChangesAsync(ct);
                return ServiceResult<bool>.Ok(true);
            }
            catch (DbUpdateException)
            {
                return ServiceResult<bool>.Fail(Errors.Conflict, "Não foi possível atualizar o assunto por conflito de dados no banco.");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int codAs, CancellationToken ct = default)
        {
            var assunto = await _repo.GetFirstOrDefaultAsync(x => x.CodAs == codAs);
            if (assunto is null)
                return ServiceResult<bool>.Fail(Errors.NotFound, "Assunto não encontrado.");

            try
            {
                _repo.Remove(assunto);
                await _repo.SaveChangesAsync(ct);
                return ServiceResult<bool>.Ok(true);
            }
            catch (DbUpdateException)
            {
                return ServiceResult<bool>.Fail(Errors.Conflict, "Não foi possível remover o assunto pois existem livros relacionados.");
            }
        }
    }



}
