using ForApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ForApplication.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
    : base(dbContextOptions)
    {
        
    }
}