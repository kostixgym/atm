namespace Lab5.Presentation.Requests;

public record CreateUserSessionRequest
{
    public required int AccountId { get; init; }

    public required string PinCode { get; init; }
}