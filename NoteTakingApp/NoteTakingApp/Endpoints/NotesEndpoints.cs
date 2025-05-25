using System.Security.Claims;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Application.Queries;
using NoteTakingApp.Endpoints.Dtos;
using NoteTakingApp.Endpoints.ViewModels;

namespace NoteTakingApp.Endpoints;

public static class NotesEndpoints
{
    public static void AddNotesEndpoints(this WebApplication app)
    {
        app.MapGet("/notes/{projectId}", [Authorize] async (string projectId, IMediator mediator, ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasAccess = await mediator.Send(new HasAccessQuery() { UserId = userId, ProjectId = projectId });

            if (!hasAccess)
            {
                return Results.Forbid();
            }
            
            var notes = await mediator.Send(new GetNotesQuery { ProjectId = projectId });
            var res =  notes.Select(async  n =>
            {
                var paragraphs = await mediator.Send(new GetParagraphsQuery { ParagraphIds = n.ParagraphIds });
                var noteViewModel = n.Adapt<NoteViewModel>();
                return noteViewModel with { Paragraphs = paragraphs.Adapt<IList<ParagraphViewModel>>() };
            });
            return Results.Ok(await Task.WhenAll(res));
        }).WithTags("Notes");
        
        app.MapPost("/notes", [Authorize] async (CreateNoteDto dto, IMediator mediator, ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasAccess = await mediator.Send(new HasAccessQuery() { UserId = userId, ProjectId = dto.ProjectId });

            if (!hasAccess)
            {
                return Results.Forbid();
            }
            
            var command = dto.Adapt<CreateNoteCommand>();
            var res = await mediator.Send(command);
            return res.IsSuccess ? Results.Ok(res) : Results.BadRequest(res.Error);
        }).WithTags("Notes");
        
        app.MapPost("/notes/duplicate", [Authorize] async (DuplicateNoteDto dto, IMediator mediator) =>
        {
            var command = dto.Adapt<DuplicateNoteCommand>();
            var res = await mediator.Send(command);
            return res.IsSuccess ? Results.Ok(res) : Results.BadRequest(res.Error);
        }).WithTags("Notes");
        
        app.MapDelete("/notes/{id}", [Authorize] async (string id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteNoteCommand() { Id = id });
            return Results.Ok();
        }).WithTags("Notes");
    }
}