using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryAccountRepositories;
using Lab5.Domain.Accounts;
using Lab5.Domain.ValueObjects.AccountIds;

namespace Lab5.Infrastructure.InMemoryRepositories.InMemoryAccountRepositories;

public class InMemoryAccountRepository : IInMemoryAccountRepository
{
    private readonly Dictionary<AccountId, Account> _accounts = new();

    public Account? GetAccountById(AccountId accountId)
    {
        return _accounts.GetValueOrDefault(accountId);
    }

    public void SaveAccount(Account account)
    {
        _accounts[account.AccountId] = account;
    }

    public bool Exists(AccountId accountId)
    {
        return _accounts.ContainsKey(accountId);
    }
}