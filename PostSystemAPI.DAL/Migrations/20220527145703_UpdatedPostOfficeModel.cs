using Microsoft.EntityFrameworkCore.Migrations;

namespace PostSystemAPI.DAL.Migrations
{
    public partial class UpdatedPostOfficeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "PostOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostOfficeBalance",
                table: "PostOffices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "PostOffices");

            migrationBuilder.DropColumn(
                name: "PostOfficeBalance",
                table: "PostOffices");

        }
    }
}
