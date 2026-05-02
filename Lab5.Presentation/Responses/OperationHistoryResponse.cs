namespace Lab5.Presentation.Responses;

public record OperationHistoryResponse(
    Guid OperationId,
    int AccountId,
    string OperationType,
    decimal Amount,
    decimal BalanceAfter);
