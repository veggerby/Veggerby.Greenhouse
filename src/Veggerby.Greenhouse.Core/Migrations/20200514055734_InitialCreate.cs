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
                name: "Sensors",
                columns: table => new
                {
                    DeviceId = table.Column<string>(nullable: false),
                    SensorId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => new { x.DeviceId, x.SensorId });
                    table.ForeignKey(
                        name: "FK_Sensors_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(nullable: true),
                    SensorId = table.Column<string>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Measurements_Sensors_DeviceId_SensorId",
                        columns: x => new { x.DeviceId, x.SensorId },
                        principalTable: "Sensors",
                        principalColumns: new[] { "DeviceId", "SensorId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedUtc", "Decimals", "Name", "Tolerance", "Unit" },
                values: new object[,]
                {
                    { "temperature", new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390), 3, "Temperature", 0.14999999999999999, "°C" },
                    { "humidity", new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390), 3, "Relative Humidity", 0.5, "%" },
                    { "pressure", new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390), 1, "Pressure", 1.0, "mbar" },
                    { "soil_humidity", new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390), 0, "Soil Humidity", 100.0, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PropertyId",
                table: "Measurements",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_DeviceId_SensorId",
                table: "Measurements",
                columns: new[] { "DeviceId", "SensorId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
