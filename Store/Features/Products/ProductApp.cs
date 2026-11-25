using FluentValidation;

namespace Store.Features.Products;

public class ProductApp(ProductService service, ProductInputVmValidator validator)
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
        await validator.ValidateAndThrowAsync(inputVm);
        
        var product = new Product(inputVm.Name, inputVm.Price);
        await service.AddAsync(product);

        return product.ToVm();
    }

    public async Task<ProductVm?> UpdateAsync(Guid id, ProductInputVm inputVm)
    {
        await validator.ValidateAndThrowAsync(inputVm);
        
        var product = new Product(inputVm.Name, inputVm.Price);
        await service.UpdateAsync(id, product);

        return product.ToVm();
    }

    public async Task<ProductVm?> RemoveAsync(Guid id)
    {
        var product = await service.RemoveAsync(id);
        return product?.ToVm();
    }

    public async Task<ProductVm?> ActivateAsync(Guid id)
    {
        var product = await service.ActivateAsync(id);
        return product?.ToVm();
    }

    public async Task<ProductVm?> DeactivateAsync(Guid id)
    {
        var product = await service.DeactivateAsync(id);
        return product?.ToVm();
    }
}