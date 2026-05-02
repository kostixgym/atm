using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryAccountRepositories;
using Lab5.Application.Abstractions.SessionFactories.Interfaces;
using Lab5.Domain.Accounts;
using Lab5.Domain.Sessions.Interfaces;
using Lab5.Domain.Sessions.UserSessions;
using Lab5.Domain.ValueObjects.AccountIds;
using Lab5.Domain.ValueObjects.PinCodes;

namespace Lab5.Application.SessionFactories.Implementation.UserSessionFactories;

public class UserSessionFactory : ISessionFactory
{
    private readonly IInMemoryAccountRepository _accountRepository;
    private readonly PinCode _pinCode;
    private readonly AccountId _accountId;

    public UserSessionFactory(IInMemoryAccountRepository accountRepository, AccountId accountId, PinCode pinCode)
    {
        _accountRepository = accountRepository;
        _accountId = accountId;
        _pinCode = pinCode;
    }

    public ISession CreateSession()
    {
        Account? account = _accountRepository.GetAccountById(_accountId);

        if (account == null)
        {
            throw new InvalidOperationException("Account not found");
        }

        if (!account.VerifyPinCode(_pinCode.Value))
        {
            throw new InvalidOperationException("Invalid pin code");
        }

        return new UserSession(_accountId);
    }
}