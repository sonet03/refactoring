using Mapster;
using NoteTakingApp.Application.Commands;
using NoteTakingApp.Domain.Models;
using NoteTakingApp.Endpoints.Dtos;
using NoteTakingApp.Endpoints.ViewModels;

namespace NoteTakingApp.Domain.MappingProfiles;

public class NotesMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateNoteDto, CreateNoteCommand>();
        config.NewConfig<DuplicateNoteDto, DuplicateNoteCommand>();
        
        config.NewConfig<CreateNoteCommand, Note>();
        
        config.NewConfig<Note, NoteViewModel>();
    }
}