namespace NoteTakingApp.Endpoints.Dtos;

public record CreateNoteDto
{
    public required string ProjectId { get; init; }
    public required string? Headline { get; init; }
}