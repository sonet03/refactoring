namespace NoteTakingApp.Domain.Common.ValueObjects;

public abstract record EntityId
{
    public string Value { get; }

    protected EntityId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Id cannot be empty", nameof(value));
        Value = value;
    }

    public static implicit operator string(EntityId id) => id.Value;

    public override string ToString() => Value;
}

public record NoteId : EntityId
{
    public NoteId(string value) : base(value) { }
    public static NoteId New() => new(Guid.NewGuid().ToString());
}

public record ProjectId : EntityId
{
    public ProjectId(string value) : base(value) { }
    public static ProjectId New() => new(Guid.NewGuid().ToString());
}

public record UserId : EntityId
{
    public UserId(string value) : base(value) { }
    public static UserId New() => new(Guid.NewGuid().ToString());
}

public record ParagraphId : EntityId
{
    public ParagraphId(string value) : base(value) { }
    public static ParagraphId New() => new(Guid.NewGuid().ToString());
} 