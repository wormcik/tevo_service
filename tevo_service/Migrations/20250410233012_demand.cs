using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tevo_service.Migrations
{
    /// <inheritdoc />
    public partial class demand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Demand",
                type: "VARCHAR(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Demand");
        }
    }
}
