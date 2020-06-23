using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Veggerby.Greenhouse.Core.Migrations
{
    public partial class AddDomainValuesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PropertyDomains",
                columns: new[] { "PropertyDomainId", "CreatedUtc", "Name" },
                values: new object[] { "charging", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Charging Discrete Values" });

            migrationBuilder.InsertData(
                table: "PropertyDomains",
                columns: new[] { "PropertyDomainId", "CreatedUtc", "Name" },
                values: new object[] { "power_input", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Power Input Discrete Values" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "charging",
                column: "PropertyDomainId",
                value: "charging");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "power_input",
                column: "PropertyDomainId",
                value: "power_input");

            migrationBuilder.InsertData(
                table: "PropertyDomainValues",
                columns: new[] { "PropertyDomainId", "PropertyDomainValueId", "CreatedUtc", "LowerValue", "Name", "UpperValue" },
                values: new object[,]
                {
                    { "charging", "not_charging", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, "NOT CHARGING", 0.0 },
                    { "charging", "charging", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1.0, "CHARGING", 1.0 },
                    { "power_input", "not_present", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), -1.0, "NOT_PRESENT", -1.0 },
                    { "power_input", "bad", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, "BAD", 0.0 },
                    { "power_input", "weak", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1.0, "WEAK", 1.0 },
                    { "power_input", "present", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2.0, "PRESENT", 2.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PropertyDomainValues",
                keyColumns: new[] { "PropertyDomainId", "PropertyDomainValueId" },
                keyValues: new object[] { "charging", "charging" });

            migrationBuilder.DeleteData(
                table: "PropertyDomainValues",
                keyColumns: new[] { "PropertyDomainId", "PropertyDomainValueId" },
                keyValues: new object[] { "charging", "not_charging" });

            migrationBuilder.DeleteData(
                table: "PropertyDomainValues",
                keyColumns: new[] { "PropertyDomainId", "PropertyDomainValueId" },
                keyValues: new object[] { "power_input", "bad" });

            migrationBuilder.DeleteData(
                table: "PropertyDomainValues",
                keyColumns: new[] { "PropertyDomainId", "PropertyDomainValueId" },
                keyValues: new object[] { "power_input", "not_present" });

            migrationBuilder.DeleteData(
                table: "PropertyDomainValues",
                keyColumns: new[] { "PropertyDomainId", "PropertyDomainValueId" },
                keyValues: new object[] { "power_input", "present" });

            migrationBuilder.DeleteData(
                table: "PropertyDomainValues",
                keyColumns: new[] { "PropertyDomainId", "PropertyDomainValueId" },
                keyValues: new object[] { "power_input", "weak" });

            migrationBuilder.DeleteData(
                table: "PropertyDomains",
                keyColumn: "PropertyDomainId",
                keyValue: "charging");

            migrationBuilder.DeleteData(
                table: "PropertyDomains",
                keyColumn: "PropertyDomainId",
                keyValue: "power_input");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "charging",
                column: "PropertyDomainId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "power_input",
                column: "PropertyDomainId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "charging",
                column: "PropertyDomainId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: "power_input",
                column: "PropertyDomainId",
                value: null);
        }
    }
}
