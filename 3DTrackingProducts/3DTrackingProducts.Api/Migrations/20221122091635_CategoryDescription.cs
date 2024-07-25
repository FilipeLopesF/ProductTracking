using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class CategoryDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "ace4b3f5-9824-404b-a365-41491a1ad447");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "36b6e2e5-9e20-492f-9ee5-d1972eaf5de7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8af1d58a-0045-4a5d-8f58-ca4945ea9287", "AQAAAAEAACcQAAAAEH9FRjncLSSSfIzHNALgETKvUaMAzxJuiyGBwwFI7Yb74PFqVl1Fv153X+qCn8bP+Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CategoryId",
                table: "Tags",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Category_CategoryId",
                table: "Tags",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Category_CategoryId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CategoryId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Tags");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "08ed07ea-9367-47f8-8a9b-4f9bb908c09a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "9f5f10c3-d2e4-4c47-9773-56b376f36693");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a81ccc92-e314-440d-9b9b-356bf83d3e2a", "AQAAAAEAACcQAAAAEGVlga4eIxQ7OFWQt98tB09TCMbHhZ9U0SKijo1UYsF1UwX3ranH9lhao8s4nF0zVQ==" });
        }
    }
}
