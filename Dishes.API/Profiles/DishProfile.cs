using AutoMapper;
using Dishes.API.Entities;
using Dishes.API.Models;

namespace Dishes.API.Profiles;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDto>();
        CreateMap<Dish, DishForCreationDto>().ReverseMap();
        CreateMap<Dish, DishForUpdateDto>().ReverseMap();
    }
}