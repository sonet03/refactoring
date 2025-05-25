using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public record CreateNoteCommand : IRequest<Result<string?>>
{
    public required string ProjectId { get; init; }
    public required string? Headline { get; init; }
}