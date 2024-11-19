using Server.DTOs;

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

List<Project> projects = new List<Project>()
{
    new Project
    {
        Id = 1,
        ProjectName = "Project1",
        ProjectInfo = "This is the our first project ever!"
    }
};

app.UseHttpsRedirection();

app.MapGet("/", () => "hello world");

app.Run();

