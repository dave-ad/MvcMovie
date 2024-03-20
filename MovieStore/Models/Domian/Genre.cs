namespace MovieStore.Models.Domian;

public class Genre
{
    public int Id { get; set; }

    [Required]
    public string? GenreName { get; set; }
}
