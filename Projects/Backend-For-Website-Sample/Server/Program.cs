using Server.DTOs;
using Server.Responses;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

List<Project> projects =
[
    new Project
    {
        Id = 1,
        ProjectName = "Project1",
        ProjectInfo = "This is the our first project ever!"
    },
    new Project
    {
        Id = 2,
        ProjectName = "Project2",
        ProjectInfo = "This is the second project!"
    },
    new Project
    {
        Id = 3,
        ProjectName = "Project3",
        ProjectInfo = "This is the third and last project!"
    }
];

app.UseHttpsRedirection();

app.MapGet("/", () => "This is an API you want to use with /{Service}.\n" +
" If you are seeing this message then make sure you use the correct routing address.");

app.MapGet("/Projects", () => Response.GetAllProjects(projects));
app.MapGet("/Projects/{index}", (int index) => Response.GetProject(index, projects));


app.Run();
