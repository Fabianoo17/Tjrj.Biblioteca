using Tjrj.Biblioteca.Application.Commom;
using Tjrj.Biblioteca.Application.Dtos.FormaCompra;
using Tjrj.Biblioteca.Application.Interfaces;
using Tjrj.Biblioteca.Infra.Repositories;

namespace Tjrj.Biblioteca.Application.Services
{
    public class FormaCompraService : IFormaCompraService
    {
        private readonly IFormaCompraRepository _repo;

        public FormaCompraService(IFormaCompraRepository repo) => _repo = repo;

        public async Task<ServiceResult<List<FormaCompraDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var formas = await _repo.GetListAsync(cancellationToken: ct);

            var dto = formas
                .OrderBy(x => x.Descricao)
                .Select(x => new FormaCompraDto { Id = x.Id, Descricao = x.Descricao })
                .ToList();

            return ServiceResult<List<FormaCompraDto>>.Ok(dto);
        }
    }



}
