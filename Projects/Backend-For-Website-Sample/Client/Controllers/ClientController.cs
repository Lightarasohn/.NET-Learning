using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
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
        ViewData["Form"] = "";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact([Bind("FullName,Email,PhoneNumber,Message")] CustomerModel customer)
    {
        if (ModelState.IsValid)
        {
            var client = new HttpClient();
            var customerJson = JsonSerializer.Serialize(customer);
            var customerContent = new StringContent(customerJson);
            customerContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseOfPostRequest = await client.PostAsync("http://localhost:5201/Customer", customerContent);
            if(responseOfPostRequest.IsSuccessStatusCode)
                ViewData["Form"] = "Thank you, we will inform you in 3 days!";
            return View();
        }
        ViewData["Form"] = "Something went wrong :(";
        return View();
        
    }

    public IActionResult Resume()
    {
        return View();
    }

    public async Task<IActionResult> Projects()
    {
        try
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5201/Projects");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            var projectList = JsonSerializer.Deserialize<List<ProjectModel>>(responseString, _options);
            return View(projectList);
        }
        catch (HttpRequestException)
        {
            return View();
        }
    }
}
