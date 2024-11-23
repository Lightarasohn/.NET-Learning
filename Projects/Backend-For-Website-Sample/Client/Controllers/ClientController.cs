using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using Client.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Client.Controllers;

public class ClientController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Resume()
    {
        return View();
    }
    
    public async Task<IActionResult> Projects()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5201/Projects");
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
        var projectList = JsonSerializer.Deserialize<List<ProjectModel>>(responseString);
        Console.WriteLine(projectList.Count);
        Console.WriteLine(projectList[0]);
        Console.WriteLine(projectList[0].ToString);
        Console.WriteLine(projectList[0].Id);
        Console.WriteLine(projectList[0].ProjectName);
        Console.WriteLine(projectList[0].ProjectInfo);
        return View(projectList);
    }
}
