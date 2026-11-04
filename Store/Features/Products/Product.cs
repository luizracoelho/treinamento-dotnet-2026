using Store.Shared.Exceptions;

namespace Store.Features.Products;

public class Product(string name, decimal price)
{
    // Properties
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public decimal Price { get; private set; } = price;
    public bool IsActive { get; private set; } = true;

    // Methods
    public void Update(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public void Activate()
    {
        if (IsActive)
            throw new ActivateDeactivateException("O produto já está ativo.");

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new ActivateDeactivateException("O produto já está inativo.");

        IsActive = false;
    }
}