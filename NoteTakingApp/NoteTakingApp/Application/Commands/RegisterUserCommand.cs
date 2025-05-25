using MediatR;
using NoteTakingApp.Domain.Common;

namespace NoteTakingApp.Application.Commands;

public record RegisterUserCommand : IRequest<Result>
{
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}