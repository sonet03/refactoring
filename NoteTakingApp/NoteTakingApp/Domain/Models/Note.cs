namespace NoteTakingApp.Domain.Models;

public record Note : IHasId
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    public required string ProjectId { get; init; }
    
    public string? Headline { get; init; }

    public IList<string> ParagraphIds { get; init; } = new List<string>();

    public void AddParagraph(string id)
    {
        ParagraphIds.Add(id);
    }
    
    public void RemoveParagraph(string id)
    {
        ParagraphIds.Remove(id);
    }
    
    public bool HasParagraph(string id)
    {
        return ParagraphIds.Contains(id);
    }
}