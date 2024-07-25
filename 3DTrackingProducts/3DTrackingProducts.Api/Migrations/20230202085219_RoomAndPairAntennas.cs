using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class RoomAndPairAntennas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "antenna1XPosition",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "antenna2XPosition",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ipAddressAntenna1",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "roomWidth",
                table: "Rooms",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "roomLength",
                table: "Rooms",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "ipAddressAntenna2",
                table: "Rooms",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Antennas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    antenna01IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    antenna01X = table.Column<int>(type: "int", nullable: false),
                    antenna01Y = table.Column<int>(type: "int", nullable: false),
                    antenna02IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    antenna02X = table.Column<int>(type: "int", nullable: false),
                    antenna02Y = table.Column<int>(type: "int", nullable: false),
                    idRoom = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antennas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Antennas_Rooms_idRoom",
                        column: x => x.idRoom,
                        principalTable: "Rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Antennas_idRoom",
                table: "Antennas",
                column: "idRoom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Antennas");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Rooms",
                newName: "roomWidth");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Rooms",
                newName: "ipAddressAntenna2");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Rooms",
                newName: "roomLength");

            migrationBuilder.AddColumn<double>(
                name: "antenna1XPosition",
                table: "Rooms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "antenna2XPosition",
                table: "Rooms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ipAddressAntenna1",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
