using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryAccountRepositories;
using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemorySessionRepositories;
using Lab5.Application.Contracts.Services.AccountServices;
using Lab5.Application.Contracts.Services.SessionServices;
using Lab5.Application.Services.Implementation.AccountServices;
using Lab5.Application.Services.Implementation.SessionServices;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Application.ServiceCollectionExtensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string systemPassword)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ISessionService>(provider =>
        {
            IInMemorySessionRepository sessionRepository = provider.GetRequiredService<IInMemorySessionRepository>();
            IInMemoryAccountRepository accountRepository = provider.GetRequiredService<IInMemoryAccountRepository>();
            return new SessionService(sessionRepository, accountRepository, systemPassword);
        });

        return services;
    }
}