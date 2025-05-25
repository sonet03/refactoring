namespace NoteTakingApp.Endpoints.Dtos;

public record LoginUserDto
{
    public required string UsernameOrEmail { get; init; }
    public required string Password { get; init; }
}