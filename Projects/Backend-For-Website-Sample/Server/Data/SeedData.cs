using Server.DTOs;
using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Data;

public class SeedData
{
    private static readonly List<Customer> _SeedListCustomer =
    [
        new Customer
        {
            Email = "onurdemir1771@gmail.com",
            FullName = "Onur Demir",
            PhoneNumber = "05465588563",
            Message = "Merhabalar, Ben onur demir.",
        },
        new Customer
        {
            Email = "onurcelik@gmail.com",
            FullName = "Onur Celik",
            PhoneNumber = "123456789",
            Message = "Merhabalar, Ben onur celik. Bu yan hesabim",
        },
    ];

    private static readonly List<Project> _SeedListProject =
    [
        new Project
    {
        ProjectName = "Project1",
        ProjectInfo = "This is the our first project ever!"
    },
    new Project
    {
        ProjectName = "Project2",
        ProjectInfo = "This is the second project!"
    },
    new Project
    {
        ProjectName = "Project3",
        ProjectInfo = "This is the third project!"
    },
    new Project
    {
        ProjectName = "Project4",
        ProjectInfo = "This is the fourth project!"
    },
    new Project
    {
        ProjectName = "Project5",
        ProjectInfo = "This is the fifth and last project!"
    }
    ];

    public static async Task<ProjectContext> InitializeProjectDatabase(IServiceProvider serviceProvider)
    {
        var context = new ProjectContext(
            serviceProvider.GetRequiredService<DbContextOptions<ProjectContext>>());
        
            if (context.Projects.Any())
                return context;
            foreach(var project in _SeedListProject)
            {
                await context.Projects.AddAsync(project);
            }
            await context.SaveChangesAsync();
            return context;
        
    }

    public static async Task<CustomerContext> InitializeCustomerDatabase(IServiceProvider serviceProvider)
    {
        var context = new CustomerContext(serviceProvider.GetRequiredService<DbContextOptions<CustomerContext>>());

        if (context.Customers.Any())
            return context;
        foreach(var customer in _SeedListCustomer)
            await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();
        return context;
    }
}
