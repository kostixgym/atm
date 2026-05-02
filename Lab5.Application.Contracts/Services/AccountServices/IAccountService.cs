using Lab5.Application.Contracts.BalanceResults;
using Lab5.Domain.OperationHistories;
using Lab5.Domain.OperationResults;

namespace Lab5.Application.Contracts.Services.AccountServices;

public interface IAccountService
{
    OperationResult CreateAccount(int accountId, string pinCode, decimal initialBalance);

    BalanceResult GetBalance(Guid sessionId);

    OperationResult Withdraw(Guid sessionId, decimal amount);

    OperationResult Deposit(Guid sessionId, decimal amount);

    IReadOnlyCollection<OperationHistory>? GetOperationHistory(Guid sessionId);
}