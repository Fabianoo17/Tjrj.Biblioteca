using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tjrj.Biblioteca.Infra.Contexts;

namespace Tjrj.Biblioteca.Infra.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class
    {
        protected readonly BibliotecaDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(BibliotecaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<List<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            if (include is not null)
                query = include(query);

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.ToListAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => _dbSet.AnyAsync(predicate, cancellationToken);

        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => _dbSet.AddAsync(entity, cancellationToken).AsTask();

        public void Update(TEntity entity)
            => _dbSet.Update(entity);

        public void Remove(TEntity entity)
            => _dbSet.Remove(entity);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
