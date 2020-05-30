using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Veggerby.Greenhouse.Core.Migrations
{
    public partial class AddAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Annotations",
                columns: table => new
                {
                    AnnotationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annotations", x => x.AnnotationId);
                    table.ForeignKey(
                        name: "FK_Annotations_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "MeasurementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "pressure",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "soil_humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "temperature",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedUtc", "Decimals", "Name", "Tolerance", "Unit" },
                values: new object[,]
                {
                    { "battery_charge", new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520), 0, "Battery Charge", 0.10000000000000001, "%" },
                    { "battery_temperature", new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520), 0, "Battery Temperature", 0.10000000000000001, "°C" },
                    { "battery_voltage", new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520), 0, "Battery Voltage", 5.0, "mV" },
                    { "io_voltage", new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520), 0, "I/O Voltage", 10.0, "mV" },
                    { "io_current", new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520), 0, "I/O Current", 1.0, "mA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annotations_MeasurementId",
                table: "Annotations",
                column: "MeasurementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Annotations");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_charge");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_temperature");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_voltage");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "io_current");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "io_voltage");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "pressure",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "soil_humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "temperature",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390));
        }
    }
}
