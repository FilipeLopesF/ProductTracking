using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class Tag3DPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag3DPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagEPC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    x = table.Column<double>(type: "float", nullable: false),
                    y = table.Column<double>(type: "float", nullable: false),
                    z = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag3DPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag3DPositions_Tags_TagEPC",
                        column: x => x.TagEPC,
                        principalTable: "Tags",
                        principalColumn: "EPC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "87d49f9a-b8c6-4a9c-8094-fc42d0d73959");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "a3a31daf-d87f-43d1-8805-158f554b4a16");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6e518cd1-841e-4507-8d1f-d99bb2757b19", "AQAAAAEAACcQAAAAEKnUPG/LolYBLNUhF2OdEHR4EE6HlaO0fhsrh3IuC+jg1cHcJcrDGkexnvVpoeVCXQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Tag3DPositions_TagEPC",
                table: "Tag3DPositions",
                column: "TagEPC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag3DPositions");

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
