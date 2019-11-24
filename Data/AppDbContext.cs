using battleSimulator.Models;
using Microsoft.EntityFrameworkCore;

namespace battleSimulator.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {}

        public DbSet<Game> Games { get; set; }
        public DbSet<Army> Armies { get; set; }
    }
}