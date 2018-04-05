using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SFMForFraudTransactions.Data.Migrations
{
    public partial class TransactionBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldBalance",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "NewBalanceDestination",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewBalanceOrigin",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldBalanceDestination",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldBalanceOrigin",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewBalanceDestination",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "NewBalanceOrigin",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OldBalanceDestination",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OldBalanceOrigin",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "OldBalance",
                table: "Customers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
