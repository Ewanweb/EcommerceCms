using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BEC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "Body", "Slug", "Title" },
                values: new object[,]
                {
                    { 1, "This is the Home Page", "home", "Home" },
                    { 2, "This is the About Page", "about", "About" },
                    { 3, "This is the Services Page", "services", "Services" },
                    { 4, "This is the Contact Page", "contact", "Contact" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
