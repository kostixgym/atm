using Lab5.Domain.ValueObjects.Finances;

namespace Lab5.Application.Contracts.BalanceResults;

public abstract record BalanceResult
{
    private BalanceResult() { }

    public sealed record Success(Money Balance) : BalanceResult;

    public sealed record Failure() : BalanceResult;
}