using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Removeiconfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "TEXT",
                nullable: true);
        }
    }
}
