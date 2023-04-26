using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicafeteria.Migrations
{
    /// <inheritdoc />
    public partial class coffeeComplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Coffee",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Coffee",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Coffee");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Coffee");
        }
    }
}
