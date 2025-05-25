namespace NoteTakingApp.Domain.Models;

public record Project : IHasId
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    public required string UserId { get; init; }
    
    public required string Name { get; init; }
    
    public PrivacyLevel PrivacyLevel { get; init; }
}