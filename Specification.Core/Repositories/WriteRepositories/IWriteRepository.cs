using Microsoft.EntityFrameworkCore;
using Utility.Core.Results;

namespace FoundationKit.Specification.Core.Repositories.WriteRepositories;

public interface IWriteRepository
{
    DbContext DbContext { get; }
    Task<ApplicationResult<TEntity>> AddAsync<TEntity>(TEntity entity) where TEntity : class;
    void Attach<TEntity>(TEntity entity) where TEntity : class;
    Task<ApplicationResult<int>> Delete<TEntity>(TEntity entity) where TEntity : class;
    Task<ApplicationResult<int>> SaveChangesAsync();
}
