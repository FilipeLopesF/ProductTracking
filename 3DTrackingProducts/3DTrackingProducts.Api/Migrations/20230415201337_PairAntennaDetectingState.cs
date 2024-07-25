using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    /// <inheritdoc />
    public partial class PairAntennaDetectingState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetectingState",
                table: "PairAntennas",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVerificationTimeStamp",
                table: "PairAntennas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e915f376-ae2a-45aa-b6ae-94fe8a891f49", "AQAAAAIAAYagAAAAEH3esJ8marmcLWhULl+PORl5k0rpfpmf4yhoc1JGzuLIQmyUhLUIQzFJpc+DY8RB3A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetectingState",
                table: "PairAntennas");

            migrationBuilder.DropColumn(
                name: "LastVerificationTimeStamp",
                table: "PairAntennas");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4a2b93a6-d044-43ba-ba1f-b740ae4b0a2f", "AQAAAAIAAYagAAAAECUyQJ1OZZS6mS1e8TU8qELpheH7TJNmSj+8H5aGa4R5jRP+gpqQHhW4nI8k8v0Ajg==" });
        }
    }
}
