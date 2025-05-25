using MediatR;
using NoteTakingApp.Application.Queries;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;

namespace NoteTakingApp.Application.Handlers.Paragraphs;

public class GetParagraphsQueryHandler(IParagraphsRepository paragraphsRepository) : IRequestHandler<GetParagraphsQuery, IList<NoteParagraph>>
{
    public async Task<IList<NoteParagraph>> Handle(GetParagraphsQuery request, CancellationToken cancellationToken)
    {
        var tasks = request.ParagraphIds.Select(id => paragraphsRepository.GetAsync(id, cancellationToken));

        var res = await Task.WhenAll(tasks);

        return res.Where(r => r != null).ToList()!;
    }
}