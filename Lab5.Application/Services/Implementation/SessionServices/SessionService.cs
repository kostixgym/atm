using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryAccountRepositories;
using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemorySessionRepositories;
using Lab5.Application.Contracts.CreateSessionResults;
using Lab5.Application.Contracts.Services.SessionServices;
using Lab5.Application.SessionFactories.Implementation.AdminSessionFactories;
using Lab5.Application.SessionFactories.Implementation.UserSessionFactories;
using Lab5.Domain.Sessions.Interfaces;
using Lab5.Domain.ValueObjects.AccountIds;
using Lab5.Domain.ValueObjects.PinCodes;

namespace Lab5.Application.Services.Implementation.SessionServices;

public class SessionService : ISessionService
{
    private readonly IInMemorySessionRepository _sessionRepository;
    private readonly IInMemoryAccountRepository _accountRepository;
    private readonly string _systemPassword;

    public SessionService(
        IInMemorySessionRepository sessionRepository,
        IInMemoryAccountRepository accountRepository,
        string systemPassword)
    {
        _sessionRepository = sessionRepository;
        _accountRepository = accountRepository;
        _systemPassword = systemPassword;
    }

    public CreateSessionResult CreateUserSession(int accountId, string pinCode)
    {
        try
        {
            var accountIdValue = new AccountId(accountId);
            var pinCodeValue = new PinCode(pinCode);

            var factory = new UserSessionFactory(_accountRepository, accountIdValue, pinCodeValue);
            ISession session = factory.CreateSession();
            _sessionRepository.SaveSession(session);
            return new CreateSessionResult.Success(session.SessionId);
        }
        catch (InvalidOperationException)
        {
            return new CreateSessionResult.Failure();
        }
    }

    public CreateSessionResult CreateAdminSession(string providedPassword)
    {
        try
        {
            var factory = new AdminSessionFactory(providedPassword, _systemPassword);
            ISession session = factory.CreateSession();
            _sessionRepository.SaveSession(session);
            return new CreateSessionResult.Success(session.SessionId);
        }
        catch (InvalidOperationException)
        {
            return new CreateSessionResult.Failure();
        }
    }

    public ISession? ValidateSession(Guid sessionId)
    {
        return _sessionRepository.GetSessionById(sessionId);
    }

    public void DeleteSession(Guid sessionId)
    {
        _sessionRepository.DeleteSession(sessionId);
    }
}