using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class TagsLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EPC = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RSSI = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Angle = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "9851a310-a52c-4ef7-9ce6-275ac64d3d05");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "f6b946b2-73ca-40ba-a890-fd46b53943e2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4fc2906e-7e44-4918-8397-058c34795781", "AQAAAAEAACcQAAAAEG+ad2BFj3Wi1TI+QGLUtMdI45a4Xnx5ZyarQsXSau2dQtnhGvHhB2mW5TeF3ql7iQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Log_TagId",
                table: "Log",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "7cc70d78-1ebf-46cc-9671-7f0d76a583d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "fc323545-de0a-4477-a41d-7608f1d9d339");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3c0ebe19-a50d-4322-9bb2-2ad7714282e9", "AQAAAAEAACcQAAAAECAI2XUZaChataN12ZZLqsDm9dtM68THAT33IBy/B99A8rDYWOvU6FsAkuMJMVhlUw==" });
        }
    }
}
