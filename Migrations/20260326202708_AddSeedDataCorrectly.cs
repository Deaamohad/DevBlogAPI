using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevBlogAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataCorrectly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 1, "deaa@deaa.com", "Deaa Mohammed" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Learning .NET is actually pretty cool!", new DateTime(2026, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "My First C# Post" },
                    { 2, 1, "I can't believe SQL Server is running in a container.", new DateTime(2026, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Docker is Magic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
