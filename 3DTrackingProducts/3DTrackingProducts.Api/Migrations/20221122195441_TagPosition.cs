using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class TagPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Category_CategoryId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TagPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagEPC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    x = table.Column<double>(type: "float", nullable: false),
                    y = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagPositions_Tags_TagEPC",
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

            migrationBuilder.CreateIndex(
                name: "IX_TagPositions_TagEPC",
                table: "TagPositions",
                column: "TagEPC");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Categories_CategoryId",
                table: "Tags",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Categories_CategoryId",
                table: "Tags");


            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropTable(
      name: "TagPositions");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "554ed139-2699-4257-8b03-48a6d7b50bb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "c9a13341-7c3c-4140-842a-08d135fa16ab");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f4f55c24-2a63-4199-8f91-0877e84c86af", "AQAAAAEAACcQAAAAEJIqjFsvgS1HVPfWJoybCrx/Ixs1TsYo7hX0i3eAfeI06yCSOpyp+2cowHQRrEYi3w==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Category_CategoryId",
                table: "Tags",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
