using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace tevo_service.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    Password = table.Column<string>(type: "VARCHAR(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AddressInfo",
                columns: table => new
                {
                    AddressInfoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Value = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Longitude = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Latitude = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressInfo", x => x.AddressInfoId);
                    table.ForeignKey(
                        name: "FK_AddressInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    ContactInfoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    Value = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.ContactInfoId);
                    table.ForeignKey(
                        name: "FK_ContactInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Demand",
                columns: table => new
                {
                    DemandId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DemandedMilk = table.Column<decimal>(type: "numeric", nullable: true),
                    DeliveredMilk = table.Column<decimal>(type: "numeric", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    Currency = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    DelivererUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demand", x => x.DemandId);
                    table.ForeignKey(
                        name: "FK_Demand_User_DelivererUserId",
                        column: x => x.DelivererUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demand_User_RecipientUserId",
                        column: x => x.RecipientUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressInfo_UserId",
                table: "AddressInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_UserId",
                table: "ContactInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Demand_DelivererUserId",
                table: "Demand",
                column: "DelivererUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Demand_RecipientUserId",
                table: "Demand",
                column: "RecipientUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressInfo");

            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "Demand");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
