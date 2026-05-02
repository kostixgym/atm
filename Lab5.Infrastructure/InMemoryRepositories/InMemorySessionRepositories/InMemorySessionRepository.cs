using Lab5.Application.Abstractions.IInMemoryRepositories.IInMemorySessionRepositories;
using Lab5.Domain.Sessions.Interfaces;

namespace Lab5.Infrastructure.InMemoryRepositories.InMemorySessionRepositories;

public class InMemorySessionRepository : IInMemorySessionRepository
{
    private readonly Dictionary<Guid, ISession> _sessions = new();

    public void SaveSession(ISession session)
    {
        _sessions[session.SessionId] = session;
    }

    public void DeleteSession(Guid sessionId)
    {
        _sessions.Remove(sessionId);
    }

    public ISession? GetSessionById(Guid sessionId)
    {
        return _sessions.GetValueOrDefault(sessionId);
    }
}