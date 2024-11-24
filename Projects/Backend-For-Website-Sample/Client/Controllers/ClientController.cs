﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using Client.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

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
