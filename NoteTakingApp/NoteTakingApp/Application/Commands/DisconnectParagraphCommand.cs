using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public record DisconnectParagraphCommand : IRequest<Result>
{
    public required string ParagraphId { get; init; }
    public required string NoteId { get; init; }
}