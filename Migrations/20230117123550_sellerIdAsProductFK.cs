using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicafeteria.Migrations
{
    /// <inheritdoc />
    public partial class sellerIdAsProductFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "Coffee",
                newName: "sellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sellerId",
                table: "Coffee",
                newName: "clientId");
        }
    }
}
