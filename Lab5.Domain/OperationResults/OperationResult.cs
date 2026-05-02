namespace Lab5.Domain.OperationResults;

public abstract record OperationResult
{
    private OperationResult() { }

    public sealed record Success() : OperationResult;

    public sealed record Failure() : OperationResult;
}