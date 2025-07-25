using Dishes.API.EndpointHandlers;
using Dishes.API.Models;

namespace Dishes.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    private static void RegisterDishesEndpoints(this IEndpointRouteBuilder builder)
    {
        var dishesGroup = builder.MapGroup("/dishes")
            .WithTags("Dishes")
            .WithOpenApi();

        var dishesWithIdGroup = dishesGroup.MapGroup("/{dishId:guid}")
            .WithTags("Dishes")
            .WithOpenApi();

        dishesGroup.MapGet("", DishesHandlers.GetDishesAsync)
            .WithName("GetDishes")
            .Accepts<Guid>("application/json")
            .Produces<IEnumerable<DishDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all dishes")
            .WithDescription("Returns all dishes from the database.");

        dishesWithIdGroup.MapGet("", DishesHandlers.GetDishByIdAsync)
            .WithName("GetDishById")
            .Accepts<Guid>("application/json")
            .Produces<DishDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get a dish by id")
            .WithDescription("Returns a dish from the database by its id.");

        dishesGroup.MapGet("/{dishName}", DishesHandlers.GetDishByNameAsync)
            .WithName("GetDishByName")
            .Accepts<Guid>("application/json")
            .Produces<DishDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get a dish by name")
            .WithDescription("Returns a dish from the database by its name.");

        dishesGroup.MapPost("", DishesHandlers.CreateDishAsync)
            .WithName("CreateDish")
            .Accepts<DishForCreationDto>("application/json")
            .Produces<DishDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create a dish")
            .WithDescription("Creates a dish in the database.");

        dishesWithIdGroup.MapPut("", DishesHandlers.UpdateDishAsync)
            .WithName("UpdateDish")
            .Accepts<DishForUpdateDto>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Update a dish")
            .WithDescription("Updates a dish in the database.");

        dishesWithIdGroup.MapDelete("", DishesHandlers.DeleteDishAsync)
            .WithName("DeleteDish")
            .Accepts<Guid>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete a dish")
            .WithDescription("Deletes a dish from the database.");
    }

    private static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder builder)
    {
        var ingredientsGroup = builder.MapGroup("dishes/{dishId:guid}/ingredients")
            .WithTags("Ingredients")
            .WithOpenApi();

        ingredientsGroup.MapGet("", IngredientsHandlers.GetIngredientsAsync)
            .WithName("GetIngredients")
            .Accepts<Guid>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<IEnumerable<IngredientDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all ingredients")
            .WithDescription("Returns all ingredients from the database.");
    }

    public static void RegisterEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.RegisterDishesEndpoints();
        builder.RegisterIngredientsEndpoints();
    }
}