using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevBlogAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordToAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Content",
                value: "Hello!!!");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "Title" },
                values: new object[] { "I'm inside a container.", "HI I'm a post!" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Content",
                value: "Learning .NET is actually pretty cool!");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "Title" },
                values: new object[] { "I can't believe SQL Server is running in a container.", "Docker is Magic" });
        }
    }
}
