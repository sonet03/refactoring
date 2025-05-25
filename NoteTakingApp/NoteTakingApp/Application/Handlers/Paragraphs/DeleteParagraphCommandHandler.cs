using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class DeleteParagraphCommandHandler(INotesRepository notesRepository, IParagraphsRepository paragraphsRepository)
    : IRequestHandler<DeleteParagraphCommand, Result>
{
    public async Task<Result> Handle(DeleteParagraphCommand request, CancellationToken cancellationToken)
    {
        var notes = await notesRepository.SearchAsync(_ => true, cancellationToken);

        // TODO: Transaction
        foreach (var note in notes)
        {
            if (note.HasParagraph(request.Id))
            {
                note.RemoveParagraph(request.Id);
                await notesRepository.UpsertAsync(note, cancellationToken);
            }
        }

        await paragraphsRepository.DeleteAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}