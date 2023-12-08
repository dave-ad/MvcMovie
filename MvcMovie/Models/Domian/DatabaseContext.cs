using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models.Domian;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
            
    }
}
