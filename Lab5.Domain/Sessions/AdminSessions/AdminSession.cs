using Lab5.Domain.Sessions.Interfaces;
using Lab5.Domain.ValueObjects.AccountIds;

namespace Lab5.Domain.Sessions.AdminSessions;

public class AdminSession : ISession
{
    public bool IsAdmin()
    {
        return true;
    }

    public bool IsUser()
    {
        return false;
    }

    public AdminSession()
    {
        SessionId = Guid.NewGuid();
    }

    public Guid SessionId { get; }

    public AccountId? AccountId => null;
}