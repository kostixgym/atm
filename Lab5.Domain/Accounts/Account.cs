using Lab5.Domain.OperationResults;
using Lab5.Domain.ValueObjects.AccountIds;
using Lab5.Domain.ValueObjects.Finances;
using Lab5.Domain.ValueObjects.PinCodes;

namespace Lab5.Domain.Accounts;

public class Account
{
    public AccountId AccountId { get; private set; }

    public Money Money { get; private set; }

    public PinCode PinCode { get; private set; }

    public Account(AccountId accountId, Money money, PinCode pinCode)
    {
        AccountId = accountId;
        Money = money;
        PinCode = pinCode;
    }

    public OperationResult Withdraw(Money amount)
    {
        if (amount.Value <= 0)
        {
            return new OperationResult.Failure();
        }

        if (Money.Value < amount.Value)
        {
            return new OperationResult.Failure();
        }

        Money = new Money(Money.Value - amount.Value);
        return new OperationResult.Success();
    }

    public OperationResult Deposit(Money amount)
    {
        if (amount.Value <= 0)
        {
            return new OperationResult.Failure();
        }

        Money = new Money(Money.Value + amount.Value);
        return new OperationResult.Success();
    }

    public bool VerifyPinCode(string pinCode)
    {
        return PinCode.Compare(pinCode);
    }
}