namespace Store.Features.Products;

public static class ProductDi
{
    public static IServiceCollection AddProductDi(this IServiceCollection services)
    {
        services.AddScoped<ProductRepo>();
        services.AddScoped<ProductService>();
        services.AddScoped<ProductApp>();
        services.AddScoped<ProductInputVmValidator>();
        
        return services;
    }
}