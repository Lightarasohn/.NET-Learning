﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
    
    public IActionResult Projects()
    {
        return View();
    }
}