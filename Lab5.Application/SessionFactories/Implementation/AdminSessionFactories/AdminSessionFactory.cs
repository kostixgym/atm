using Lab5.Application.Abstractions.SessionFactories.Interfaces;
using Lab5.Domain.Sessions.AdminSessions;
using Lab5.Domain.Sessions.Interfaces;

namespace Lab5.Application.SessionFactories.Implementation.AdminSessionFactories;

public class AdminSessionFactory : ISessionFactory
{
    private readonly string _providedPassword;
    private readonly string _systemPassword;

    public AdminSessionFactory(string providedPassword, string systemPassword)
    {
        _providedPassword = providedPassword;
        _systemPassword = systemPassword;
    }

    public ISession CreateSession()
    {
        if (_providedPassword != _systemPassword)
        {
            throw new InvalidOperationException("Invalid admin password.");
        }

        return new AdminSession();
    }
}