using MediatR;

namespace NoteTakingApp.Application.Commands;

public record DeleteNoteCommand : IRequest
{
    public required string Id { get; init; }
}