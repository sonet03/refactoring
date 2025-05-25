using Mapster;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Endpoints.Dtos;

namespace NoteTakingApp.Domain.MappingProfiles;

public class UserMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginUserDto, LoginUserCommand>();
        config.NewConfig<RegisterUserDto, RegisterUserCommand>();
    }
}