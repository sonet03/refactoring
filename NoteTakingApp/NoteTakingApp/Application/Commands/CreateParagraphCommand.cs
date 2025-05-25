using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public class CreateParagraphCommand : IRequest<Result<string?>>
{
    public required string NoteId { get; init; }
    public required string Content { get; init; }
}