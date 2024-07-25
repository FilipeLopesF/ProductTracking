using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAnglesToTagPositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "angleAntenna01",
                table: "TagPositions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "angleAntenna02",
                table: "TagPositions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "58e175d3-683e-4611-b5fd-8f94ff3bb29e", "AQAAAAIAAYagAAAAEMr9CW7D+TMvg8+IJoM9e4lj+YZx7BQbYmJ/XFr9Z4YKYWPYvEWqSOFJtVO1bTwDeg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "angleAntenna01",
                table: "TagPositions");

            migrationBuilder.DropColumn(
                name: "angleAntenna02",
                table: "TagPositions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e915f376-ae2a-45aa-b6ae-94fe8a891f49", "AQAAAAIAAYagAAAAEH3esJ8marmcLWhULl+PORl5k0rpfpmf4yhoc1JGzuLIQmyUhLUIQzFJpc+DY8RB3A==" });
        }
    }
}
