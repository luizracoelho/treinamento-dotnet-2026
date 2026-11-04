using Microsoft.AspNetCore.Mvc;
using Store.Shared.Exceptions;

namespace Store.Features.Products;

public static class ProductApi
{
    public static IEndpointRouteBuilder MapProductsApi(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/products").WithTags("Products");

        // Exemplo: https://store.com/products?search=televisao
        group.MapGet("/", async ([FromServices] ProductApp app, [FromQuery] string? search) =>
        {
            List<ProductVm> vms;

            if (string.IsNullOrWhiteSpace(search))
                vms = await app.ListAsync();
            else
                vms = await app.ListByNameAsync(search);

            return Results.Ok(vms);
        });

        group.MapGet("/{id:guid}", async ([FromServices] ProductApp app, [FromRoute] Guid id) =>
        {
            try
            {
                var vm = await app.FindAsync(id);
                return Results.Ok(vm);
            }
            catch (Exception e)
            {
                var problem = new ProblemDetails();
                problem.Title = e.Message;

                if (e is NotFoundException)
                    return Results.NotFound(problem);

                return Results.BadRequest(problem);
            }
        });

        group.MapPost("/", async ([FromServices] ProductApp app, [FromBody] ProductInputVm inputVm) =>
        {
            try
            {
                var vm = await app.AddAsync(inputVm);
                return Results.Ok(vm);
            }
            catch (Exception e)
            {
                var problem = new ProblemDetails();
                problem.Title = e.Message;

                return Results.BadRequest(problem);
            }
        });

        return builder;
    }
}