using Lab5.Domain.Sessions.Interfaces;
using Lab5.Domain.ValueObjects.AccountIds;

namespace Lab5.Domain.Sessions.UserSessions;

public class UserSession : ISession
{
    public bool IsAdmin()
    {
        return false;
    }

    public bool IsUser()
    {
        return true;
    }

    public UserSession(AccountId accountId)
    {
        AccountId = accountId;
        SessionId = Guid.NewGuid();
    }

    public AccountId AccountId { get; private set; }

    public Guid SessionId { get; }
}