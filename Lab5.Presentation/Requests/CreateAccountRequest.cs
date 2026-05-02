namespace Lab5.Presentation.Requests;

public record CreateAccountRequest
{
    public required int AccountId { get; init; }

    public required string PinCode { get; init; }

    public required decimal InitialBalance { get; init; }
}