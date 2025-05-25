using Mapster;
using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Notes;

public class CreateNoteCommandHandler(INotesRepository repository) : IRequestHandler<CreateNoteCommand, Result<string?>>
{
    public async Task<Result<string?>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        // TODO: validate project exists
        var note = request.Adapt<Note>();
        await repository.UpsertAsync(note, cancellationToken);
        return Result.Success<string?>(note.Id);
    }
}