using AutoMapper;
using Dishes.API.DbContexts;
using Dishes.API.Entities;
using Dishes.API.Extensions;
using Dishes.API.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DishesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DishesDbConnectionString")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    // app.UseExceptionHandler(config =>
    // {
    //     config.Run(async context =>
    //     {
    //         context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    //         context.Response.ContentType = "application/json";
    //         await context.Response.WriteAsJsonAsync(new ProblemDetails
    //         {
    //             Title = "An unexpected error occurred!",
    //             Detail = context.Features.Get<IExceptionHandlerFeature>()?.Error.Message
    //         });
    //     });
    // });
}

app.UseHttpsRedirection();

app.RegisterDishesEndpoints();
app.RegisterIngredientsEndpoints();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DishesDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
