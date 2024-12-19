using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DineMetrics.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDelaySecondsForDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efe9b645-2e0f-49b8-a599-c24eede796f4"));

            migrationBuilder.AddColumn<int>(
                name: "SecondsDelay",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 5);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AppointmentDate", "EateryId", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("0e55d223-7d76-482f-8db3-0ec3f384c50c"), new DateOnly(2022, 11, 28), null, "admin@gmail.com", "f9c355b602a10ee3e31c2f2c23acdcba3b299ddcf9607ba0d10ae9d041e8e09b", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0e55d223-7d76-482f-8db3-0ec3f384c50c"));

            migrationBuilder.DropColumn(
                name: "SecondsDelay",
                table: "Devices");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AppointmentDate", "EateryId", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("efe9b645-2e0f-49b8-a599-c24eede796f4"), new DateOnly(2022, 11, 28), null, "admin@gmail.com", "f9c355b602a10ee3e31c2f2c23acdcba3b299ddcf9607ba0d10ae9d041e8e09b", 0 });
        }
    }
}
