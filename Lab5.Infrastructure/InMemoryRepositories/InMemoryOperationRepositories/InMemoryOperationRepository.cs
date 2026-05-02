using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryOperationHistoryRepositories;
using Lab5.Domain.OperationHistories;
using Lab5.Domain.ValueObjects.AccountIds;

namespace Lab5.Infrastructure.InMemoryRepositories.InMemoryOperationRepositories;

public class InMemoryOperationRepository : IInMemoryOperationHistoryRepository
{
    private readonly List<OperationHistory> _history = new();

    public IReadOnlyCollection<OperationHistory> GetByAccountId(AccountId accountId)
    {
        return _history.Where(history => history.AccountId == accountId).ToList();
    }

    public void SaveOperationHistory(OperationHistory operationHistory)
    {
        _history.Add(operationHistory);
    }
}