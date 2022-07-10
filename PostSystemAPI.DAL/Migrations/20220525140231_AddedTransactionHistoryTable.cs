using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostSystemAPI.DAL.Migrations
{
    public partial class AddedTransactionHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionHistoryId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransactionsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsHistory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_TransactionHistoryId",
                table: "Deliveries",
                column: "TransactionHistoryId",
                unique: true,
                filter: "[TransactionHistoryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_TransactionsHistory_TransactionHistoryId",
                table: "Deliveries",
                column: "TransactionHistoryId",
                principalTable: "TransactionsHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_TransactionsHistory_TransactionHistoryId",
                table: "Deliveries");

            migrationBuilder.DropTable(
                name: "TransactionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_TransactionHistoryId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "TransactionHistoryId",
                table: "Deliveries");

        }
    }
}
