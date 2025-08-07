﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Addnotetotransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Transactions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Transactions");
        }
    }
}
