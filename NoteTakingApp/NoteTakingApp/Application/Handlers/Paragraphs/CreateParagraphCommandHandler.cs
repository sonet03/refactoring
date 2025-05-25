using Mapster;
using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Common.ValueObjects;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;
using NoteTakingApp.Infrastructure.Services;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class CreateParagraphCommandHandler(
    INotesRepository notesRepository,
    IParagraphsRepository paragraphsRepository,
    ITransactionService transactionService)
    : IRequestHandler<CreateParagraphCommand, Result<string?>>
{
    public async Task<Result<string?>> Handle(CreateParagraphCommand request, CancellationToken cancellationToken)
    {
        return await transactionService.ExecuteInTransaction(async () =>
        {
            var note = await notesRepository.GetAsync(request.NoteId, cancellationToken);

            if (note == null)
            {
                return Result.Failure<string?>($"Note={request.NoteId} does not exists");
            }

            var paragraph = request.Adapt<NoteParagraph>();
            note.AddParagraph(new ParagraphId(paragraph.Id));
            
            await paragraphsRepository.UpsertAsync(paragraph, cancellationToken);
            await notesRepository.UpsertAsync(note, cancellationToken);

            return Result.Success<string?>(paragraph.Id);
        });
    }
}