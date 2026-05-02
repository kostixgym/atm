namespace Lab5.Application.Contracts.CreateSessionResults;

public abstract record CreateSessionResult
{
    private CreateSessionResult() { }

    public sealed record Success(Guid SessionId) : CreateSessionResult;

    public sealed record Failure() : CreateSessionResult;
}