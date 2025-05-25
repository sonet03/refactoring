using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Common.ValueObjects;
using NoteTakingApp.Infrastructure.Repositories;
using NoteTakingApp.Infrastructure.Services;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class DeleteParagraphCommandHandler(
    INotesRepository notesRepository,
    IParagraphsRepository paragraphsRepository,
    ITransactionService transactionService)
    : IRequestHandler<DeleteParagraphCommand, Result>
{
    public async Task<Result> Handle(DeleteParagraphCommand request, CancellationToken cancellationToken)
    {
        return await transactionService.ExecuteInTransaction(async () =>
        {
            var notes = await notesRepository.SearchAsync(_ => true, cancellationToken);

            foreach (var note in notes)
            {
                if (note.HasParagraph(new ParagraphId(request.Id)))
                {
                    note.RemoveParagraph(new ParagraphId(request.Id));
                    await notesRepository.UpsertAsync(note, cancellationToken);
                }
            }

            await paragraphsRepository.DeleteAsync(request.Id, cancellationToken);

            return Result.Success();
        });
    }
}