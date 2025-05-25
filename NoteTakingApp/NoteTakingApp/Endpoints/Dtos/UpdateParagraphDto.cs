namespace NoteTakingApp.Endpoints.Dtos;

public record UpdateParagraphDto
{
    public string? Content { get; init; }
}