using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public record LoginUserCommand : IRequest<Result<string?>>
{
    public required string UsernameOrEmail { get; init; }
    public required string Password { get; init; }
}