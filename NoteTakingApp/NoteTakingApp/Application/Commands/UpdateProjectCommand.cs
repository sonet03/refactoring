using MediatR;
using NoteTakingApp.Domain;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public record UpdateProjectCommand : IRequest<Result>
{
    public required string Id { get; init; }
    
    public required string UserId { get; init; }
    
    public required string Name { get; init; }
    
    public PrivacyLevel PrivacyLevel { get; init; }
}