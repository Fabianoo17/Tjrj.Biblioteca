using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tjrj.Biblioteca.Infra.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
