using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public class DeleteParagraphCommand : IRequest<Result>
{
    public required string Id { get; init; }
}