using Mapster;
using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class CreateParagraphCommandHandler(INotesRepository notesRepository, IParagraphsRepository paragraphsRepository)
    : IRequestHandler<CreateParagraphCommand, Result<string?>>
{
    public async Task<Result<string?>> Handle(CreateParagraphCommand request, CancellationToken cancellationToken)
    {
        var note = await notesRepository.GetAsync(request.NoteId, cancellationToken);

        if (note == null)
        {
            return Result.Failure<string?>($"Note={request.NoteId} does not exists");
        }

        var paragraph = request.Adapt<NoteParagraph>();
        note.AddParagraph(paragraph.Id);
        
        // TODO: Transaction
        await paragraphsRepository.UpsertAsync(paragraph, cancellationToken);
        await notesRepository.UpsertAsync(note, cancellationToken);

        return Result.Success<string?>(paragraph.Id);
    }
}