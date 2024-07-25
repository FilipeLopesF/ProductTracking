using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class Update_ControlTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ControlTags",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "158e2138-51af-46de-85cf-34136ebbe80e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "546c2279-956c-421e-b034-2ff4edbd03b6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f2d3ca9f-fc88-4130-aa4f-ba05f61efebc", "AQAAAAEAACcQAAAAECPr/pTVV4qVKfnOmLOAeR9+HH4Z0oJZEyvHFL2LfYSXpotBz3Kn1FjwFWB5ZaSYLw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ControlTags");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "91c0a191-3584-4174-b966-38b98181e90d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "53254d31-6dd7-4cc1-9597-d3f45bccb547");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "42b19eb5-5fa6-4c17-b44e-47b66fa46379", "AQAAAAEAACcQAAAAED4caoVV9TTwCfXCDZbdH/Oslc0l5SWlsJFDNm8q4iByVWAKihW5ghhAScakzf50SA==" });
        }
    }
}
