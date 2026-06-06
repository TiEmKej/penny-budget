using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PennyBudget.Migrations
{
    /// <inheritdoc />
    public partial class RecordCurrencyRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "FinancialRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "FinancialRecords");
        }
    }
}
