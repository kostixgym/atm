namespace Lab5.Domain.ValueObjects.AccountIds;

public record AccountId
{
    public int Value { get; }

    public AccountId(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("AccountId can not be negative");
        }

        Value = value;
    }
}