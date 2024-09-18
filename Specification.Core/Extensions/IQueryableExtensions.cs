using Utility.Core.Models;

namespace Specification.Core.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> Pagging<TEntity>(this IQueryable<TEntity> query, Pagging pagging)
    {
        return query.Skip(pagging.Skip).Take(pagging.Take);
    }
}
