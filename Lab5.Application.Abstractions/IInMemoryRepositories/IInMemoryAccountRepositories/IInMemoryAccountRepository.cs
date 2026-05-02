using Lab5.Domain.Accounts;
using Lab5.Domain.ValueObjects.AccountIds;

namespace Lab5.Application.Abstractions.IInMemoryRepositories.IInMemoryAccountRepositories;

public interface IInMemoryAccountRepository
{
    Account? GetAccountById(AccountId accountId);

    void SaveAccount(Account account);

    bool Exists(AccountId accountId);
}