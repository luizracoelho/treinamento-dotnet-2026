namespace Store.Features.Products;

public class Product
{
    // Properties
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    // Construtor
    public Product(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
    }

    // Methods
    public void ToDouble()
    {
        Price *= 2;
    }
}