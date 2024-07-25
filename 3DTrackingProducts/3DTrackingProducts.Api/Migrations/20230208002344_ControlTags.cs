using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class ControlTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PairTagsControl");

            migrationBuilder.CreateTable(
                name: "ControlTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    X_Tag = table.Column<double>(type: "float", nullable: false),
                    Y_Tag = table.Column<double>(type: "float", nullable: false),
                    Origin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlTags", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "50ed1650-ab83-4adf-b3d1-bd32fc59541e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "5a07bea6-0a8d-4baf-b7e6-7ff9e86dd284");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a209b25d-6e64-4dba-bd48-faa751b5232d", "AQAAAAEAACcQAAAAEHeWPSSd2s4Th1rW6LUC70LH59lQ2UfgKzGIHrLmsP59j5PGwdP/4e+JCygpxM9tTw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlTags");

            migrationBuilder.CreateTable(
                name: "PairTagsControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    xTagA = table.Column<double>(type: "float", nullable: false),
                    xTagB = table.Column<double>(type: "float", nullable: false),
                    yTagA = table.Column<double>(type: "float", nullable: false),
                    yTagB = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PairTagsControl", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "f4e07a9c-7be5-4113-86e3-c42b87520191");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "011e3983-3fb1-43b1-9061-dd84427ed926");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ab983816-5ddc-4784-afa4-af0d81dc542f", "AQAAAAEAACcQAAAAECAO/FXclROl5fvaoxJe14sP5o7DJfOUuZjS5CdbRGq+J3lA/GnSF8tIzN/AySweCg==" });
        }
    }
}
