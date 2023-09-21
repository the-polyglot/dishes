using AutoMapper;
using Dishes.API.DbContexts;
using Dishes.API.Entities;
using Dishes.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Dishes.API.EndpointHandlers;

public static class IngredientsHandlers
{
    public static async Task<Results<NotFound, Ok<IEnumerable<IngredientDto>>>> GetIngredientsAsync(
        DishesDbContext dishesDbContext,
        IMapper mapper,
        ILogger<IngredientDto> logger,
        Guid dishId)
    {
        logger.LogInformation("Getting ingredients for dish with id {DishId}", dishId);

        var dish = await dishesDbContext.Dishes
            .FirstOrDefaultAsync(d => d.Id == dishId);
        if (dish is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<IEnumerable<IngredientDto>>((await dishesDbContext.Dishes
            .Include(d => d.Ingredients)
            .FirstOrDefaultAsync(d => d.Id == dishId))?.Ingredients));
    }
}