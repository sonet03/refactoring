using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class DisconnectParagraphCommandHandler(INotesRepository repository)
    : IRequestHandler<DisconnectParagraphCommand, Result>
{
    public async Task<Result> Handle(DisconnectParagraphCommand request, CancellationToken cancellationToken)
    {
        var note = await repository.GetAsync(request.NoteId, cancellationToken);

        if (note == null)
        {
            return Result.Failure($"Note={request.NoteId} does not exists");
        }

        note.RemoveParagraph(request.ParagraphId);
        await repository.UpsertAsync(note, cancellationToken);

        return Result.Success();
    }
}