using Mapster;
using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class UpdateParagraphCommandHandler(IParagraphsRepository paragraphsRepository)
    : IRequestHandler<UpdateParagraphCommand, Result>
{
    public async Task<Result> Handle(UpdateParagraphCommand request, CancellationToken cancellationToken)
    {
        var paragraph = request.Adapt<NoteParagraph>();
        
        await paragraphsRepository.UpsertAsync(paragraph, cancellationToken);

        return Result.Success();
    }
}