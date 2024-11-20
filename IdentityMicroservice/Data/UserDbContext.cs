using IdentityMicroservice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityMicroservice.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser>
    {

        public UserDbContext(DbContextOptions options) : base(options)
        {
                
        }
    }
}
