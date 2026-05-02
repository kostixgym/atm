namespace Lab5.Domain.ValueObjects.PinCodes;

public record PinCode
{
    public string Value { get; }

    public PinCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("PinCode is empty");
        }

        if (value.Length != 4)
        {
            throw new ArgumentException("PinCode is not 4 digits long");
        }

        if (!value.All(char.IsDigit))
        {
            throw new ArgumentException("PinCode is not a digit");
        }

        Value = value;
    }

    public bool Compare(string value)
    {
        return Value == value;
    }
}