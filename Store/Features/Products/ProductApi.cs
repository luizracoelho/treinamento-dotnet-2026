using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.Shared.Exceptions;

namespace Store.Features.Products;

public static class ProductApi
{
    public static IEndpointRouteBuilder MapProductsApi(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/products").WithTags("Products");

        // Exemplo: https://store.com/products?search=televisao
        group.MapGet("/", async Task<Ok<List<ProductVm>>> ([FromServices] ProductApp app, [FromQuery] string? search) =>
        {
            List<ProductVm> vms;

            if (string.IsNullOrWhiteSpace(search))
                vms = await app.ListAsync();
            else
                vms = await app.ListByNameAsync(search);

            return TypedResults.Ok(vms);
        });

        group.MapGet("/{id:guid}", async Task<Ok<ProductVm>> ([FromServices] ProductApp app, [FromRoute] Guid id) =>
        {
            var vm = await app.FindAsync(id);
            return TypedResults.Ok(vm);
        }).ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", async Task<Created<ProductVm>> ([FromServices] ProductApp app, [FromBody] ProductInputVm inputVm) =>
        {
            var vm = await app.AddAsync(inputVm);
            return TypedResults.Created($"/products/{vm?.Id}", vm);
        }).ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:guid}", async Task<Ok<ProductVm>> ([FromServices] ProductApp app, [FromRoute] Guid id, [FromBody] ProductInputVm inputVm) =>
            {
                var vm = await app.UpdateAsync(id, inputVm);
                return TypedResults.Ok(vm);
            })
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapDelete("/{id:guid}", async Task<Ok<ProductVm>> ([FromServices] ProductApp app, [FromRoute] Guid id) =>
            {
                var vm = await app.RemoveAsync(id);
                return TypedResults.Ok(vm);
            })
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        ;

        group.MapPatch("/{id:guid}/activate", async Task<Ok<ProductVm>> ([FromServices] ProductApp app, [FromRoute] Guid id) =>
            {
                var vm = await app.ActivateAsync(id);
                return TypedResults.Ok(vm);
            })
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        ;

        group.MapPatch("/{id:guid}/deactivate", async Task<Ok<ProductVm>> ([FromServices] ProductApp app, [FromRoute] Guid id) =>
            {
                var vm = await app.DeactivateAsync(id);
                return TypedResults.Ok(vm);
            })
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        ;

        return builder;
    }
}