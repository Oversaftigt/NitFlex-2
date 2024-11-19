using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityMicroservice.Data
{
    public class UserDbContext : IdentityDbContext
    {

        public UserDbContext(DbContextOptions options) : base(options)
        {
                
        }
    }
}
