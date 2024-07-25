using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDistanceHighToControlTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DistanceHigh",
                table: "Tag3DPositions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4a2b93a6-d044-43ba-ba1f-b740ae4b0a2f", "AQAAAAIAAYagAAAAECUyQJ1OZZS6mS1e8TU8qELpheH7TJNmSj+8H5aGa4R5jRP+gpqQHhW4nI8k8v0Ajg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceHigh",
                table: "Tag3DPositions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4b58f8f-6c40-411b-8a90-0e6b47c03ed9", "AQAAAAIAAYagAAAAEK4Z2IVexGHzXaipmCxbM2DLWld1rCwQycJMDrEBZUg+Q+JF/oH2GCYIgo5T13s3eQ==" });
        }
    }
}
