namespace NoteTakingApp.Endpoints.Dtos;

public record CreateParagraphDto
{
    public string? Content { get; init; }
}