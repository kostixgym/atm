using Lab5.Domain.OperationTypes;
using Lab5.Domain.ValueObjects.AccountIds;
using Lab5.Domain.ValueObjects.Finances;

namespace Lab5.Domain.OperationHistories;

public class OperationHistory
{
    public Guid OperationId { get; private set; }

    public AccountId AccountId { get; private set; }

    public OperationType OperationType { get; private set; }

    public Money Amount { get; private set; }

    public Money BalanceAfter { get; private set; }

    private OperationHistory(AccountId accountId, OperationType operationType, Money amount, Money balanceAfter)
    {
        AccountId = accountId;
        OperationType = operationType;
        Amount = amount;
        BalanceAfter = balanceAfter;
        OperationId = Guid.NewGuid();
    }

    public static OperationHistory CreateWithdrawal(AccountId accountNumber, Money amount, Money balanceAfter)
    {
        return new OperationHistory(accountNumber, OperationType.Withdrawal, amount, balanceAfter);
    }

    public static OperationHistory CreateDeposit(AccountId accountNumber, Money amount, Money balanceAfter)
    {
        return new OperationHistory(accountNumber, OperationType.Deposit, amount, balanceAfter);
    }

    public static OperationHistory CreateBalanceCheck(AccountId accountNumber, Money balance)
    {
        return new OperationHistory(accountNumber, OperationType.BalanceCheck, new Money(0), balance);
    }
}