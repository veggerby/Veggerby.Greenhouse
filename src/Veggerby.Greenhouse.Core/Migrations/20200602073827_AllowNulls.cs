using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Veggerby.Greenhouse.Core.Migrations
{
    public partial class AllowNulls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Measurements",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "SumValue",
                table: "Measurements",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "MinValue",
                table: "Measurements",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "MaxValue",
                table: "Measurements",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Measurements",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "SumValue",
                table: "Measurements",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MinValue",
                table: "Measurements",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MaxValue",
                table: "Measurements",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_charge",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_temperature",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "battery_voltage",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "humidity",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "io_current",
                column: "CreatedUtc",
                value: new DateTime(2020, 5, 29, 9, 7, 49, 946, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "io_voltage",
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
        }
    }
}
