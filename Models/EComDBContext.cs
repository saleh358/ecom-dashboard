using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECom_wep_app.Models;

public class EComDBContext : DbContext
{

    public DbSet<Customer> Customer { get; set; }
    public DbSet<Product> Products { get; set; }

    public EComDBContext(DbContextOptions<EComDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
