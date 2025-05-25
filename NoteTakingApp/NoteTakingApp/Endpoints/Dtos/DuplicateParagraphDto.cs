namespace NoteTakingApp.Endpoints.Dtos;

public record DuplicateParagraphDto
{
    public required string ParagraphId { get; init; }
    public required string NoteId { get; init; }
}