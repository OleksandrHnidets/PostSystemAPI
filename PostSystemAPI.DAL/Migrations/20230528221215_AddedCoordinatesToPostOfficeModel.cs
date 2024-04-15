using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PostSystemAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedCoordinatesToPostOfficeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DeleteData(
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
                keyValue: "c47e9e41-aaee-40a4-b922-f9b4dc96b436");*/

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "PostOffices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "PostOffices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "115b8828-2c8e-4144-b1ad-8cd798c2f66a", null, "Administrator", "ADMINISTRATOR" },
                    { "90085b47-5160-41bc-89f2-fbe3caf1ad2d", null, "Driver", "DRIVER" },
                    { "f62fedc9-8383-49a9-b842-7406cad76a7d", null, "Viewer", "VIEWER" }
                });*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "115b8828-2c8e-4144-b1ad-8cd798c2f66a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90085b47-5160-41bc-89f2-fbe3caf1ad2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f62fedc9-8383-49a9-b842-7406cad76a7d");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "PostOffices");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "PostOffices");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "94cc5ac6-44b0-48e9-8571-d0cdbd69bba5", null, "Viewer", "VIEWER" },
                    { "97be6011-7514-431c-9d57-dddd3066925e", null, "Administrator", "ADMINISTRATOR" },
                    { "c47e9e41-aaee-40a4-b922-f9b4dc96b436", null, "Driver", "DRIVER" }
                });
        }
    }
}
