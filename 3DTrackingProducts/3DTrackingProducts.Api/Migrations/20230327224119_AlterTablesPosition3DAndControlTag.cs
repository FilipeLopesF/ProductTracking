using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterTablesPosition3DAndControlTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ControlTags",
                table: "ControlTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ControlTags");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "ControlTags");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "TagPositions",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "z",
                table: "Tag3DPositions",
                newName: "RelativePosZ");

            migrationBuilder.RenameColumn(
                name: "y",
                table: "Tag3DPositions",
                newName: "RelativePosY");

            migrationBuilder.RenameColumn(
                name: "x",
                table: "Tag3DPositions",
                newName: "RelativePosX");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Tag3DPositions",
                newName: "DateTimeRegistered");

            migrationBuilder.RenameColumn(
                name: "Y_Tag",
                table: "ControlTags",
                newName: "PositionY");

            migrationBuilder.RenameColumn(
                name: "X_Tag",
                table: "ControlTags",
                newName: "PositionX");

            migrationBuilder.AddColumn<Guid>(
                name: "PairAntennaId",
                table: "TagPositions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ControlTagEPCLeft",
                table: "Tag3DPositions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ControlTagEPCRight",
                table: "Tag3DPositions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "DistanceLeft",
                table: "Tag3DPositions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DistanceRight",
                table: "Tag3DPositions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "EPC",
                table: "ControlTags",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControlTags",
                table: "ControlTags",
                column: "EPC");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "995ed1ed-f6f1-426b-b38f-2c4450f406be", "AQAAAAIAAYagAAAAEJzoZwNRRcXgMZ0J8kYlw4W+r2OAEDa4PicT3THs727bhqjQq8ffK3Qc2/rGj7jylA==" });

            migrationBuilder.CreateIndex(
                name: "IX_TagPositions_PairAntennaId",
                table: "TagPositions",
                column: "PairAntennaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag3DPositions_ControlTagEPCLeft",
                table: "Tag3DPositions",
                column: "ControlTagEPCLeft");

            migrationBuilder.CreateIndex(
                name: "IX_Tag3DPositions_ControlTagEPCRight",
                table: "Tag3DPositions",
                column: "ControlTagEPCRight");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag3DPositions_ControlTags_ControlTagEPCLeft",
                table: "Tag3DPositions",
                column: "ControlTagEPCLeft",
                principalTable: "ControlTags",
                principalColumn: "EPC");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag3DPositions_ControlTags_ControlTagEPCRight",
                table: "Tag3DPositions",
                column: "ControlTagEPCRight",
                principalTable: "ControlTags",
                principalColumn: "EPC");

            migrationBuilder.AddForeignKey(
                name: "FK_TagPositions_PairAntennas_PairAntennaId",
                table: "TagPositions",
                column: "PairAntennaId",
                principalTable: "PairAntennas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag3DPositions_ControlTags_ControlTagEPCLeft",
                table: "Tag3DPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag3DPositions_ControlTags_ControlTagEPCRight",
                table: "Tag3DPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_TagPositions_PairAntennas_PairAntennaId",
                table: "TagPositions");

            migrationBuilder.DropIndex(
                name: "IX_TagPositions_PairAntennaId",
                table: "TagPositions");

            migrationBuilder.DropIndex(
                name: "IX_Tag3DPositions_ControlTagEPCLeft",
                table: "Tag3DPositions");

            migrationBuilder.DropIndex(
                name: "IX_Tag3DPositions_ControlTagEPCRight",
                table: "Tag3DPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ControlTags",
                table: "ControlTags");

            migrationBuilder.DropColumn(
                name: "PairAntennaId",
                table: "TagPositions");

            migrationBuilder.DropColumn(
                name: "ControlTagEPCLeft",
                table: "Tag3DPositions");

            migrationBuilder.DropColumn(
                name: "ControlTagEPCRight",
                table: "Tag3DPositions");

            migrationBuilder.DropColumn(
                name: "DistanceLeft",
                table: "Tag3DPositions");

            migrationBuilder.DropColumn(
                name: "DistanceRight",
                table: "Tag3DPositions");

            migrationBuilder.DropColumn(
                name: "EPC",
                table: "ControlTags");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "TagPositions",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "RelativePosZ",
                table: "Tag3DPositions",
                newName: "z");

            migrationBuilder.RenameColumn(
                name: "RelativePosY",
                table: "Tag3DPositions",
                newName: "y");

            migrationBuilder.RenameColumn(
                name: "RelativePosX",
                table: "Tag3DPositions",
                newName: "x");

            migrationBuilder.RenameColumn(
                name: "DateTimeRegistered",
                table: "Tag3DPositions",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "PositionY",
                table: "ControlTags",
                newName: "Y_Tag");

            migrationBuilder.RenameColumn(
                name: "PositionX",
                table: "ControlTags",
                newName: "X_Tag");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ControlTags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "Origin",
                table: "ControlTags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControlTags",
                table: "ControlTags",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                column: "ConcurrencyStamp",
                value: "158e2138-51af-46de-85cf-34136ebbe80e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                column: "ConcurrencyStamp",
                value: "546c2279-956c-421e-b034-2ff4edbd03b6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f2d3ca9f-fc88-4130-aa4f-ba05f61efebc", "AQAAAAEAACcQAAAAECPr/pTVV4qVKfnOmLOAeR9+HH4Z0oJZEyvHFL2LfYSXpotBz3Kn1FjwFWB5ZaSYLw==" });
        }
    }
}
