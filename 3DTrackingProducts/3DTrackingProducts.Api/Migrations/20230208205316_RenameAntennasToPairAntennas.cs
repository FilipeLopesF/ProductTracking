using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class RenameAntennasToPairAntennas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Antennas_Rooms_idRoom",
                table: "Antennas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Antennas",
                table: "Antennas");

            migrationBuilder.RenameTable(
                name: "Antennas",
                newName: "PairAntennas");

            migrationBuilder.RenameIndex(
                name: "IX_Antennas_idRoom",
                table: "PairAntennas",
                newName: "IX_PairAntennas_idRoom");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PairAntennas",
                table: "PairAntennas",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "f78614d0-e0d5-4cde-a03e-0be67548962b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "4c802810-e82f-46f1-891e-2885830e82aa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "177456bc-57b5-4bf4-96c4-f136e53754aa", "AQAAAAEAACcQAAAAEDiqHR0Z8ozBgbrXV8DH0kjviJjT2Q9Dq/7CaVAru6imnuUfCuf/DcdpaewJMQlhhQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_PairAntennas_Rooms_idRoom",
                table: "PairAntennas",
                column: "idRoom",
                principalTable: "Rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PairAntennas_Rooms_idRoom",
                table: "PairAntennas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PairAntennas",
                table: "PairAntennas");

            migrationBuilder.RenameTable(
                name: "PairAntennas",
                newName: "Antennas");

            migrationBuilder.RenameIndex(
                name: "IX_PairAntennas_idRoom",
                table: "Antennas",
                newName: "IX_Antennas_idRoom");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Antennas",
                table: "Antennas",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "9b90d3ea-a42f-4dab-9cba-2d489cc2d682");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "e31e8bcb-4d09-4fe3-a9da-cd139ea097f7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "98b1e41b-3e91-41b9-87e9-954b5a865b62", "AQAAAAEAACcQAAAAEBDBnwRqU3JUcQSauvhMJgtZVjkfHofQAHXM7upL78rukSdrv2WDrUoYSSqFz47BSA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Antennas_Rooms_idRoom",
                table: "Antennas",
                column: "idRoom",
                principalTable: "Rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
