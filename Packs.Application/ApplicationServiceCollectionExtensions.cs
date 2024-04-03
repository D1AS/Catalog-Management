using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Packs.Application.Repositories;
using Packs.Application.Services;

namespace Packs.Application;

// Install Microsoft.Extensions.DependencyInjection.Abstractions
// https://docs.fluentvalidation.net/en/latest/di.html
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IPackRepository, InMemPackRepository>();
        services.AddSingleton<IPackService, PackService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }
}
