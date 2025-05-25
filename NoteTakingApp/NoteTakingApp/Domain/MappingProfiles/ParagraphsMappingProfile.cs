using Mapster;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Endpoints.Dtos;
using NoteTakingApp.Endpoints.ViewModels;

namespace NoteTakingApp.Domain.MappingProfiles;

public class ParagraphsMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(string noteId, CreateParagraphDto dto), CreateParagraphCommand>()
            .Map(dst => dst.NoteId, src => src.noteId)
            .Map(dst => dst.Content, src => src.dto.Content);

        config.NewConfig<CreateParagraphCommand, NoteParagraph>();

        config.NewConfig<ConnectParagraphDto, ConnectParagraphCommand>();
        config.NewConfig<DisconnectParagraphDto, DisconnectParagraphCommand>();
        config.NewConfig<DuplicateParagraphDto, DuplicateParagraphCommand>();
        config.NewConfig<NoteParagraph, ParagraphViewModel>();
    }
}