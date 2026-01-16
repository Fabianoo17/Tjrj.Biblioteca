using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Domain.Entities;
using Tjrj.Biblioteca.Infra.Repositories;

namespace Tjrj.Biblioteca.Application.Services
{

    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepo;
        private readonly IAutorRepository _autorRepo;
        private readonly IAssuntoRepository _assuntoRepo;
        private readonly IFormaCompraRepository _formaRepo;

        public LivroService(
            ILivroRepository livroRepo,
            IAutorRepository autorRepo,
            IAssuntoRepository assuntoRepo,
            IFormaCompraRepository formaRepo)
        {
            _livroRepo = livroRepo;
            _autorRepo = autorRepo;
            _assuntoRepo = assuntoRepo;
            _formaRepo = formaRepo;
        }

        public async Task<ServiceResult<int>> CreateAsync(LivroCreateDto dto, CancellationToken ct = default)
        {
            // (Opcional, mas recomendado) garantir que IDs existem:
            if (!await AutoresExistem(dto.AutorIds, ct))
                return ServiceResult<int>.Fail(Errors.Validation, "Um ou mais autores informados não existem.");

            if (dto.AssuntoIds.Any() && !await AssuntosExistem(dto.AssuntoIds, ct))
                return ServiceResult<int>.Fail(Errors.Validation, "Um ou mais assuntos informados não existem.");

            if (dto.Precos.Any() && !await FormasCompraExistem(dto.Precos.Select(p => p.FormaCompraId), ct))
                return ServiceResult<int>.Fail(Errors.Validation, "Uma ou mais formas de compra informadas não existem.");

            var livro = new Livro
            {
                Titulo = dto.Titulo.Trim(),
                Editora = dto.Editora.Trim(),
                Edicao = dto.Edicao,
                AnoPublicacao = dto.AnoPublicacao.Trim()
            };

            foreach (var autorId in dto.AutorIds.Distinct())
                livro.Autores.Add(new LivroAutor { Autor_CodAu = autorId });

            foreach (var assuntoId in dto.AssuntoIds.Distinct())
                livro.Assuntos.Add(new LivroAssunto { Assunto_CodAs = assuntoId });

            foreach (var p in dto.Precos)
                livro.Precos.Add(new LivroPreco { FormaCompra_Id = p.FormaCompraId, Valor = p.Valor });

            try
            {
                await _livroRepo.AddAsync(livro, ct);
                await _livroRepo.SaveChangesAsync(ct);
                return ServiceResult<int>.Ok(livro.Codl);
            }
            catch (DbUpdateException)
            {
                return ServiceResult<int>.Fail(Errors.Conflict, "Não foi possível salvar o livro por conflito de dados no banco.");
            }
        }

        public async Task<ServiceResult<bool>> UpdateAsync(LivroUpdateDto dto, CancellationToken ct = default)
        {
            var livro = await _livroRepo.GetByIdAsync(dto.Codl, asNoTracking: false, ct);
            if (livro is null)
                return ServiceResult<bool>.Fail(Errors.NotFound, "Livro não encontrado.");

            if (!await AutoresExistem(dto.AutorIds, ct))
                return ServiceResult<bool>.Fail(Errors.Validation, "Um ou mais autores informados não existem.");

            if (dto.AssuntoIds.Any() && !await AssuntosExistem(dto.AssuntoIds, ct))
                return ServiceResult<bool>.Fail(Errors.Validation, "Um ou mais assuntos informados não existem.");

            if (dto.Precos.Any() && !await FormasCompraExistem(dto.Precos.Select(p => p.FormaCompraId), ct))
                return ServiceResult<bool>.Fail(Errors.Validation, "Uma ou mais formas de compra informadas não existem.");

            livro.Titulo = dto.Titulo.Trim();
            livro.Editora = dto.Editora.Trim();
            livro.Edicao = dto.Edicao;
            livro.AnoPublicacao = dto.AnoPublicacao.Trim();

            // Recria relações (simples e seguro)
            livro.Autores.Clear();
            foreach (var autorId in dto.AutorIds.Distinct())
                livro.Autores.Add(new LivroAutor { Livro_Codl = livro.Codl, Autor_CodAu = autorId });

            livro.Assuntos.Clear();
            foreach (var assuntoId in dto.AssuntoIds.Distinct())
                livro.Assuntos.Add(new LivroAssunto { Livro_Codl = livro.Codl, Assunto_CodAs = assuntoId });

            livro.Precos.Clear();
            foreach (var p in dto.Precos)
                livro.Precos.Add(new LivroPreco { Livro_Codl = livro.Codl, FormaCompra_Id = p.FormaCompraId, Valor = p.Valor });

            try
            {
                _livroRepo.Update(livro);
                await _livroRepo.SaveChangesAsync(ct);
                return ServiceResult<bool>.Ok(true);
            }
            catch (DbUpdateException)
            {
                return ServiceResult<bool>.Fail(Errors.Conflict, "Não foi possível atualizar o livro por conflito de dados no banco.");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int codl, CancellationToken ct = default)
        {
            var livro = await _livroRepo.GetByIdAsync(codl, asNoTracking: false, ct);
            if (livro is null)
                return ServiceResult<bool>.Fail(Errors.NotFound, "Livro não encontrado.");

            try
            {
                _livroRepo.Remove(livro);
                await _livroRepo.SaveChangesAsync(ct);
                return ServiceResult<bool>.Ok(true);
            }
            catch (DbUpdateException)
            {
                return ServiceResult<bool>.Fail(Errors.Conflict, "Não foi possível remover o livro. Verifique dependências no banco.");
            }
        }

        private async Task<bool> AutoresExistem(IEnumerable<int> ids, CancellationToken ct)
        {
            var distinct = ids.Distinct().ToList();
            var autores = await _autorRepo.GetListAsync(a => distinct.Contains(a.CodAu), cancellationToken: ct);
            return autores.Count == distinct.Count;
        }

        private async Task<bool> AssuntosExistem(IEnumerable<int> ids, CancellationToken ct)
        {
            var distinct = ids.Distinct().ToList();
            var assuntos = await _assuntoRepo.GetListAsync(a => distinct.Contains(a.CodAs), cancellationToken: ct);
            return assuntos.Count == distinct.Count;
        }

        private async Task<bool> FormasCompraExistem(IEnumerable<int> ids, CancellationToken ct)
        {
            var distinct = ids.Distinct().ToList();
            var formas = await _formaRepo.GetListAsync(f => distinct.Contains(f.Id), cancellationToken: ct);
            return formas.Count == distinct.Count;
        }
    }
}
