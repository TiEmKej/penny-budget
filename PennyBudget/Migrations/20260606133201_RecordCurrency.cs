using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PennyBudget.Migrations
{
    /// <inheritdoc />
    public partial class RecordCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "FinancialRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "FinancialRecords");
        }
    }
}
