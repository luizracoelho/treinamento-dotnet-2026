namespace Store.Features.Products;

public static class ProductMap
{
    public static ProductVm ToVm(this Product product) 
        => new(product.Id, product.Name, product.Price, product.IsActive);

    public static List<ProductVm> ToVm(this List<Product> products)
    {
        var vms = new List<ProductVm>();

        foreach (var product in products)
        {
            var productVm = product.ToVm();
            vms.Add(productVm);
        }

        return vms;
    }
}