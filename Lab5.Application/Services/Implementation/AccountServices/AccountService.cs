using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryAccountRepositories;
using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryOperationHistoryRepositories;
using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemorySessionRepositories;
using Lab5.Application.Contracts.BalanceResults;
using Lab5.Application.Contracts.Services.AccountServices;
using Lab5.Domain.Accounts;
using Lab5.Domain.OperationHistories;
using Lab5.Domain.OperationResults;
using Lab5.Domain.Sessions.Interfaces;
using Lab5.Domain.ValueObjects.AccountIds;
using Lab5.Domain.ValueObjects.Finances;
using Lab5.Domain.ValueObjects.PinCodes;

namespace Lab5.Application.Services.Implementation.AccountServices;

public class AccountService : IAccountService
{
    private readonly IInMemoryAccountRepository _accountRepository;
    private readonly IInMemoryOperationHistoryRepository _operationHistoryRepository;
    private readonly IInMemorySessionRepository _sessionRepository;

    public AccountService(
        IInMemoryAccountRepository accountRepository,
        IInMemoryOperationHistoryRepository operationHistoryRepository,
        IInMemorySessionRepository sessionRepository)
    {
        _accountRepository = accountRepository;
        _operationHistoryRepository = operationHistoryRepository;
        _sessionRepository = sessionRepository;
    }

    public OperationResult CreateAccount(int accountId, string pinCode, decimal initialBalance)
    {
        try
        {
            var accountIdValue = new AccountId(accountId);
            var pinCodeValue = new PinCode(pinCode);
            var initialBalanceValue = new Money(initialBalance);

            if (_accountRepository.Exists(accountIdValue))
            {
                return new OperationResult.Failure();
            }

            var account = new Account(accountIdValue, initialBalanceValue, pinCodeValue);
            _accountRepository.SaveAccount(account);

            return new OperationResult.Success();
        }
        catch (ArgumentException)
        {
            return new OperationResult.Failure();
        }
    }

    public BalanceResult GetBalance(Guid sessionId)
    {
        ISession? session = _sessionRepository.GetSessionById(sessionId);

        if (session == null || !session.IsUser() || session.AccountId == null)
        {
            return new BalanceResult.Failure();
        }

        Account? account = _accountRepository.GetAccountById(session.AccountId);

        if (account == null)
        {
            return new BalanceResult.Failure();
        }

        Money balance = account.Money;
        var operationHistory = OperationHistory.CreateBalanceCheck(account.AccountId, balance);
        _operationHistoryRepository.SaveOperationHistory(operationHistory);

        return new BalanceResult.Success(balance);
    }

    public OperationResult Withdraw(Guid sessionId, decimal amount)
    {
        try
        {
            var moneyAmount = new Money(amount);
            ISession? session = _sessionRepository.GetSessionById(sessionId);

            if (session == null || !session.IsUser() || session.AccountId == null)
            {
                return new OperationResult.Failure();
            }

            Account? account = _accountRepository.GetAccountById(session.AccountId);

            if (account == null)
            {
                return new OperationResult.Failure();
            }

            OperationResult result = account.Withdraw(moneyAmount);

            if (result is OperationResult.Success)
            {
                _accountRepository.SaveAccount(account);
                var operationHistory = OperationHistory.CreateWithdrawal(session.AccountId, moneyAmount, account.Money);
                _operationHistoryRepository.SaveOperationHistory(operationHistory);
            }

            return result;
        }
        catch (ArgumentException)
        {
            return new OperationResult.Failure();
        }
    }

    public OperationResult Deposit(Guid sessionId, decimal amount)
    {
        try
        {
            var moneyAmount = new Money(amount);
            ISession? session = _sessionRepository.GetSessionById(sessionId);

            if (session == null || !session.IsUser() || session.AccountId == null)
            {
                return new OperationResult.Failure();
            }

            Account? account = _accountRepository.GetAccountById(session.AccountId);

            if (account == null)
            {
                return new OperationResult.Failure();
            }

            OperationResult result = account.Deposit(moneyAmount);

            if (result is OperationResult.Success)
            {
                _accountRepository.SaveAccount(account);
                var operationHistory = OperationHistory.CreateDeposit(session.AccountId, moneyAmount, account.Money);
                _operationHistoryRepository.SaveOperationHistory(operationHistory);
            }

            return result;
        }
        catch (ArgumentException)
        {
            return new OperationResult.Failure();
        }
    }

    public IReadOnlyCollection<OperationHistory>? GetOperationHistory(Guid sessionId)
    {
        ISession? session = _sessionRepository.GetSessionById(sessionId);

        if (session == null || !session.IsUser() || session.AccountId == null)
        {
            return null;
        }

        return _operationHistoryRepository.GetByAccountId(session.AccountId);
    }
}