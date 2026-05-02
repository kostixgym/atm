using Lab5.Application.Contracts.CreateSessionResults;
using Lab5.Domain.Sessions.Interfaces;

namespace Lab5.Application.Contracts.Services.SessionServices;

public interface ISessionService
{
    CreateSessionResult CreateUserSession(int accountId, string pinCode);

    CreateSessionResult CreateAdminSession(string providedPassword);

    ISession? ValidateSession(Guid sessionId);

    void DeleteSession(Guid sessionId);
}