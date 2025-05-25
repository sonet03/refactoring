using System.Security.Claims;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Application.Queries;
using NoteTakingApp.Domain;
using NoteTakingApp.Endpoints.Dtos;

namespace NoteTakingApp.Endpoints;

public static class ProjectEndpoints
{
    public static void AddProjectEndpoints(this WebApplication app)
    {
        app.MapGet("/projects", async ([FromQuery] PrivacyLevel? privacyLevel, [FromQuery] string? nameSearch,
            IMediator mediator, ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var projects = await mediator.Send(new GetProjectsQuery
                { UserId = userId, PrivacyLevel = privacyLevel, NameSearch = nameSearch });
            return projects;
        }).WithTags("Projects");

        app.MapPost("/projects", [Authorize] async (CreateProjectDto dto, ClaimsPrincipal user, IMediator mediator) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = (userId, dto).Adapt<CreateProjectCommand>();
            var res = await mediator.Send(command);
            return res.IsSuccess ? Results.Ok(res.Value) : Results.BadRequest(res.Error);
        }).WithTags("Projects");
        
        app.MapPut("/projects", [Authorize] async (UpdateProjectDto dto, ClaimsPrincipal user, IMediator mediator) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = (userId, dto).Adapt<UpdateProjectCommand>();
            var res = await mediator.Send(command);

            return res.IsSuccess ? Results.Ok() : Results.BadRequest(res.Error);
        }).WithTags("Projects");
        
        app.MapDelete("/projects/{projectId}", [Authorize] async (string projectId, ClaimsPrincipal user, IMediator mediator) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new DeleteProjectCommand() { Id = projectId, UserId = userId };
            var res = await mediator.Send(command);

            return res.IsSuccess ? Results.Ok() : Results.BadRequest(res.Error);
        }).WithTags("Projects");
    }
}