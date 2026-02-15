using DataEntities;
using Microsoft.EntityFrameworkCore;
using Products.Data;

//// <summary>
//// Endpoints for the Products API.
//// This file contains extension methods that register HTTP endpoints for product operations.
//// </summary>
namespace Products.Endpoints;


/// <summary>
/// Provides extension methods to map HTTP endpoints for product operations.
/// </summary>
/// <remarks>
/// This static class contains an extension method that registers the following endpoints:
/// - GET /api/Product/           : Returns all products.
/// - GET /api/Product/{productId}: Returns a product by id.
/// - POST /api/Product/          : Creates a new product.
/// - PUT /api/Product/{id}       : Updates an existing product.
/// - DELETE /api/Product/{id}    : Deletes a product.
/// </remarks>
public static class ProductEndpoints
{
    /// <summary>
    /// Maps product-related endpoints onto the provided <see cref="IEndpointRouteBuilder"/>
    /// </summary>
    /// <param name="routes">The endpoint route builder to register routes on.</param>
    /// <remarks>
    /// This method is an extension method and will register a route group at "/api/Product".
    /// Registered endpoints:
    /// - GET "/" returns all products.
    /// - GET "/{productId}" returns a product by id or 404 if not found.
    /// - POST "/" creates a new product and returns 201 with Location header.
    /// - PUT "/{id}" updates an existing product and returns 204 on success or 404 if not found.
    /// - DELETE "/{id}" deletes a product and returns 204 on success or 404 if not found.
    /// </remarks>
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Product");

        group.MapGet("/", async (ProductDataContext db) =>
        {
            return await db.Product.ToListAsync();
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        //get existing product by id
        group.MapGet("/{productId}", async (int productId, ProductDataContext db) =>
        {
            return await db.Product.FindAsync(productId)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // create a new product
        group.MapPost("/", async (Product product, ProductDataContext db) =>
        {
            db.Product.Add(product);
            await db.SaveChangesAsync();

            return Results.CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);

        // update an existing product
        group.MapPut("/{id}", async (int id, Product updatedProduct, ProductDataContext db) =>
        {
            var existing = await db.Product.FindAsync(id);
            if (existing is null)
                return Results.NotFound();

            // update fields
            existing.Name = updatedProduct.Name;
            existing.Description = updatedProduct.Description;
            existing.Price = updatedProduct.Price;
            existing.ImageUrl = updatedProduct.ImageUrl;

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        // delete a product
        group.MapDelete("/{id}", async (int id, ProductDataContext db) =>
        {
            var existing = await db.Product.FindAsync(id);
            if (existing is null)
                return Results.NotFound();

            db.Product.Remove(existing);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteProduct")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
