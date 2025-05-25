namespace NoteTakingApp.Endpoints.Dtos;

public record DisconnectParagraphDto
{
    public required string ParagraphId { get; init; }
    public required string NoteId { get; init; }
}