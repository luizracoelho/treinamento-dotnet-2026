namespace Store.Features.Products;

public class ProductApp(ProductService service)
{
    public async Task<List<ProductVm>> ListAsync()
    {
        var products = await service.ListAsync();
        return products.ToVm();
    }

    public async Task<List<ProductVm>> ListByNameAsync(string name)
    {
        var products = await service.ListByNameAsync(name);
        return products.ToVm();
    }

    public async Task<ProductVm?> FindAsync(Guid id)
    {
        var product = await service.FindAsync(id);
        return product.ToVm();
    }

    public async Task<ProductVm?> AddAsync(ProductInputVm inputVm)
    {
        var product = new Product(inputVm.Name, inputVm.Price);
        await service.AddAsync(product);

        return product.ToVm();
    }
}