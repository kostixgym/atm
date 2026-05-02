using Lab5.Domain.ValueObjects.AccountIds;

namespace Lab5.Domain.Sessions.Interfaces;

public interface ISession
{
    bool IsAdmin();

    bool IsUser();

    Guid SessionId { get; }

    AccountId? AccountId { get; }
}