using Lab5.Domain.Sessions.Interfaces;

namespace Lab5.Application.Abstractions.SessionFactories.Interfaces;

public interface ISessionFactory
{
    ISession CreateSession();
}