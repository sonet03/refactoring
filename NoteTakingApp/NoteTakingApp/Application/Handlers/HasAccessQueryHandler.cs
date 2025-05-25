using MediatR;
using NoteTakingApp.Application.Queries;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers;

public class HasAccessQueryHandler(IProjectsRepository repository) : IRequestHandler<HasAccessQuery, bool>
{
    public async Task<bool> Handle(HasAccessQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
        {
            return false;
        }

        var project = await repository.GetAsync(request.ProjectId, cancellationToken);

        return project != null && project.UserId == request.UserId;
    }
}