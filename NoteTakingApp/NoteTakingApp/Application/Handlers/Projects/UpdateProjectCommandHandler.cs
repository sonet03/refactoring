using Mapster;
using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Projects;

public class UpdateProjectCommandHandler(IProjectsRepository repository)
    : IRequestHandler<UpdateProjectCommand, Result>
{
    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var projects =
            await repository.SearchAsync(p => p.Id == request.Id && p.UserId == request.UserId, cancellationToken);

        if (!projects.Any())
        {
            return Result.Failure($"User={request.UserId} is not owner of Project={request.Id}");
        }

        var project = request.Adapt<Project>();
        await repository.UpsertAsync(project, cancellationToken);
        return Result.Success();
    }
}