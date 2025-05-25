using Mapster;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Endpoints.Dtos;

namespace NoteTakingApp.Domain.MappingProfiles;

public class ProjectsMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(string userId, CreateProjectDto dto), CreateProjectCommand>()
            .Map(dst => dst.UserId, src => src.userId)
            .Map(dst => dst.Name, src => src.dto.Name)
            .Map(dst => dst.PrivacyLevel, src => src.dto.PrivacyLevel);
        
        config.NewConfig<(string userId, UpdateProjectDto dto), UpdateProjectCommand>()
            .Map(dst => dst.UserId, src => src.userId)
            .Map(dst => dst.Id, src => src.dto.Id)
            .Map(dst => dst.Name, src => src.dto.Name)
            .Map(dst => dst.PrivacyLevel, src => src.dto.PrivacyLevel);

        config.NewConfig<CreateProjectCommand, Project>();
    }
}