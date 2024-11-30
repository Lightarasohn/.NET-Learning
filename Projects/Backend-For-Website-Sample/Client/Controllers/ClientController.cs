using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using Client.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Client.Controllers;

public class ClientController : Controller
{
    private JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true, // Ignore case-insensitive property name mismatches
        AllowTrailingCommas = true, // Allow trailing commas in JSON arrays
    };

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact([Bind("FullName,Email,PhoneNumber,Message")] Customer customer)
    {
        if (ModelState.IsValid)
        {
            var client = new HttpClient();
            var customerJson = JsonSerializer.Serialize(customer);
            var customerContent = new StringContent(customerJson);
            var responseOfPostRequest = await client.PostAsync("http://localhost:5201/Customer", customerContent);
        }
        return new CreatedResult();
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
        
        var projectList = JsonSerializer.Deserialize<List<ProjectModel>>(responseString, _options);
        return View(projectList);
    }
}
