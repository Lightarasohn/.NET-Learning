namespace Server.DTOs;

public record class Project
{
    public int Id { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectInfo { get; set; }

}
