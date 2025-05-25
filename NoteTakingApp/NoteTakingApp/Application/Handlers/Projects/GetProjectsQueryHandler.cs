using MediatR;
using NoteTakingApp.Application.Queries;
using NoteTakingApp.Domain;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Projects;

public class GetProjectsQueryHandler(IProjectsRepository repository) : IRequestHandler<GetProjectsQuery, IList<Project>>
{
    public async Task<IList<Project>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Project> projects =
            await repository.SearchAsync(p => p.UserId == request.UserId,
                cancellationToken);

        if (request.PrivacyLevel != null)
        {
            projects = projects.Where(p => p.PrivacyLevel == request.PrivacyLevel.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.NameSearch))
        {
            projects = projects.Where(p =>
                p.Name.Contains(request.NameSearch, StringComparison.InvariantCultureIgnoreCase));
        }

        return projects.ToList();
    }
}