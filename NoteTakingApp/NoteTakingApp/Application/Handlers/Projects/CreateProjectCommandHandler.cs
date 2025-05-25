using FluentValidation;
using Mapster;
using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Projects;

public class CreateProjectCommandHandler(IProjectsRepository repository, IValidator<CreateProjectCommand> validator)
    : IRequestHandler<CreateProjectCommand, Result<string?>>
{
    public async Task<Result<string?>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<string?>(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
        }
        
        var project = request.Adapt<Project>();

        await repository.UpsertAsync(project, cancellationToken);

        return Result.Success<string?>(project.Id);
    }
}