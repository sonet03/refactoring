using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Infrastructure.Repositories;
using NoteTakingApp.Infrastructure.Services;

namespace NoteTakingApp.Application.Handlers.Users;

public class LoginUserCommandHandler(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginUserCommand, Result<string?>>
{
    public async Task<Result<string?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByUsernameOrEmailAsync(request.UsernameOrEmail, request.UsernameOrEmail, cancellationToken);
        
        if (user == null)
        {
            return Result.Failure<string?>($"User={request.UsernameOrEmail} does not exist");
        }

        if (!passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Result.Failure<string?>($"User={request.UsernameOrEmail} password is incorrect");
        }

        var token = jwtTokenGenerator.GenerateToken(user.Id, user.Username);
        return Result.Success<string?>(token);
    }
}