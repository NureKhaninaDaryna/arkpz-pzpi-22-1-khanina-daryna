using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DineMetrics.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSettingsForEatery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6395e8b6-5845-4de5-a30f-ce497fca4e35"));

            migrationBuilder.AddColumn<int>(
                name: "MaximumCapacity",
                table: "Eateries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OperatingHours",
                table: "Eateries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TemperatureThreshold",
                table: "Eateries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AppointmentDate", "EateryId", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("efe9b645-2e0f-49b8-a599-c24eede796f4"), new DateOnly(2022, 11, 28), null, "admin@gmail.com", "f9c355b602a10ee3e31c2f2c23acdcba3b299ddcf9607ba0d10ae9d041e8e09b", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efe9b645-2e0f-49b8-a599-c24eede796f4"));

            migrationBuilder.DropColumn(
                name: "MaximumCapacity",
                table: "Eateries");

            migrationBuilder.DropColumn(
                name: "OperatingHours",
                table: "Eateries");

            migrationBuilder.DropColumn(
                name: "TemperatureThreshold",
                table: "Eateries");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AppointmentDate", "EateryId", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("6395e8b6-5845-4de5-a30f-ce497fca4e35"), new DateOnly(2022, 11, 28), null, "admin@gmail.com", "f9c355b602a10ee3e31c2f2c23acdcba3b299ddcf9607ba0d10ae9d041e8e09b", 0 });
        }
    }
}
