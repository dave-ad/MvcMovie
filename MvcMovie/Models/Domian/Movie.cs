namespace MvcMovie.Models.Domian;

public class Movie
{
    public int Id { get; set; }

    [Required]
    public string? Titile { get; set; }

    public string? ReleaseYear { get; set; }

    [Required]
    public string? MovieImage { get; set; } // stores movie image name with extentions (eg, image0001.jpg)

    [Required]
    public string? Cast { get; set; }

    [Required]
    public string? DIrector { get; set; }
}
