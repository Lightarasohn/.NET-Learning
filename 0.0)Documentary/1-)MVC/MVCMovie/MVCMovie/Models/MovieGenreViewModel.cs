using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using MVCMovie.Models;

namespace MVCMovie.Models;

public class MovieGenreViewModel
{
    public List<Movie>? Movies { get; set; }
    public SelectList? Genres { get; set; }
    public SelectList? Ratings { get; set; }
    public string? MovieGenre { get; set; }
    public string? SearchString { get; set; }
    public string? RatingString { get; set; }
}