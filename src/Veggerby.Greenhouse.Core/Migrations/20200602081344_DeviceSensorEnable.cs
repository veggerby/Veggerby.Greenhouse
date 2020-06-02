using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Veggerby.Greenhouse.Core.Migrations
{
    public partial class DeviceSensorEnable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Sensors",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Devices",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_charge",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_temperature",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_voltage",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "io_current",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "io_voltage",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "pressure",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "soil_humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "temperature",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Devices");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_charge",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_temperature",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_voltage",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "io_current",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "io_voltage",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "pressure",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "soil_humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "temperature",
                column: "CreatedUtc",
                value: new DateTime(2020, 6, 2, 7, 38, 27, 71, DateTimeKind.Utc).AddTicks(8930));
        }
    }
}
