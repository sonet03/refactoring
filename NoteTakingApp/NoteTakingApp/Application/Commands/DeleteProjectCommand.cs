using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public class DeleteProjectCommand : IRequest<Result>
{
    public required string Id { get; init; }
    
    public required string? UserId { get; init; }
}