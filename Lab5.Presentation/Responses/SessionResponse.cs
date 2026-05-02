namespace Lab5.Presentation.Responses;

public record SessionResponse
{
    public required Guid SessionId { get; init; }
}