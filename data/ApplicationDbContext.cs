using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
namespace MyWebApp.Data 
{
public class ApplicationDbContext : DbContext
{
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
{
    
}
public DbSet<Category> Categories { get; set; } // this single line of code is 
                                               //used to create a table in the database

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Category>().HasData(
        new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
        new Category { CategoryId = 2, Name = "Adventure", DisplayOrder = 2 },
        new Category { CategoryId = 3, Name = "Comedy", DisplayOrder = 3 }
    );
}
}
}