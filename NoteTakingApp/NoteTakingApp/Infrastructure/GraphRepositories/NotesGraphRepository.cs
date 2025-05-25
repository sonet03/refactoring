using Gremlin.Net.Driver;
using NoteTakingApp.Domain.Models;

namespace NoteTakingApp.Infrastructure.GraphRepositories;

public class NotesGraphRepository(IGremlinClient gremlinClient)
{
    public async Task AddNote(string projectId, Note note)
    {
        var addNote = $"g.addV('Note').property('id', '{note.Id}').property('headline', '{note.Headline}')";
        await gremlinClient.SubmitAsync(addNote);
        
        var addNoteToProject = $"g.V('{projectId}').addE('contains').to(g.V('{note.Id}'))";
        await gremlinClient.SubmitAsync(addNoteToProject);
    }
    
    public async Task GetAll(string projectId)
    {
        var getNotes = $"g.V('{projectId}').out('contains')";;
        var result = await gremlinClient.SubmitAsync<dynamic>(getNotes);
    }
}