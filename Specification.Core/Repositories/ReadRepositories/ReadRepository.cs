using Microsoft.EntityFrameworkCore;
using Specification.Core.Extensions;
using Specification.Core.Models;
using System.Linq.Expressions;
using Utility.Core.Models;
using Utility.Core.Results;

namespace FoundationKit.Specification.Core.Repositories.ReadRepositories;
public class ReadRepository(DbContext dbContext, EFConfiguration options) : IReadRepository
{
    public DbContext DbContext { get; } = dbContext;
    public EFConfiguration Configuration { get; } = options;

    public async Task<ApplicationResult<TEntity>> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
        where TEntity : class
    {
        try
        {
            var respone = await AsNoTracking<TEntity>().FirstOrDefaultAsync(predicate);

            return respone!;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<TResponse>> FirstOrDefaultAsync<TEntity, TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResponse>> selector)
         where TEntity : class
    {
        try
        {
            var respone = await AsNoTracking<TEntity>().Where(predicate).Select(selector).FirstOrDefaultAsync();

            return respone!;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<IEnumerable<TResponse>>> ToListAsync<TEntity, TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResponse>> selector, Pagging pagging = default!)
         where TEntity : class
    {
        try
        {
            var query = AsNoTracking<TEntity>().Where(predicate);

            int? count = null!;

            if (pagging is not null)
            {
                count = await query.CountAsync(predicate);

                query = query.Pagging(pagging);
            }

            var respone = await query.Select(selector).ToListAsync();

            count ??= respone.Count;

            return new PagedResult<IEnumerable<TResponse>>(respone, count.Value, pagging?.CurrentPage, pagging?.PageSize);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<IEnumerable<TEntity>>> ToListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagging pagging = default!)
      where TEntity : class
    {
        try
        {
            var query = AsNoTracking<TEntity>().Where(predicate);

            int? count = null!;

            if (pagging is not null)
            {
                count = await query.CountAsync(predicate);

                query = query.Pagging(pagging);
            }

            var respone = await query.ToListAsync();

            count ??= respone.Count;

            return new PagedResult<IEnumerable<TEntity>>(respone, count.Value, pagging?.CurrentPage, pagging?.PageSize);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    public async Task<ApplicationResult<IEnumerable<TEntity>>> ToListAsync<TEntity>(Pagging pagging = default!)
      where TEntity : class
    {
        try
        {
            var query = AsNoTracking<TEntity>();

            int? count = null!;

            if (pagging is not null)
            {
                count = await query.CountAsync();

                query = query.Pagging(pagging);
            }

            var respone = await query.ToListAsync();

            count ??= respone.Count;

            return new PagedResult<IEnumerable<TEntity>>(respone, count.Value, pagging?.CurrentPage, pagging?.PageSize);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<IEnumerable<TResponse>>> ToListAsync<TEntity, TResponse>(Expression<Func<TEntity, TResponse>> selector, Pagging pagging = default!)
        where TEntity : class
    {
        try
        {
            var query = AsNoTracking<TEntity>();

            int? count = null!;

            if (pagging is not null)
            {
                count = await query.CountAsync();

                query = query.Pagging(pagging);
            }

            var respone = await query.Select(selector).ToListAsync();

            count ??= respone.Count;

            return new PagedResult<IEnumerable<TResponse>>(respone, count.Value, pagging?.CurrentPage, pagging?.PageSize);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    public async Task<ApplicationResult<IEnumerable<TEntity>>> AsQueryable<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryExp, Pagging pagging = default!)
      where TEntity : class
    {
        try
        {
            var query = queryExp(AsNoTracking<TEntity>());

            int? count = null!;

            if (pagging is not null)
            {
                count = await query.CountAsync();

                query = query.Pagging(pagging);
            }

            var respone = await query.ToListAsync();

            count ??= respone.Count;

            return new PagedResult<IEnumerable<TEntity>>(respone, count.Value, pagging?.CurrentPage, pagging?.PageSize);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<IEnumerable<TResponse>>> AsQueryable<TEntity, TResponse>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryExp, Expression<Func<TEntity, TResponse>> selector, Pagging pagging = default!)
      where TEntity : class
    {
        try
        {
            var result = new ApplicationResult<IEnumerable<TEntity>>();

            var query = queryExp(AsNoTracking<TEntity>());

            int? count = null!;

            if (pagging is not null)
            {
                count = await query.CountAsync();

                query = query.Pagging(pagging);
            }

            var respone = await query.Select(selector).ToListAsync();

            count ??= respone.Count;

            return new PagedResult<IEnumerable<TResponse>>(respone, count.Value, pagging?.CurrentPage, pagging?.PageSize);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    public async Task<ApplicationResult<TEntity>> AsQueryableSingle<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryExp)
      where TEntity : class
    {
        try
        {
            var query = queryExp(AsNoTracking<TEntity>());

            var respone = await query.FirstOrDefaultAsync();

            return respone!;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<TResponse>> AsQueryableSingle<TEntity, TResponse>(Func<IQueryable<TEntity>, IQueryable<TResponse>> queryExp)
      where TEntity : class
    {
        try
        {
            var query = queryExp(AsNoTracking<TEntity>());

            var respone = await query.FirstOrDefaultAsync();

            return respone!;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<bool>> IsDuplicateAsync<TEntity>(
        List<(Expression<Func<TEntity, object>> propertySelector, object value)> properties)
        where TEntity : class
    {
        try
        {
            var parameter = Expression.Parameter(typeof(TEntity), "entity");

            // Initialize the combined OR expression to null
            Expression combinedExpression = null!;

            // Iterate through all property selectors and their corresponding values
            foreach (var (propertySelector, value) in properties)
            {
                // Convert the property to an object type
                var propertyExpression = Expression.Convert(Expression.Invoke(propertySelector, parameter), typeof(object));

                // Create the equality expression for the current property
                var equalityExpression = Expression.Equal(
                    propertyExpression,
                    Expression.Constant(value, typeof(object))
                );

                // Combine with OR operator
                combinedExpression = combinedExpression == null
                    ? (Expression)equalityExpression
                    : Expression.OrElse(combinedExpression, equalityExpression);
            }

            // Create the final expression as a lambda expression
            var finalExpression = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);

            var response = await DbContext.Set<TEntity>().AnyAsync(finalExpression);

            return response;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    public async Task<ApplicationResult<bool>> IsDuplicateAsync<TEntity>(
        List<(Expression<Func<TEntity, object>> propertySelector, object value)> properties,
        Expression<Func<TEntity, bool>> idSelector)
        where TEntity : class
    {
        try
        {


            var parameter = Expression.Parameter(typeof(TEntity), "entity");

            // Initialize the combined OR expression to null
            Expression combinedExpression = null!;

            // Iterate through all property selectors and their corresponding values
            foreach (var (propertySelector, value) in properties)
            {
                // Convert the property to an object type
                var propertyExpression = Expression.Convert(Expression.Invoke(propertySelector, parameter), typeof(object));

                // Create the equality expression for the current property
                var equalityExpression = Expression.Equal(
                    propertyExpression,
                    Expression.Constant(value, typeof(object))
                );

                // Combine with OR operator
                combinedExpression = combinedExpression == null
                    ? (Expression)equalityExpression
                    : Expression.OrElse(combinedExpression, equalityExpression);
            }

            // Combine the OR expression with the ID exclusion expression
            var finalExpression = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(combinedExpression, Expression.Not(Expression.Invoke(idSelector, parameter))),
                parameter
            );

            var respone = await DbContext.Set<TEntity>().AnyAsync(finalExpression);
            return respone;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    private IQueryable<TEntity> AsNoTracking<TEntity>() where TEntity : class
    {
        var dbSet = DbContext.Set<TEntity>();

        if (Configuration.UseAsNotTracking)
        {
            return dbSet.AsNoTracking();
        }

        return dbSet;
    }
}