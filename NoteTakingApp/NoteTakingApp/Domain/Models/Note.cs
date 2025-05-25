using NoteTakingApp.Domain.Common.ValueObjects;

namespace NoteTakingApp.Domain.Models;

public record Note : IHasId
{
    public string Id { get; init; } = NoteId.New();
    
    public required ProjectId ProjectId { get; init; }
    
    public string? Headline { get; init; }

    public IList<ParagraphId> ParagraphIds { get; init; } = new List<ParagraphId>();

    public void AddParagraph(ParagraphId id)
    {
        ParagraphIds.Add(id);
    }
    
    public void RemoveParagraph(ParagraphId id)
    {
        ParagraphIds.Remove(id);
    }
    
    public bool HasParagraph(ParagraphId id)
    {
        return ParagraphIds.Contains(id);
    }
}