using FoundationKit.Specification.Core.Repositories.WriteRepositories;
using Microsoft.EntityFrameworkCore;
using Utility.Core.Results;

namespace Specification.Core.Repositories.WriteRepositories;



public class WriteRepository : IWriteRepository
{

    public WriteRepository(DbContext dbContext)
    {
        DbContext = dbContext;
    }
    public DbContext DbContext { get; set; }

    public async Task<ApplicationResult<TEntity>> AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        try
        {
            var response = await DbContext.Set<TEntity>().AddAsync(entity);

            await DbContext.SaveChangesAsync();

            return response.Entity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public void Attach<TEntity>(TEntity entity)
       where TEntity : class
    {
        try
        {
            DbContext.Set<TEntity>().Attach(entity);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ApplicationResult<int>> Delete<TEntity>(TEntity entity) where TEntity : class
    {
        try
        {
            DbContext.Set<TEntity>().Remove(entity);

            var response = await DbContext.SaveChangesAsync();

            return response;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<int>> SaveChangesAsync()
    {
        try
        {
            var response = await DbContext.SaveChangesAsync();

            return response;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}