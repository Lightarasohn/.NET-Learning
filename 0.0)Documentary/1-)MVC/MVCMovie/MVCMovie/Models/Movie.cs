using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCMovie.Models;

public class Movie
{
    public int Id { get; set; }
    [StringLength(80, MinimumLength = 3)]
    [Required]
    public string? Title { get; set; }
    [DisplayName("Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    [StringLength(60)]
    [Required]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    public string? Genre { get; set; }
    [DataType(DataType.Currency)]
    [Range(1,100)]
    public decimal Price { get; set; }
    [Required]
    [StringLength(5)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    public string? Rating { get; set; }
}
