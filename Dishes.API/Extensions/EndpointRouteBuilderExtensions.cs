using Dishes.API.EndpointHandlers;
using Dishes.API.Models;

namespace Dishes.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    private const string BaseRoute = "dishes";
    private const string TagDishes = "Dishes";
    private const string TagIngredients = "Ingredients";

    private static T WithDefaultResponses<T>(this T builder) where T : IEndpointConventionBuilder
    {
        builder.ProducesProblem(StatusCodes.Status500InternalServerError);
        return builder;
    }

    private static IEndpointRouteBuilder RegisterDishesEndpoints(this IEndpointRouteBuilder builder)
    {
        var dishesGroup = builder.MapGroup($"/{BaseRoute}")
            .WithTags(TagDishes)
            .WithOpenApi()
            .WithDefaultResponses();

        var dishesWithIdGroup = dishesGroup.MapGroup("/{dishId:guid}")
            .ProducesProblem(StatusCodes.Status404NotFound);

        dishesGroup.MapGet("", DishesHandlers.GetDishesAsync)
            .WithName("GetDishes")
            .Produces<IEnumerable<DishDto>>(StatusCodes.Status200OK)
            .WithSummary("Get all dishes")
            .WithDescription("Returns all dishes from the database.");

        dishesWithIdGroup.MapGet("", DishesHandlers.GetDishByIdAsync)
            .WithName("GetDishById")
            .Produces<DishDto>(StatusCodes.Status200OK)
            .WithSummary("Get a dish by id")
            .WithDescription("Returns a dish from the database by its id.");

        dishesGroup.MapGet("/{dishName}", DishesHandlers.GetDishByNameAsync)
            .WithName("GetDishByName")
            .Produces<DishDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get a dish by name")
            .WithDescription("Returns a dish from the database by its name.");

        dishesGroup.MapPost("", DishesHandlers.CreateDishAsync)
            .WithName("CreateDish")
            .Accepts<DishForCreationDto>("application/json")
            .Produces<DishDto>(StatusCodes.Status201Created)
            .WithSummary("Create a dish")
            .WithDescription("Creates a dish in the database.");

        dishesWithIdGroup.MapPut("", DishesHandlers.UpdateDishAsync)
            .WithName("UpdateDish")
            .Accepts<DishForUpdateDto>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .WithSummary("Update a dish")
            .WithDescription("Updates a dish in the database.");

        dishesWithIdGroup.MapDelete("", DishesHandlers.DeleteDishAsync)
            .WithName("DeleteDish")
            .Produces(StatusCodes.Status204NoContent)
            .WithSummary("Delete a dish")
            .WithDescription("Deletes a dish from the database.");

        return builder;
    }

    private static IEndpointRouteBuilder RegisterIngredientsEndpoints(this IEndpointRouteBuilder builder)
    {
        var ingredientsGroup = builder.MapGroup($"{BaseRoute}/{{dishId:guid}}/ingredients")
            .WithTags(TagIngredients)
            .WithOpenApi()
            .WithDefaultResponses()
            .ProducesProblem(StatusCodes.Status404NotFound);

        ingredientsGroup.MapGet("", IngredientsHandlers.GetIngredientsAsync)
            .WithName("GetIngredients")
            .Produces<IEnumerable<IngredientDto>>(StatusCodes.Status200OK)
            .WithSummary("Get all ingredients")
            .WithDescription("Returns all ingredients from the database.");

        return builder;
    }

    public static IEndpointRouteBuilder RegisterEndpoints(this IEndpointRouteBuilder builder)
    {
        return builder
            .RegisterDishesEndpoints()
            .RegisterIngredientsEndpoints();
    }
}