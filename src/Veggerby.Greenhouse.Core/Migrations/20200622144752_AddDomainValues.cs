using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Veggerby.Greenhouse.Core.Migrations
{
    public partial class AddDomainValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PropertyDomainId",
                table: "Properties",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PropertyDomains",
                columns: table => new
                {
                    PropertyDomainId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDomains", x => x.PropertyDomainId);
                });

            migrationBuilder.CreateTable(
                name: "PropertyDomainValues",
                columns: table => new
                {
                    PropertyDomainValueId = table.Column<string>(nullable: false),
                    PropertyDomainId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LowerValue = table.Column<double>(nullable: false),
                    UpperValue = table.Column<double>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDomainValues", x => new { x.PropertyDomainId, x.PropertyDomainValueId });
                    table.ForeignKey(
                        name: "FK_PropertyDomainValues_PropertyDomains_PropertyDomainId",
                        column: x => x.PropertyDomainId,
                        principalTable: "PropertyDomains",
                        principalColumn: "PropertyDomainId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedUtc", "Decimals", "Name", "PropertyDomainId", "Tolerance", "Unit" },
                values: new object[] { "battery_current", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "Battery Current", null, 1.0, "mA" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedUtc", "Decimals", "Name", "PropertyDomainId", "Tolerance", "Unit" },
                values: new object[] { "charging", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "Is Battery Charging?", null, 0.01, "" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedUtc", "Decimals", "Name", "PropertyDomainId", "Tolerance", "Unit" },
                values: new object[] { "power_input", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "Power Input", null, 0.01, "" });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyDomainId",
                table: "Properties",
                column: "PropertyDomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyDomains_PropertyDomainId",
                table: "Properties",
                column: "PropertyDomainId",
                principalTable: "PropertyDomains",
                principalColumn: "PropertyDomainId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyDomains_PropertyDomainId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "PropertyDomainValues");

            migrationBuilder.DropTable(
                name: "PropertyDomains");

            migrationBuilder.DropIndex(
                name: "IX_Properties_PropertyDomainId",
                table: "Properties");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_current");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "charging");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "power_input");

            migrationBuilder.DropColumn(
                name: "PropertyDomainId",
                table: "Properties");
        }
    }
}
