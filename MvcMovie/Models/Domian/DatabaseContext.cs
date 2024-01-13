using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models.Domian;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
            
    }

    public DbSet<Genre> Genre { get; set; }
    public DbSet<Movie> Movie { get; set; }
    public DbSet<MovieGenre> MovieGenre { get; set; }
}
