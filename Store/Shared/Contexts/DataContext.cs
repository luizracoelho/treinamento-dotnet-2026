using Microsoft.EntityFrameworkCore;
using Store.Features.Products;

namespace Store.Shared.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products { get; set; }
}