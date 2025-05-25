using NoteTakingApp.Domain;

namespace NoteTakingApp.Endpoints.Dtos;

public record CreateProjectDto
{
    public required string Name { get; init; }
    public PrivacyLevel PrivacyLevel { get; init; }
}