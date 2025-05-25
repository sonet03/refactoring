using NoteTakingApp.Domain.Common.ValueObjects;

namespace NoteTakingApp.Domain.Models;

public record Project : IHasId
{
    public string Id { get; init; } = ProjectId.New();
    
    public required UserId UserId { get; init; }
    
    public required string Name { get; init; }
    
    public PrivacyLevel PrivacyLevel { get; init; }
}