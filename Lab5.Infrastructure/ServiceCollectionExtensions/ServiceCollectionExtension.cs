using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryAccountRepositories;
using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryOperationHistoryRepositories;
using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemorySessionRepositories;
using Lab5.Infrastructure.InMemoryRepositories.InMemoryAccountRepositories;
using Lab5.Infrastructure.InMemoryRepositories.InMemoryOperationRepositories;
using Lab5.Infrastructure.InMemoryRepositories.InMemorySessionRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Infrastructure.ServiceCollectionExtensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IInMemoryAccountRepository, InMemoryAccountRepository>();
        services.AddSingleton<IInMemoryOperationHistoryRepository, InMemoryOperationRepository>();
        services.AddSingleton<IInMemorySessionRepository, InMemorySessionRepository>();

        return services;
    }
}