using MediatR;
using NoteTakingApp.Domain.Models;

namespace NoteTakingApp.Application.Queries;

public record GetNotesQuery : IRequest<IList<Note>>
{
    public required string ProjectId { get; init; }
}