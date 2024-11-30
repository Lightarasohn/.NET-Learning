using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server;
using Server.Data;
using Server.DTOs;

namespace Server.Responses;

public class Response
{
    private readonly ProjectContext _ProjectContext;
    private readonly CustomerContext _CustomerContext;
    
    public Response(ProjectContext context, CustomerContext customer)
    {
        _ProjectContext = context;
        _CustomerContext = customer;
    }
    public string APIIndexRequestError()
    {
        return "This is an API you want to use with /{Service}.\n" +
               " If you are seeing this message then make sure you use the correct routing address.";
    }
    public JsonArray GetAllProjects()
    {
        JsonArray result = new JsonArray();
        foreach (var project in _ProjectContext.Projects.ToArray())
        {
            if(project != null)
            result.Add(project);
        }
        return result;
    }

    public string GetProject(int id)
    {
        var project = _ProjectContext.Projects.Find(id);
        if (project == null)
            return "";
        return JsonSerializer.Serialize<Project>(project);
    }

    public async void PostCustomerAsync(Customer customer)
    {
        try
        {
                await _CustomerContext.Customers.AddAsync(customer);
                await _CustomerContext.SaveChangesAsync();
            
           
        }
        catch { throw new Exception(); }
    }
}
