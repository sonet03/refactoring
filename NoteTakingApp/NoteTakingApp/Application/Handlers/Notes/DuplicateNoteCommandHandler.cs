using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Common.ValueObjects;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Notes;

public class DuplicateNoteCommandHandler(INotesRepository repository) : IRequestHandler<DuplicateNoteCommand, Result<string?>>
{
    public async Task<Result<string?>> Handle(DuplicateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await repository.GetAsync(request.Id, cancellationToken);

        if (note == null)
        {
            return Result.Failure<string?>($"Note with Id={request.Id} does not exist");
        }

        var duplicate = new Note
        {
            Headline = note.Headline,
            ProjectId = note.ProjectId,
            ParagraphIds = new List<ParagraphId>(note.ParagraphIds)
        };

        await repository.UpsertAsync(duplicate, cancellationToken);
        
        return Result.Success<string?>(duplicate.Id);
    }
}