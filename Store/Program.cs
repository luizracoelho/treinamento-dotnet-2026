using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Store.Features.Products;
using Store.Shared.Contexts;
using Store.Shared.Handlers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("StoreDb");

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddOpenApi();
builder.Services.AddProductDi();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapProductsApi();

app.Run();