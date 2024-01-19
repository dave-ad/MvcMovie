using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models.Domian;

public class Movie
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }
    public string? ReleaseYear { get; set; }
    [Required]
    public string? MovieImage { get; set; } // stores movie image name with extentions (eg, image0001.jpg)
    [Required]
    public string? Cast { get; set; }
    [Required]
    public string? Director { get; set; }

    [NotMapped]
    public IFormFile ImageFile { get; set; }

    [NotMapped]
    [Required]
    public List<int> Genres { get; set; }
    [NotMapped]
    public IEnumerable<SelectListItem> GenreList;
    [NotMapped]
    public string? GenreNames { get; set; }
}
