using MediatR;
using NoteTakingApp.Domain.Models;

namespace NoteTakingApp.Application.Queries;

public class GetParagraphsQuery : IRequest<IList<NoteParagraph>>
{
    public required IList<string> ParagraphIds { get; init; }
}