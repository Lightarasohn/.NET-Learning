using Server.DTOs;
using Server.Responses;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using Server.Data;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDataBase") 
    ?? throw new InvalidOperationException("Connection string not found")));
builder.Services.AddDbContext<CustomerContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDatabase") 
    ?? throw new InvalidOperationException("Connection string not found")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

ProjectContext projectContext;
CustomerContext customerContext;
Task<ProjectContext> taskProjectContext;
Task<CustomerContext> taskCustomerContext;
var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;

    taskProjectContext = SeedData.InitializeProjectDatabase(services);
    taskCustomerContext = SeedData.InitializeCustomerDatabase(services);
    projectContext = taskProjectContext.Result;
    customerContext = taskCustomerContext.Result;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var response = new Response(projectContext, customerContext);

app.MapGet("/", () => response.APIIndexRequestError());
//#################################################################
app.MapGet("/Projects", () => response.GetAllProjects());
app.MapGet("/Projects/{index}", (int index) => response.GetProject(index));
//#################################################################
app.MapPost("/Customer", (Customer customerInfo) => response.PostCustomerAsync(customerInfo));
app.Run();
