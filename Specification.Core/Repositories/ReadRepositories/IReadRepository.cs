using System.Linq.Expressions;
using Utility.Core.Models;
using Utility.Core.Results;

namespace FoundationKit.Specification.Core.Repositories.ReadRepositories;

public interface IReadRepository
{

    Task<ApplicationResult<IEnumerable<TEntity>>> AsQueryable<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryExp, Pagging pagging = default!) where TEntity : class;
    Task<ApplicationResult<IEnumerable<TResponse>>> AsQueryable<TEntity, TResponse>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryExp, Expression<Func<TEntity, TResponse>> selector, Pagging pagging = default!) where TEntity : class;
    Task<ApplicationResult<TEntity>> AsQueryableSingle<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryExp) where TEntity : class;
    Task<ApplicationResult<TResponse>> AsQueryableSingle<TEntity, TResponse>(Func<IQueryable<TEntity>, IQueryable<TResponse>> queryExp) where TEntity : class;
    Task<ApplicationResult<TResponse>> FirstOrDefaultAsync<TEntity, TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResponse>> selector) where TEntity : class;
    Task<ApplicationResult<TEntity>> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    Task<ApplicationResult<bool>> IsDuplicateAsync<TEntity>(List<(Expression<Func<TEntity, object>> propertySelector, object value)> properties, Expression<Func<TEntity, bool>> idSelector) where TEntity : class;
    Task<ApplicationResult<bool>> IsDuplicateAsync<TEntity>(List<(Expression<Func<TEntity, object>> propertySelector, object value)> properties) where TEntity : class;
    Task<ApplicationResult<IEnumerable<TEntity>>> ToListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagging pagging = default!) where TEntity : class;
    Task<ApplicationResult<IEnumerable<TResponse>>> ToListAsync<TEntity, TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResponse>> selector, Pagging pagging = default!) where TEntity : class;
    Task<ApplicationResult<IEnumerable<TResponse>>> ToListAsync<TEntity, TResponse>(Expression<Func<TEntity, TResponse>> selector, Pagging pagging = default!) where TEntity : class;
    Task<ApplicationResult<IEnumerable<TEntity>>> ToListAsync<TEntity>(Pagging pagging = default!) where TEntity : class;
}
