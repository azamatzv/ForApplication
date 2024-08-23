using ForApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ForApplication.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=ProductDatabase; Username=postgres; Password=0932");
    }
}