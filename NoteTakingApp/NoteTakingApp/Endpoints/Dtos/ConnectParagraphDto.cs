namespace NoteTakingApp.Endpoints.Dtos;

public record ConnectParagraphDto
{
    public required string ParagraphId { get; init; }
    public required string NoteId { get; init; }
}