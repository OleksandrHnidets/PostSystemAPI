using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PostSystemAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPositionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58643345-cb60-47bf-b4f4-4ef22518edf1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60057d69-ab11-455b-96a0-ac5c5bd95885");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa9f5c9a-16d2-4aa3-9b4d-139173cebde5");*/

            migrationBuilder.DropColumn(
                name: "IsDriverOnline",
                table: "Positions");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Positions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Positions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "94cc5ac6-44b0-48e9-8571-d0cdbd69bba5", null, "Viewer", "VIEWER" },
                    { "97be6011-7514-431c-9d57-dddd3066925e", null, "Administrator", "ADMINISTRATOR" },
                    { "c47e9e41-aaee-40a4-b922-f9b4dc96b436", null, "Driver", "DRIVER" }
                });
                */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94cc5ac6-44b0-48e9-8571-d0cdbd69bba5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97be6011-7514-431c-9d57-dddd3066925e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c47e9e41-aaee-40a4-b922-f9b4dc96b436");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Positions");

            migrationBuilder.AddColumn<bool>(
                name: "IsDriverOnline",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58643345-cb60-47bf-b4f4-4ef22518edf1", null, "Driver", "DRIVER" },
                    { "60057d69-ab11-455b-96a0-ac5c5bd95885", null, "Administrator", "ADMINISTRATOR" },
                    { "fa9f5c9a-16d2-4aa3-9b4d-139173cebde5", null, "Viewer", "VIEWER" }
                });
        }
    }
}
