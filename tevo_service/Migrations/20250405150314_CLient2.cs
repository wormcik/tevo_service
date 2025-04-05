using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tevo_service.Migrations
{
    /// <inheritdoc />
    public partial class CLient2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientTelNo",
                table: "Client",
                type: "VARCHAR(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientSurname",
                table: "Client",
                type: "VARCHAR(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Client",
                type: "VARCHAR(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)");

            migrationBuilder.AddColumn<string>(
                name: "ClientDeliverMilk",
                table: "Client",
                type: "VARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientPrice",
                table: "Client",
                type: "VARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientRequestMilk",
                table: "Client",
                type: "VARCHAR(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientDeliverMilk",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientPrice",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientRequestMilk",
                table: "Client");

            migrationBuilder.AlterColumn<string>(
                name: "ClientTelNo",
                table: "Client",
                type: "VARCHAR(20)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientSurname",
                table: "Client",
                type: "VARCHAR(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Client",
                type: "VARCHAR(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldNullable: true);
        }
    }
}
