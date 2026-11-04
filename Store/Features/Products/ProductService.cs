using Store.Shared.Exceptions;

namespace Store.Features.Products;

public class ProductService(ProductRepo repo)
{
    public async Task<List<Product>> ListAsync()
        => await repo.ListAsync();

    public async Task<List<Product>> ListByNameAsync(string name)
        => await repo.ListByNameAsync(name);

    public async Task<Product> FindAsync(Guid id)
        => await FindAndValidateAsync(id);

    public async Task<Product?> AddAsync(Product product)
        => await repo.AddAsync(product);

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existingProduct = await FindAndValidateAsync(product.Id);

        existingProduct.Update(product.Name, product.Price);

        return await repo.UpdateAsync(existingProduct);
    }

    public async Task<Product?> RemoveAsync(Guid id)
    {
        var existingProduct = await FindAndValidateAsync(id);

        return await repo.RemoveAsync(existingProduct);
    }

    public async Task<Product?> ActivateAsync(Guid id)
    {
        var existingProduct = await FindAndValidateAsync(id);

        existingProduct.Activate();

        return await repo.UpdateAsync(existingProduct);
    }

    public async Task<Product?> DeactivateAsync(Guid id)
    {
        var existingProduct = await FindAndValidateAsync(id);

        existingProduct.Deactivate();

        return await repo.UpdateAsync(existingProduct);
    }

    private async Task<Product> FindAndValidateAsync(Guid id)
    {
        var existingProduct = await repo.FindAsync(id);

        if (existingProduct == null) throw new NotFoundException();

        return existingProduct;
    }
}