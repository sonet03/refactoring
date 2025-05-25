using MediatR;

namespace NoteTakingApp.Application.Queries;

public class HasAccessQuery : IRequest<bool>
{
    public required string ProjectId { get; init; }
    public string? UserId { get; init; }
}