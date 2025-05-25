namespace NoteTakingApp.Endpoints.ViewModels;

public record ParagraphViewModel
{
    public required string Id { get; init; }
    
    public string? Content { get; init; }
}