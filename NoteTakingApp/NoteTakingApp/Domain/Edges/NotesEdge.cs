namespace NoteTakingApp.Domain.Edges;

public record NotesEdge
{
    public required string FromNoteId { get; init; }
    public required string ToNoteId { get; init; }
}