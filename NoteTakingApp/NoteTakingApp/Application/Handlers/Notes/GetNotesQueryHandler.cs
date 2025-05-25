using MediatR;
using NoteTakingApp.Application.Queries;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Notes;

public class GetNotesQueryHandler(INotesRepository repository) : IRequestHandler<GetNotesQuery, IList<Note>>
{
    public async Task<IList<Note>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await repository.SearchAsync(n => n.ProjectId == request.ProjectId, cancellationToken);
        return notes;
    }
}