using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace MvcMovie.Models.Domian
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
