using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomIdOnControlTagTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "ControlTags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4b58f8f-6c40-411b-8a90-0e6b47c03ed9", "AQAAAAIAAYagAAAAEK4Z2IVexGHzXaipmCxbM2DLWld1rCwQycJMDrEBZUg+Q+JF/oH2GCYIgo5T13s3eQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_ControlTags_RoomId",
                table: "ControlTags",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlTags_Rooms_RoomId",
                table: "ControlTags",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlTags_Rooms_RoomId",
                table: "ControlTags");

            migrationBuilder.DropIndex(
                name: "IX_ControlTags_RoomId",
                table: "ControlTags");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "ControlTags");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "995ed1ed-f6f1-426b-b38f-2c4450f406be", "AQAAAAIAAYagAAAAEJzoZwNRRcXgMZ0J8kYlw4W+r2OAEDa4PicT3THs727bhqjQq8ffK3Qc2/rGj7jylA==" });
        }
    }
}
