using Microsoft.OpenApi.Models;
using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/Pizzas", () => PizzaDB.GetPizzas());
app.MapGet("/Pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapPost("/Pizzas/Create", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/Pizzas/Update", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/Pizzas/Delete", (int id) => PizzaDB.RemovePizza(id));
app.Run();