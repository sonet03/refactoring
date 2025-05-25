namespace NoteTakingApp.Domain.Models;

public record NoteParagraph : IHasId
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    public string? Content { get; init; }
}