using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class ConnectParagraphCommandHandler(INotesRepository repository)
    : IRequestHandler<ConnectParagraphCommand, Result>
{
    public async Task<Result> Handle(ConnectParagraphCommand request, CancellationToken cancellationToken)
    {
        // TODO add validations toId exists; toId same project;
        
        var note = await repository.GetAsync(request.NoteId, cancellationToken);

        if (note == null)   
        {
            return Result.Failure($"Note={request.NoteId} does not exists");
        }

        if (!note.HasParagraph(request.ParagraphId))
        {
            note.AddParagraph(request.ParagraphId);
            await repository.UpsertAsync(note, cancellationToken);
        }

        return Result.Success();
    }
}