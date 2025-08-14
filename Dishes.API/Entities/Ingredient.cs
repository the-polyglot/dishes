﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Dishes.API.Entities;

public class Ingredient
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    public ICollection<Dish> Dishes { get; set; } = [];

    public Ingredient()
    { }

    [SetsRequiredMembers]
    public Ingredient(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
