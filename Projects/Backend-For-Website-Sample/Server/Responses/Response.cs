using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server;
using Server.DTOs;

namespace Server.Responses;

public class Response
{
    public static JsonArray GetAllProjects(List<Project> projects)
    {
        JsonArray result = new JsonArray();
        foreach (var project in projects)
        {
            result.Add(project);
        }
        return result;
    }

    public static string GetProject(int id, List<Project> projects) 
    {
        return JsonSerializer.Serialize<Project>(projects[id]);
    }
}
