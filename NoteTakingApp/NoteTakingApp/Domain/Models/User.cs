namespace NoteTakingApp.Domain.Models;

public record User : IHasId
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required string PasswordHash { get; init; }
}