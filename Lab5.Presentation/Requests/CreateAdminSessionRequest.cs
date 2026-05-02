namespace Lab5.Presentation.Requests;

public record CreateAdminSessionRequest
{
    public required string Password { get; init; }
}