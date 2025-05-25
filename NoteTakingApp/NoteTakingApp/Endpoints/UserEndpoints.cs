using Mapster;
using MediatR;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Endpoints.Dtos;

namespace NoteTakingApp.Endpoints;

public static class UserEndpoints
{
    public static void AddUserEndpoints(this WebApplication app)
    {
        app.MapPost("/users/login", async (LoginUserDto dto, IMediator mediator) =>
        {
            var command = dto.Adapt<LoginUserCommand>();
            var token = await mediator.Send(command);

            return token;
        }).WithTags("Users");
        
        app.MapPost("/user/register", async (RegisterUserDto dto, IMediator mediator) =>
        {
            var command = dto.Adapt<RegisterUserCommand>();
            var res = await mediator.Send(command);

            return res.IsSuccess ? Results.Ok() : Results.BadRequest(res.Error);
        }).WithTags("Users");
    }
}