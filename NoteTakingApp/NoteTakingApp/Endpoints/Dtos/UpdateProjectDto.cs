using NoteTakingApp.Domain;

namespace NoteTakingApp.Endpoints.Dtos;

public record UpdateProjectDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public PrivacyLevel PrivacyLevel { get; init; }
}