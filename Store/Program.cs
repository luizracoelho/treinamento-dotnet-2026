using Scalar.AspNetCore;
using Store.Features.Products;
using Store.Shared.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DataContext>();

builder.Services.AddOpenApi();
builder.Services.AddProductDi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapProductsApi();

app.Run();