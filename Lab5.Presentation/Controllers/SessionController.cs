using Lab5.Application.Contracts.CreateSessionResults;
using Lab5.Application.Contracts.Services.SessionServices;
using Lab5.Presentation.Requests;
using Lab5.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Presentation.Controllers;

[ApiController]
[Route("api/sessions")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("user")]
    public ActionResult<SessionResponse> CreateUserSession([FromBody] CreateUserSessionRequest request)
    {
        CreateSessionResult result = _sessionService.CreateUserSession(request.AccountId, request.PinCode);

        return result switch
        {
            CreateSessionResult.Success success => Ok(new SessionResponse { SessionId = success.SessionId }),
            CreateSessionResult.Failure => Unauthorized(),
            _ => throw new System.Diagnostics.UnreachableException(),
        };
    }

    [HttpPost("admin")]
    public ActionResult<SessionResponse> CreateAdminSession([FromBody] CreateAdminSessionRequest request)
    {
        CreateSessionResult result = _sessionService.CreateAdminSession(request.Password);

        return result switch
        {
            CreateSessionResult.Success success => Ok(new SessionResponse { SessionId = success.SessionId }),
            CreateSessionResult.Failure => Unauthorized(),
            _ => throw new System.Diagnostics.UnreachableException(),
        };
    }
}