using Microsoft.EntityFrameworkCore.Migrations;

namespace PostSystemAPI.DAL.Migrations
{
    public partial class RoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5432ce17-d8ed-47b3-8b24-5819aadb02f5", "eee18ead-cbbd-43c8-b9bc-17ffacfbd963", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "17911b6e-cca7-4d63-8789-eb0adec9be03", "137e3d75-67ab-4423-9584-b084dec90da5", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17911b6e-cca7-4d63-8789-eb0adec9be03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5432ce17-d8ed-47b3-8b24-5819aadb02f5");
        }
    }
}
