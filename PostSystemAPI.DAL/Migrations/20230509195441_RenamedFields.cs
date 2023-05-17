using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PostSystemAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_SendedUserId",
                table: "Deliveries");

           /* migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73c20b95-eca7-4f96-ad73-55d561260f95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1c46e0b-da5b-4466-9339-9ac00f1b94c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea78878a-3923-4776-aa50-1254c8a89eb7");*/

            migrationBuilder.DropColumn(
                name: "ReceivedDeliveriesId",
                table: "PostOffices");

            migrationBuilder.DropColumn(
                name: "SentDeliveriesId",
                table: "PostOffices");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "PostOffices",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "SendedUserId",
                table: "Deliveries",
                newName: "SentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_SendedUserId",
                table: "Deliveries",
                newName: "IX_Deliveries_SentUserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Deliveries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58643345-cb60-47bf-b4f4-4ef22518edf1", null, "Driver", "DRIVER" },
                    { "60057d69-ab11-455b-96a0-ac5c5bd95885", null, "Administrator", "ADMINISTRATOR" },
                    { "fa9f5c9a-16d2-4aa3-9b4d-139173cebde5", null, "Viewer", "VIEWER" }
                });*/

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_SentUserId",
                table: "Deliveries",
                column: "SentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_SentUserId",
                table: "Deliveries");

            migrationBuilder.DeleteData(
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
                keyValue: "fa9f5c9a-16d2-4aa3-9b4d-139173cebde5");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "PostOffices",
                newName: "Adress");

            migrationBuilder.RenameColumn(
                name: "SentUserId",
                table: "Deliveries",
                newName: "SendedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_SentUserId",
                table: "Deliveries",
                newName: "IX_Deliveries_SendedUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceivedDeliveriesId",
                table: "PostOffices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SentDeliveriesId",
                table: "PostOffices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73c20b95-eca7-4f96-ad73-55d561260f95", null, "Driver", "DRIVER" },
                    { "e1c46e0b-da5b-4466-9339-9ac00f1b94c2", null, "Administrator", "ADMINISTRATOR" },
                    { "ea78878a-3923-4776-aa50-1254c8a89eb7", null, "Viewer", "VIEWER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_SendedUserId",
                table: "Deliveries",
                column: "SendedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
