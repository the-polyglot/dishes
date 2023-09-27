using Dishes.API.EndpointHandlers;

namespace Dishes.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterDishesEndpoints(this IEndpointRouteBuilder endpointRouterBuilder)
    {
        var dishesEndpoints = endpointRouterBuilder.MapGroup("/dishes")
            .WithOpenApi();

        var dishesWithIdEndpoints = dishesEndpoints.MapGroup("/{dishId:guid}")
            .WithOpenApi();

        dishesEndpoints.MapGet("", DishesHandlers.GetDishesAsync)
            .WithSummary("Get all dishes")
            .WithDescription("Returns all dishes from the database.");

        dishesWithIdEndpoints.MapGet("", DishesHandlers.GetDishByIdAsync).WithName("GetDishById")
            .WithSummary("Get a dish by id")
            .WithDescription("Returns a dish from the database by its id.");

        dishesEndpoints.MapGet("/{dishName}", DishesHandlers.GetDishByNameAsync)
            .WithSummary("Get a dish by name")
            .WithDescription("Returns a dish from the database by its name.");

        dishesEndpoints.MapPost("", DishesHandlers.CreateDishAsync)
            .WithSummary("Create a dish")
            .WithDescription("Creates a dish in the database.");

        dishesWithIdEndpoints.MapPut("", DishesHandlers.UpdateDishAsync)
            .WithSummary("Update a dish")
            .WithDescription("Updates a dish in the database.");

        dishesWithIdEndpoints.MapDelete("", DishesHandlers.DeleteDishAsync)
            .WithSummary("Delete a dish")
            .WithDescription("Deletes a dish from the database.");
    }
    public static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder endpointRouterBuilder)
    {
        var ingredientsEndpoints = endpointRouterBuilder.MapGroup("dishes/{dishId:guid}/ingredients")
            .WithOpenApi();

        ingredientsEndpoints.MapGet("", IngredientsHandlers.GetIngredientsAsync)
            .WithSummary("Get all ingredients")
            .WithDescription("Returns all ingredients from the database.");

        ingredientsEndpoints.MapPost("", () =>
        {
            throw new NotImplementedException();
        });
    }
}