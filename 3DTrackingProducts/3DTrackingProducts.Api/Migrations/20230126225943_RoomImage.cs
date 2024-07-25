using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class RoomImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                table: "Rooms",
                newName: "imageByte");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "d42bb11d-3a99-4f15-aa6a-1e1713c60ace");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "dfe13fce-fe6d-4cda-a0b5-bb46887c6556");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e5c304f1-b4ef-47b7-94ef-93c42b223e8c", "AQAAAAEAACcQAAAAEHxuq7KicmagaNuZ4it50dEMR8FXk4/FMvzmtnP+06gqqjN58UBqqYsJvCmZvWKWRQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageByte",
                table: "Rooms",
                newName: "image");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "b01be288-2f72-4238-80fb-09f65d102a15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "4ad10de0-7c88-4ff2-8683-146d63813801");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "60c4b48b-8d7f-46ce-afa8-44dd5a4ccc9c", "AQAAAAEAACcQAAAAEBO99j2/IEk60QVTxFIEiP00iYlqhwi1VIbW+F4QLvlbPsIcmTjL9lGyZGH2U5Ak5A==" });
        }
    }
}
