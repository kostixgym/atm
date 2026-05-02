using Lab5.Domain.OperationHistories;
using Lab5.Domain.ValueObjects.AccountIds;

namespace Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryOperationHistoryRepositories;

public interface IInMemoryOperationHistoryRepository
{
    IReadOnlyCollection<OperationHistory> GetByAccountId(AccountId accountId);

    void SaveOperationHistory(OperationHistory operationHistory);
}