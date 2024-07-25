using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class Rooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roomLength = table.Column<double>(type: "float", nullable: false),
                    roomWidth = table.Column<double>(type: "float", nullable: false),
                    ipAddressAntenna1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    antenna1XPosition = table.Column<double>(type: "float", nullable: false),
                    ipAddressAntenna2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    antenna2XPosition = table.Column<double>(type: "float", nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "f0961011-cbe6-4dda-a1a5-26ffb02fc941");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "681872ce-69c9-406f-8ab3-a87f3945d498");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6bd9b106-ae00-4866-9f52-5777c2086208", "AQAAAAEAACcQAAAAENK5+7YxY973a0RY0WiIi4EpWyDB04iRYacBZ9+ghBkmHPyNjyCVWBg1XH42dN6V6Q==" });
        }
    }
}
