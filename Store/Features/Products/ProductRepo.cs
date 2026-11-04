using Microsoft.EntityFrameworkCore;
using Store.Shared.Contexts;

namespace Store.Features.Products;

public class ProductRepo(DataContext context)
{
    public async Task<List<Product>> ListAsync()
        => await context.Products.ToListAsync();

    public async Task<List<Product>> ListByNameAsync(string name)
        => await context.Products.Where(x =>
            x.Name.Trim().ToLower().Contains(name.Trim().ToLower())
        ).ToListAsync();

    public async Task<Product?> FindAsync(Guid id)
        => await context.Products.FindAsync(id);

    public async Task<Product?> AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> RemoveAsync(Product product)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return product;
    }
}