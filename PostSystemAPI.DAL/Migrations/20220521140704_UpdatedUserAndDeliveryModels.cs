using Microsoft.EntityFrameworkCore.Migrations;

namespace PostSystemAPI.DAL.Migrations
{
    public partial class UpdatedUserAndDeliveryModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryName",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReceivedBy",
                table: "Deliveries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SendedBy",
                table: "Deliveries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ReceivedBy",
                table: "Deliveries",
                column: "ReceivedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SendedBy",
                table: "Deliveries",
                column: "SendedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_ReceivedBy",
                table: "Deliveries",
                column: "ReceivedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_SendedBy",
                table: "Deliveries",
                column: "SendedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_ReceivedBy",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_SendedBy",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_ReceivedBy",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_SendedBy",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "ReceivedBy",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "SendedBy",
                table: "Deliveries");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryName",
                table: "Deliveries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
