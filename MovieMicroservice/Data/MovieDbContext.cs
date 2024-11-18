using Microsoft.EntityFrameworkCore;
using MovieMicroservice.Models;

namespace MovieMicroservice.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        public DbSet<MovieItem> Movies { get; set; }
    }
}
