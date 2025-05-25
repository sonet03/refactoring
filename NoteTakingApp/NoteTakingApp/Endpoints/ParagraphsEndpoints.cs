using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Endpoints.Dtos;

namespace NoteTakingApp.Endpoints;

public static class ParagraphsEndpoints
{
    public static void AddParagraphsEndpoints(this WebApplication app)
    {
        app.MapPost("/notes/{noteId}/paragraphs", [Authorize] async (string noteId, CreateParagraphDto dto, IMediator mediator) =>
        {
            var command = (noteId, dto).Adapt<CreateParagraphCommand>();
            var res = await mediator.Send(command);
            return res.IsSuccess ? Results.Ok(res.Value) : Results.BadRequest(res.Error);
        }).WithTags("Paragraphs");

        app.MapPut("/paragraphs/{paragraphId}", [Authorize]
            async (string paragraphId, UpdateParagraphDto dto, IMediator mediator) =>
            {
                var command = new UpdateParagraphCommand { Id = paragraphId, Content = dto.Content };
                var res = await mediator.Send(command);
                return res.IsSuccess ? Results.Ok() : Results.BadRequest(res.Error);
            }).WithTags("Paragraphs");
        
        app.MapDelete("/paragraphs/{paragraphId}", [Authorize] async (string paragraphId, IMediator mediator) =>
        {
            var command = new DeleteParagraphCommand { Id = paragraphId };
            var res = await mediator.Send(command);
            return res.IsSuccess ? Results.Ok() : Results.BadRequest(res.Error);
        }).WithTags("Paragraphs");
        
        app.MapPost("/paragraphs/connect", [Authorize] async (ConnectParagraphDto dto, IMediator mediator) =>
        {
            var command = dto.Adapt<ConnectParagraphCommand>();
            var res = await mediator.Send(command);
            return res.IsSuccess ? Results.Ok() : Results.BadRequest(res.Error);
        }).WithTags("Paragraphs");
        
        app.MapPost("/paragraphs/disconnect", [Authorize] async (DisconnectParagraphDto dto, IMediator mediator) =>
        {
            var command = dto.Adapt<DisconnectParagraphCommand>();
            var res = await mediator.Send(command);
            return res.IsSuccess ? Results.Ok() : Results.BadRequest(res.Error);
        }).WithTags("Paragraphs");
        
        app.MapPost("/paragraphs/duplicate", [Authorize] async (DuplicateParagraphDto dto, IMediator mediator) =>
        {
            var command = dto.Adapt<DuplicateParagraphCommand>();
            var res = await mediator.Send(command);
            return res.IsSuccess ? Results.Ok() : Results.BadRequest(res.Error);
        }).WithTags("Paragraphs");
    }
}