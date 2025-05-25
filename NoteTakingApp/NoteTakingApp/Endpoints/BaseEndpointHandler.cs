using System.Security.Claims;
using MediatR;
using NoteTakingApp.Application.Queries;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Endpoints;

public abstract class BaseEndpointHandler(IMediator mediator)
{
    protected readonly IMediator Mediator = mediator;

    protected async Task<bool> HasAccess(string userId, string projectId)
    {
        return await Mediator.Send(new HasAccessQuery { UserId = userId, ProjectId = projectId });
    }

    protected string GetUserId(ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("User not authenticated");
    }

    protected IResult HandleResult<T>(Result<T> result)
    {
        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
    }

    protected IResult HandleResult(Result result)
    {
        return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
    }
} 