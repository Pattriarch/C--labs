using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Context;

public class EducationContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder); optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=Profi123987")
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Book>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Publisher>().Property(p => p.Id).ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Book>().HasMany(au => au.Authors).WithMany(af => af.Books);
        modelBuilder.Entity<Book>().HasOne(au => au.Category).WithMany(af => af.Books);
        modelBuilder.Entity<Book>().HasOne(au => au.Publisher).WithMany(af => af.Books);
    }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
}