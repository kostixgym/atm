using Lab5.Domain.Sessions.Interfaces;

namespace Lab5.Application.Abstractions.IInMemoryRepositories.IInMemorySessionRepositories;

public interface IInMemorySessionRepository
{
    void SaveSession(ISession session);

    void DeleteSession(Guid sessionId);

    ISession? GetSessionById(Guid sessionId);
}