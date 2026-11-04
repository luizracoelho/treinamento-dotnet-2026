using Microsoft.EntityFrameworkCore;
using Store.Features.Products;

namespace Store.Shared.Contexts;

public class DataContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}