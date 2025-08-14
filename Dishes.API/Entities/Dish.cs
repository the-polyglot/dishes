﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Dishes.API.Entities;

public class Dish
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    public ICollection<Ingredient> Ingredients { get; set; } = [];

    public Dish()
    {
    }

    [SetsRequiredMembers]
    public Dish(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
