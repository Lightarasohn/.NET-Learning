using Humanizer;


Console.WriteLine("Hello, World!");

static void HumanizedQuantities()
{
    Console.WriteLine("case".ToQuantity(0));
    Console.WriteLine("case".ToQuantity(1));
    Console.WriteLine("case".ToQuantity(5));
}

Console.WriteLine("Quantites");
HumanizedQuantities();

static void HumanizedDates()
{
    Console.WriteLine(DateTime.UtcNow.AddHours(3).Humanize());
    Console.WriteLine(DateTime.UtcNow.AddHours(-24).Humanize());
    Console.WriteLine(DateTime.UtcNow.AddHours(-4).Humanize());
}
Console.WriteLine("Dates");
HumanizedDates();