using Microsoft.EntityFrameworkCore;
using MTG.Database.Models.Entity;

namespace MTG.Database.Models.DatabaseContext
{
    public class MTGDbContext : DbContext
    {
        public DbSet<Color> Colors { get; set; }

        public MTGDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Midgard\\MSSQLSERVER01;Initial Catalog=MagicTheGathering;Integrated Security=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>().HasKey(x => x.Id);
            modelBuilder.Entity<Color>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Color>().Property(p => p.Description).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 0, Name = "Unknown", Description = "Color not defined" },
                new Color { Id = 1, Name = "None", Description = "No color" },
                new Color { Id = 2, Name = "W", Description = "White" },
                new Color { Id = 4, Name = "U", Description = "Blue" },
                new Color { Id = 8, Name = "B", Description = "Black" },
                new Color { Id = 16, Name = "R", Description = "Red" },
                new Color { Id = 32, Name = "G", Description = "Green" }
            );
        }
    }
}
