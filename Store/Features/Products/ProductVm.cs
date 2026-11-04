namespace Store.Features.Products;

public record ProductVm(Guid Id, string Name, decimal Price, bool IsActive);