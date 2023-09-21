using AutoMapper;
using Dishes.API.Entities;
using Dishes.API.Models;

namespace Dishes.API.Profiles;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<Ingredient, IngredientDto>()
            .ForMember(
                dest => dest.DishId,
                opt => opt.MapFrom(src => src.Dishes.First().Id));
    }
}