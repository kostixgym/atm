using Lab5.Application.Contracts.BalanceResults;
using Lab5.Application.Contracts.Services.AccountServices;
using Lab5.Application.Contracts.Services.SessionServices;
using Lab5.Domain.OperationHistories;
using Lab5.Domain.OperationResults;
using Lab5.Domain.Sessions.Interfaces;
using Lab5.Presentation.Requests;
using Lab5.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Presentation.Controllers;

[ApiController]
[Route("api/accounts")]
public sealed class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ISessionService _sessionService;

    public AccountController(IAccountService accountService, ISessionService sessionService)
    {
        _accountService = accountService;
        _sessionService = sessionService;
    }

    [HttpPost]
    public ActionResult CreateAccount([FromHeader(Name = "SessionId")] Guid sessionId, [FromBody] CreateAccountRequest request)
    {
        ISession? session = _sessionService.ValidateSession(sessionId);
        if (session == null || !session.IsAdmin())
        {
            return Unauthorized();
        }

        OperationResult result = _accountService.CreateAccount(request.AccountId, request.PinCode, request.InitialBalance);

        return result switch
        {
            OperationResult.Success => Ok(),
            OperationResult.Failure => BadRequest(),
            _ => throw new System.Diagnostics.UnreachableException(),
        };
    }

    [HttpGet("balance")]
    public ActionResult<BalanceResponse> GetBalance([FromHeader(Name = "SessionId")] Guid sessionId)
    {
        BalanceResult result = _accountService.GetBalance(sessionId);

        return result switch
        {
            BalanceResult.Success success => Ok(new BalanceResponse(success.Balance.Value)),
            BalanceResult.Failure => Unauthorized(),
            _ => throw new System.Diagnostics.UnreachableException(),
        };
    }

    [HttpPost("deposit")]
    public ActionResult Deposit([FromHeader(Name = "SessionId")] Guid sessionId, [FromBody] MoneyOperationRequest request)
    {
        OperationResult result = _accountService.Deposit(sessionId, request.Amount);

        return result switch
        {
            OperationResult.Success => Ok(),
            OperationResult.Failure => BadRequest(),
            _ => throw new System.Diagnostics.UnreachableException(),
        };
    }

    [HttpPost("withdraw")]
    public ActionResult Withdraw([FromHeader(Name = "SessionId")] Guid sessionId, [FromBody] MoneyOperationRequest request)
    {
        OperationResult result = _accountService.Withdraw(sessionId, request.Amount);

        return result switch
        {
            OperationResult.Success => Ok(),
            OperationResult.Failure => BadRequest(),
            _ => throw new System.Diagnostics.UnreachableException(),
        };
    }

    [HttpGet("history")]
    public ActionResult<IEnumerable<OperationHistoryResponse>> GetHistory([FromHeader(Name = "SessionId")] Guid sessionId)
    {
        IReadOnlyCollection<OperationHistory>? history = _accountService.GetOperationHistory(sessionId);

        if (history == null)
        {
            return Unauthorized();
        }

        IEnumerable<OperationHistoryResponse> response = history.Select(h => new OperationHistoryResponse(
            h.OperationId,
            h.AccountId.Value,
            h.OperationType.ToString(),
            h.Amount.Value,
            h.BalanceAfter.Value));

        return Ok(response);
    }
}