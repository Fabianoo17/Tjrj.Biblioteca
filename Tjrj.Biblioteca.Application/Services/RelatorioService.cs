using Microsoft.EntityFrameworkCore;
using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.Livros;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Infra.Contexts;

namespace Tjrj.Biblioteca.Application.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly BibliotecaDbContext _db;

        public RelatorioService(BibliotecaDbContext db)
        {
            _db = db;
        }

        public async Task<ServiceResult<List<LivrosPorAutorDto>>> LivrosPorAutorAsync(CancellationToken ct = default)
        {
            var rows = await _db.VwRelatorioLivrosPorAutor
                .AsNoTracking()
                .OrderBy(x => x.AutorNome)
                .ThenBy(x => x.Titulo)
                .Select(x => new LivrosPorAutorDto
                {
                    CodAu = x.CodAu,
                    AutorNome = x.AutorNome,
                    Codl = x.Codl,
                    Titulo = x.Titulo,
                    Editora = x.Editora,
                    Edicao = x.Edicao,
                    AnoPublicacao = x.AnoPublicacao,
                    Assuntos = x.Assuntos
                })
                .ToListAsync(ct);

            return ServiceResult<List<LivrosPorAutorDto>>.Ok(rows);
        }
    }



}
