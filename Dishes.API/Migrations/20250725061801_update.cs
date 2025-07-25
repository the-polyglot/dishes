using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dishes.API.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DishIngredient",
                keyColumns: new[] { "DishesId", "IngredientsId" },
                keyValues: new object[] { new Guid("b512d7cf-b331-4b54-8dae-d1228d128e8d"), new Guid("ecd396c3-4403-4fbf-83ca-94a8e9d859b3") });

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("ecd396c3-4403-4fbf-83ca-94a8e9d859b3"));

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("d5cad9a4-144e-4a3d-858d-9840792fa65d"),
                column: "Name",
                value: "Bay leaves");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("d5cad9a4-144e-4a3d-858d-9840792fa65d"),
                column: "Name",
                value: "Bay leave");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ecd396c3-4403-4fbf-83ca-94a8e9d859b3"), "Red wine" });

            migrationBuilder.InsertData(
                table: "DishIngredient",
                columns: new[] { "DishesId", "IngredientsId" },
                values: new object[] { new Guid("b512d7cf-b331-4b54-8dae-d1228d128e8d"), new Guid("ecd396c3-4403-4fbf-83ca-94a8e9d859b3") });
        }
    }
}
