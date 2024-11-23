using Server.DTOs;
using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Data;

public class SeedData
{
    private static readonly List<Project> _SeedList =
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

    public static async Task<ProjectContext> Initialize(IServiceProvider serviceProvider)
    {
        var context = new ProjectContext(
            serviceProvider.GetRequiredService<DbContextOptions<ProjectContext>>());
        
            if (context.Projects.Any())
                return context;
            foreach(var project in _SeedList)
            {
                context.Projects.Add(project);
            }
            await context.SaveChangesAsync();
            return context;
        
    }
}
