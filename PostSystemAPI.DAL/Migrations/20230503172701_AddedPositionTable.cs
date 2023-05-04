using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PostSystemAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedPositionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_ReceivedBy",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_SendedBy",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_PostOffices_PostOfficeId",
                table: "Deliveries");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a05bea5-ca0f-4e50-9cd3-8457a152586b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edb9b861-5295-4a77-8702-2105d528d2a3");

            migrationBuilder.RenameColumn(
                name: "SendedBy",
                table: "Deliveries",
                newName: "SendedUserId");

            migrationBuilder.RenameColumn(
                name: "ReceivedBy",
                table: "Deliveries",
                newName: "ReceivedUserId");

            migrationBuilder.RenameColumn(
                name: "PostOfficeId",
                table: "Deliveries",
                newName: "StartPostOfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_SendedBy",
                table: "Deliveries",
                newName: "IX_Deliveries_SendedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_ReceivedBy",
                table: "Deliveries",
                newName: "IX_Deliveries_ReceivedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_PostOfficeId",
                table: "Deliveries",
                newName: "IX_Deliveries_StartPostOfficeId");

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

            migrationBuilder.AddColumn<string>(
                name: "AssignedDriverId",
                table: "Deliveries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DestinationPostOfficeId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDriverOnline = table.Column<bool>(type: "bit", nullable: false),
                    CurrentDriverStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            /*
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73c20b95-eca7-4f96-ad73-55d561260f95", null, "Driver", "DRIVER" },
                    { "e1c46e0b-da5b-4466-9339-9ac00f1b94c2", null, "Administrator", "ADMINISTRATOR" },
                    { "ea78878a-3923-4776-aa50-1254c8a89eb7", null, "Viewer", "VIEWER" }
                });
            */
            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_AssignedDriverId",
                table: "Deliveries",
                column: "AssignedDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DestinationPostOfficeId",
                table: "Deliveries",
                column: "DestinationPostOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DeliveryId",
                table: "Positions",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_UserId",
                table: "Positions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_AssignedDriverId",
                table: "Deliveries",
                column: "AssignedDriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_ReceivedUserId",
                table: "Deliveries",
                column: "ReceivedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_SendedUserId",
                table: "Deliveries",
                column: "SendedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_PostOffices_DestinationPostOfficeId",
                table: "Deliveries",
                column: "DestinationPostOfficeId",
                principalTable: "PostOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_PostOffices_StartPostOfficeId",
                table: "Deliveries",
                column: "StartPostOfficeId",
                principalTable: "PostOffices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_AssignedDriverId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_ReceivedUserId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_SendedUserId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_PostOffices_DestinationPostOfficeId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_PostOffices_StartPostOfficeId",
                table: "Deliveries");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_AssignedDriverId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_DestinationPostOfficeId",
                table: "Deliveries");

            migrationBuilder.DeleteData(
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
                keyValue: "ea78878a-3923-4776-aa50-1254c8a89eb7");

            migrationBuilder.DropColumn(
                name: "ReceivedDeliveriesId",
                table: "PostOffices");

            migrationBuilder.DropColumn(
                name: "SentDeliveriesId",
                table: "PostOffices");

            migrationBuilder.DropColumn(
                name: "AssignedDriverId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "DestinationPostOfficeId",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "StartPostOfficeId",
                table: "Deliveries",
                newName: "PostOfficeId");

            migrationBuilder.RenameColumn(
                name: "SendedUserId",
                table: "Deliveries",
                newName: "SendedBy");

            migrationBuilder.RenameColumn(
                name: "ReceivedUserId",
                table: "Deliveries",
                newName: "ReceivedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_StartPostOfficeId",
                table: "Deliveries",
                newName: "IX_Deliveries_PostOfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_SendedUserId",
                table: "Deliveries",
                newName: "IX_Deliveries_SendedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_ReceivedUserId",
                table: "Deliveries",
                newName: "IX_Deliveries_ReceivedBy");
            /*
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a05bea5-ca0f-4e50-9cd3-8457a152586b", "023a25df-50a5-4053-83aa-309dbc4f148f", "Administrator", "ADMINISTRATOR" },
                    { "edb9b861-5295-4a77-8702-2105d528d2a3", "152de72d-4232-4f64-9a6a-adf5ffbdccbb", "Viewer", "VIEWER" }
                });
            */
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

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_PostOffices_PostOfficeId",
                table: "Deliveries",
                column: "PostOfficeId",
                principalTable: "PostOffices",
                principalColumn: "Id");
        }
    }
}
