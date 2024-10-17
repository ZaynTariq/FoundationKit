using FoundationKit.Specification.Core.Repositories.ReadRepositories;
using FoundationKit.Specification.Core.Repositories.WriteRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Specification.Core.Models;
using Specification.Core.Repositories.WriteRepositories;

namespace Specification.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFoundationKit<TDBContext>
        (this IServiceCollection services, Action<EFConfiguration> configCallback = default!)
    where TDBContext : DbContext
    {
        var configuration = EFConfiguration.Create();

        configCallback?.Invoke(configuration);

        services.AddScoped<IReadRepository, ReadRepository>(x =>
        {
            using var scope = x.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TDBContext>();
            return new ReadRepository(dbContext, configuration);
        });

        services.AddScoped<IWriteRepository, WriteRepository>(x =>
        {
            using var scope = x.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TDBContext>();
            return new WriteRepository(dbContext);
        });

        return services;
    }
}
