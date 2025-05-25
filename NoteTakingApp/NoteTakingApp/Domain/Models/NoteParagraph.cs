using NoteTakingApp.Domain.Common.ValueObjects;

namespace NoteTakingApp.Domain.Models;

public record NoteParagraph : IHasId
{
    public string Id { get; init; } = ParagraphId.New();
    
    public string? Content { get; init; }
}