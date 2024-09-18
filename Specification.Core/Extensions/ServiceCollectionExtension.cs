using Microsoft.Extensions.DependencyInjection;

namespace Specification.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFoundationKit(this IServiceCollection services)
    {
        return services;
    }
}
