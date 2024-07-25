using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    public partial class TagLogsNewFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Tag_TagId",
                table: "Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_TagId",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Log");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Log",
                newName: "Logs");

            migrationBuilder.AlterColumn<string>(
                name: "EPC",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegistered",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Logs",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "TagEPC",
                table: "Logs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "EPC");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Logs_TagEPC",
                table: "Logs",
                column: "TagEPC");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Tags_TagEPC",
                table: "Logs",
                column: "TagEPC",
                principalTable: "Tags",
                principalColumn: "EPC",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Tags_TagEPC",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_TagEPC",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DateRegistered",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagEPC",
                table: "Logs");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "Log");

            migrationBuilder.AlterColumn<string>(
                name: "EPC",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Log",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Log",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log",
                table: "Log",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Tag_TagId",
                table: "Log",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
