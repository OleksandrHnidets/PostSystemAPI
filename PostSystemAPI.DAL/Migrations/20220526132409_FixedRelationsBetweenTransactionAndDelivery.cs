using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostSystemAPI.DAL.Migrations
{
    public partial class FixedRelationsBetweenTransactionAndDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_TransactionsHistory_TransactionHistoryId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_TransactionHistoryId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "TransactionHistoryId",
                table: "Deliveries");

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryId",
                table: "TransactionsHistory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_DeliveryId",
                table: "TransactionsHistory",
                column: "DeliveryId",
                unique: true,
                filter: "[DeliveryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Deliveries_DeliveryId",
                table: "TransactionsHistory",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Deliveries_DeliveryId",
                table: "TransactionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionsHistory_DeliveryId",
                table: "TransactionsHistory");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "TransactionsHistory");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionHistoryId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
