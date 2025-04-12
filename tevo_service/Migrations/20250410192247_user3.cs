using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tevo_service.Migrations
{
    /// <inheritdoc />
    public partial class user3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BanReason",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "User",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanReason",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "User");
        }
    }
}
