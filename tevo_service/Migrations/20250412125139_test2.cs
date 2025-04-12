using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tevo_service.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AddressInfoId",
                table: "Demand",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ContactInfoId",
                table: "Demand",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressInfoId",
                table: "Demand");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                table: "Demand");
        }
    }
}
