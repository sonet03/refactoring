namespace NoteTakingApp.Domain.Edges;

public record NoteProjectEdge
{
    public required string ProjectId { get; init; }
    public required string NoteId { get; init; }
}