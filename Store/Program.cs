using Store.Features.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    var tv = new Product("TV", 2500.00m);
    var videoGame = new Product("Video Game", 3600.25m);

    var products = new Product[] { tv, videoGame };
    
    tv.ToDouble();
    products = products.Append(tv).ToArray();

    return products;
});

app.Run();