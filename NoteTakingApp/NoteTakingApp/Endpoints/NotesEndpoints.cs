using System.Security.Claims;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Application.Queries;
using NoteTakingApp.Domain.Common.ValueObjects;
using NoteTakingApp.Endpoints.Dtos;
using NoteTakingApp.Endpoints.ViewModels;

namespace NoteTakingApp.Endpoints;

public class NotesEndpointHandler : BaseEndpointHandler
{
    public NotesEndpointHandler(IMediator mediator) : base(mediator)
    {
    }

    public async Task<IResult> GetNotes(string projectId, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        if (!await HasAccess(userId, projectId))
        {
            return Results.Forbid();
        }
        
        var notes = await Mediator.Send(new GetNotesQuery { ProjectId = projectId });
        var res = notes.Select(async n =>
        {
            var paragraphs = await Mediator.Send(new GetParagraphsQuery
                { ParagraphIds = n.ParagraphIds.Select(p => p.Value).ToList() });
            var noteViewModel = n.Adapt<NoteViewModel>();
            return noteViewModel with { Paragraphs = paragraphs.Adapt<IList<ParagraphViewModel>>() };
        });
        return Results.Ok(await Task.WhenAll(res));
    }

    public async Task<IResult> CreateNote(CreateNoteDto dto, ClaimsPrincipal user)
    {
        var userId = GetUserId(user);
        if (!await HasAccess(userId, dto.ProjectId))
        {
            return Results.Forbid();
        }
        
        var command = dto.Adapt<CreateNoteCommand>();
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    public async Task<IResult> DuplicateNote(DuplicateNoteDto dto)
    {
        var command = dto.Adapt<DuplicateNoteCommand>();
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    public async Task<IResult> DeleteNote(string id)
    {
        await Mediator.Send(new DeleteNoteCommand { Id = id });
        return Results.Ok();
    }
}

public static class NotesEndpoints
{
    public static void AddNotesEndpoints(this WebApplication app)
    {
        var handler = new NotesEndpointHandler(app.Services.GetRequiredService<IMediator>());

        app.MapGet("/notes/{projectId}", [Authorize] async (string projectId, ClaimsPrincipal user) =>
            await handler.GetNotes(projectId, user)).WithTags("Notes");
        
        app.MapPost("/notes", [Authorize] async (CreateNoteDto dto, ClaimsPrincipal user) =>
            await handler.CreateNote(dto, user)).WithTags("Notes");
        
        app.MapPost("/notes/duplicate", [Authorize] async (DuplicateNoteDto dto) =>
            await handler.DuplicateNote(dto)).WithTags("Notes");
        
        app.MapDelete("/notes/{id}", [Authorize] async (string id) =>
            await handler.DeleteNote(id)).WithTags("Notes");
    }
}