namespace Lab5.Domain.ValueObjects.Finances;

public record Money
{
    public decimal Value { get; }

    public Money(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Currency value cannot be negative");
        }

        Value = value;
    }
}