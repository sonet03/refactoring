using FluentValidation;
using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Common;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Infrastructure.Repositories;
using NoteTakingApp.Infrastructure.Services;

namespace NoteTakingApp.Application.Handlers.Users;

public class RegisterUserCommandHandler(
    IUserRepository repository,
    IValidator<RegisterUserCommand> validator,
    IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, Result>
{
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
        }
        
        var existingUser = await repository.GetByUsernameOrEmailAsync(request.Username, request.Email, cancellationToken);

        if (existingUser != null)
        {
            return Result.Failure($"User Username={request.Username} Email={request.Email} already exists");
        }
        
        var user = new User
        {
            Email = request.Email,
            Username = request.Username,
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };

        await repository.UpsertAsync(user, cancellationToken);

        return Result.Success();
    }
}