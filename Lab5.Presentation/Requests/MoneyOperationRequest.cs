namespace Lab5.Presentation.Requests;

public record MoneyOperationRequest
{
    public required decimal Amount { get; init; }
}