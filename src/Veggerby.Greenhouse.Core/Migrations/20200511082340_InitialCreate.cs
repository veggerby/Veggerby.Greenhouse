using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Veggerby.Greenhouse.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Tolerance = table.Column<double>(nullable: true),
                    Decimals = table.Column<int>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(nullable: true),
                    PropertyId = table.Column<string>(nullable: true),
                    FirstTimeUtc = table.Column<DateTime>(nullable: false),
                    LastTimeUtc = table.Column<DateTime>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    SumValue = table.Column<double>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    MinValue = table.Column<double>(nullable: false),
                    MaxValue = table.Column<double>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    UpdatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementId);
                    table.ForeignKey(
                        name: "FK_Measurements_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Measurements_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedUtc", "Decimals", "Name", "Tolerance", "Unit" },
                values: new object[] { "temperature", new DateTime(2020, 5, 11, 8, 23, 40, 175, DateTimeKind.Utc).AddTicks(5460), 3, "Temperature", 0.14999999999999999, "°C" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedUtc", "Decimals", "Name", "Tolerance", "Unit" },
                values: new object[] { "humidity", new DateTime(2020, 5, 11, 8, 23, 40, 175, DateTimeKind.Utc).AddTicks(5460), 3, "Relative Humidity", 0.5, "%" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedUtc", "Decimals", "Name", "Tolerance", "Unit" },
                values: new object[] { "pressure", new DateTime(2020, 5, 11, 8, 23, 40, 175, DateTimeKind.Utc).AddTicks(5460), 1, "Pressure", 1.0, "mbar" });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_DeviceId",
                table: "Measurements",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PropertyId",
                table: "Measurements",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
