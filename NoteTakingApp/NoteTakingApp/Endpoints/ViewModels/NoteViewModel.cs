namespace NoteTakingApp.Endpoints.ViewModels;

public record NoteViewModel
{
    public required string Id { get; init; }
    
    public required string ProjectId { get; init; }
    
    public string? Headline { get; init; }

    public IList<ParagraphViewModel> Paragraphs { get; init; } = new List<ParagraphViewModel>();

}