using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PennyBudget.Migrations
{
    /// <inheritdoc />
    public partial class IsExpenseFromCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpense",
                table: "FinancialRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpense",
                table: "FinancialRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
