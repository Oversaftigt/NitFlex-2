using Microsoft.EntityFrameworkCore;
using RentalMicroservice.Models;

namespace RentalMicroservice.Data
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options) { }

        public DbSet<RentalItem> Rentals { get; set; }
    }
}
