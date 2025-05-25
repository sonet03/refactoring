using MediatR;
using NoteTakingApp.Domain;
using NoteTakingApp.Domain.Models;

namespace NoteTakingApp.Application.Queries;

public record GetProjectsQuery : IRequest<IList<Project>>
{
    public required string? UserId { get; init; }
    public required PrivacyLevel? PrivacyLevel { get; init; }
    public required string? NameSearch { get; init; }
}