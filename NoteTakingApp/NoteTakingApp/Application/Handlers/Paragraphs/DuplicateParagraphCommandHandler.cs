﻿using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class DuplicateParagraphCommandHandler(INotesRepository notesRepository, IParagraphsRepository paragraphsRepository) : IRequestHandler<DuplicateParagraphCommand, Result<string?>>
{
    public async Task<Result<string?>> Handle(DuplicateParagraphCommand request, CancellationToken cancellationToken)
    {
        var paragraph = await paragraphsRepository.GetAsync(request.ParagraphId, cancellationToken);

        if (paragraph == null)
        {
            return Result.Failure<string?>($"Paragraph with Id={request.ParagraphId} does not exist");
        }

        var note = await notesRepository.GetAsync(request.NoteId, cancellationToken);

        if (note == null)
        {
            return Result.Failure<string?>($"Note with Id={request.NoteId} does not exist");
        }

        if (!note.HasParagraph(paragraph.Id))
        {
            return Result.Failure<string?>($"Note with Id={request.NoteId} does not have paragraph with Id={request.ParagraphId}");
        }
        
        var duplicate = new NoteParagraph()
        {
            Content = paragraph.Content
        };

        note.AddParagraph(duplicate.Id);

        // TODO: use transaction
        await paragraphsRepository.UpsertAsync(duplicate, cancellationToken);
        await notesRepository.UpsertAsync(note, cancellationToken);
        
        return Result.Success<string?>(duplicate.Id);
    }
}