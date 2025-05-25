using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public class DuplicateNoteCommand : IRequest<Result<string?>>
{
    public required string Id { get; init; }
}