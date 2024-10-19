using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using Microsoft.EntityFrameworkCore.Sqlite;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSqlite<PizzaDB>(connectionString);

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1"
            , new OpenApiInfo
            {
                Title = "Pizza API"
            ,
                Description = "Track Your Pizzas"
            ,
                Version = "v1"
            });
    }
    );

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
app.MapGet("/Pizzas", async (PizzaDB db) => await db.Pizzas.ToListAsync());
app.MapGet("Pizzas/{id}", async (PizzaDB db, int id) => await db.Pizzas.FindAsync(id));
app.MapPost("/Pizzas/Create", async (Pizza pizza, PizzaDB pizzaDB) =>
{
    await pizzaDB.Pizzas.AddAsync(pizza);
    await pizzaDB.SaveChangesAsync();
    return Results.Created($"/Pizzas/Create/{pizza.Id}", pizza);
});
app.MapPut("/Pizzas/Update", async (PizzaDB pizzaDb, Pizza pizza, int id) => {
    var oldPizza = await pizzaDb.Pizzas.FindAsync(id);
    if(oldPizza is null){
        return Results.NotFound($"There is no pizza named {pizza.Name}");
    }
    oldPizza.Name = pizza.Name;
    oldPizza.Description = pizza.Description;
    await pizzaDb.SaveChangesAsync();
    return Results.Ok($"{oldPizza.Name} Updated To {pizza.Name}");
    });
app.MapDelete("/Pizzas/Delete", async(PizzaDB pizzaDb, int id) =>
{
    var pizza = await pizzaDb.Pizzas.FindAsync(id);
    if(pizza is null)
    {
        return Results.NotFound($"There is already no pizza having id of {id}");
    }
    pizzaDb.Pizzas.Remove(pizza);
    await pizzaDb.SaveChangesAsync();
    return Results.Ok("Done");
});

app.Run();
