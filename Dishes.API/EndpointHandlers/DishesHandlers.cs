using AutoMapper;
using Dishes.API.DbContexts;
using Dishes.API.Entities;
using Dishes.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dishes.API.EndpointHandlers;

public static class DishesHandlers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public static async Task<Ok<IEnumerable<DishDto>>> GetDishesAsync(
        DishesDbContext dishesDbContext,
        IMapper mapper,
        string? name)
    {
        return TypedResults.Ok(mapper.Map<IEnumerable<DishDto>>(
            await dishesDbContext.Dishes
                .Where(d => name == null || d.Name.Contains(name!))
                .ToListAsync()));
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public static async Task<Results<NotFound, Ok<DishDto>>> GetDishByIdAsync(
        DishesDbContext dishesDbContext,
        IMapper mapper,
        Guid dishId)
    {
        var dish = await dishesDbContext.Dishes
            .FirstOrDefaultAsync(d => d.Id == dishId);

        if (dish == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<DishDto>(dish));
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public static async Task<Results<NotFound, Ok<DishDto>>> GetDishByNameAsync(
        DishesDbContext dishesDbContext,
        IMapper mapper,
        string dishName)
    {
        var dish = await dishesDbContext.Dishes
            .FirstOrDefaultAsync(d => d.Name == dishName);

        if (dish is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<DishDto>(dish));
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public static async Task<CreatedAtRoute<DishDto>> CreateDishAsync(
        DishesDbContext dishesDbContext,
        IMapper mapper,
        DishForCreationDto dishForCreationDto)
    {
        var dish = mapper.Map<Dish>(dishForCreationDto);

        dishesDbContext.Add(dish);
        await dishesDbContext.SaveChangesAsync();

        var createdDish = mapper.Map<DishDto>(dish);

        return TypedResults.CreatedAtRoute(
            createdDish,
            "GetDishById",
            new { dishId = createdDish.Id });
    }


    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public static async Task<Results<NoContent, NotFound>> UpdateDishAsync(
        DishesDbContext dishesDbContext,
        IMapper mapper,
        Guid dishId,
        DishForUpdateDto dishForUpdateDto)
    {
        var existingDish = await dishesDbContext.Dishes
            .FirstOrDefaultAsync(d => d.Id == dishId);

        if (existingDish is null)
        {
            return TypedResults.NotFound();
        }

        var dish = mapper.Map(dishForUpdateDto, existingDish);

        await dishesDbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public static async Task<Results<NoContent, NotFound>> DeleteDishAsync(
        DishesDbContext dishesDbContext,
        Guid dishId)
    {
        var existingDish = await dishesDbContext.Dishes
            .FirstOrDefaultAsync(d => d.Id == dishId);

        if (existingDish is null)
        {
            return TypedResults.NotFound();
        }
        dishesDbContext.Remove(existingDish);
        await dishesDbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}