using Dishes.API.EndpointHandlers;

namespace Dishes.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterDishesEndpoints(this IEndpointRouteBuilder endpointRouterBuilder)
    {
        var dishesEndpoints = endpointRouterBuilder.MapGroup("/dishes");
        var dishesWithIdEndpoints = dishesEndpoints.MapGroup("/{dishId:guid}");

        dishesEndpoints.MapGet("", DishesHandlers.GetDishesAsync);
        dishesWithIdEndpoints.MapGet("", DishesHandlers.GetDishByIdAsync).WithName("GetDishById");
        dishesEndpoints.MapGet("/{dishName}", DishesHandlers.GetDishByNameAsync);
        dishesEndpoints.MapPost("", DishesHandlers.CreateDishAsync);
        dishesWithIdEndpoints.MapPut("", DishesHandlers.UpdateDishAsync);
        dishesWithIdEndpoints.MapDelete("", DishesHandlers.DeleteDishAsync);
    }
    public static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder endpointRouterBuilder)
    {
        var ingredientsEndpoints = endpointRouterBuilder.MapGroup("dishes/{dishId:guid}/ingredients");

        ingredientsEndpoints.MapGet("", IngredientsHandlers.GetIngredientsAsync);
        ingredientsEndpoints.MapPost("", () =>{
            throw new NotImplementedException();
        });
    }
}