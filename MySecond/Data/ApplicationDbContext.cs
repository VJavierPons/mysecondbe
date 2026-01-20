using Microsoft.EntityFrameworkCore;

namespace MySecond.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>()
                .Property(w => w.Id)
                .ValueGeneratedOnAdd();
        }
    }
}

//testing