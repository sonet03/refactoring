﻿using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Projects;

public class DeleteProjectCommandHandler(IProjectsRepository repository, INotesRepository notesRepository, IMediator mediator) : IRequestHandler<DeleteProjectCommand, Result>
{
    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await repository.GetAsync(request.Id, cancellationToken);
        if (project == null || project.UserId != request.UserId)
        {
            return Result.Failure($"Project={request.Id} does not exist for user={request.UserId}");
        }

        //TODO: transaction or eventual consistency
        var notes = await notesRepository.SearchAsync(n => n.ProjectId == request.Id, cancellationToken);
        foreach (var note in notes)
        {
            await mediator.Send(new DeleteNoteCommand { Id = note.Id }, cancellationToken);
        }
        
        await repository.DeleteAsync(project.Id, cancellationToken);
        return Result.Success();
    }
}