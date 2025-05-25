using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public class UpdateParagraphCommand : IRequest<Result>
{
    public required string Id { get; init; }
    public required string? Content { get; init; }
}