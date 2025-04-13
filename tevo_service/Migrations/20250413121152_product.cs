using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tevo_service.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DemandedMilk",
                table: "Demand",
                newName: "Demanded");

            migrationBuilder.RenameColumn(
                name: "DeliveredMilk",
                table: "Demand",
                newName: "Delivered");

            migrationBuilder.AddColumn<bool>(
                name: "ManuelMi",
                table: "Demand",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Demand",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManuelMi",
                table: "Demand");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Demand");

            migrationBuilder.RenameColumn(
                name: "Demanded",
                table: "Demand",
                newName: "DemandedMilk");

            migrationBuilder.RenameColumn(
                name: "Delivered",
                table: "Demand",
                newName: "DeliveredMilk");
        }
    }
}
