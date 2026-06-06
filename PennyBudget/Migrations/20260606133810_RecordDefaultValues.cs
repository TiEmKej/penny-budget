using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PennyBudget.Migrations
{
    /// <inheritdoc />
    public partial class RecordDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "FinancialRecords",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "date('now')",
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrencyRate",
                table: "FinancialRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: 1.0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "FinancialRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "PLN",
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "FinancialRecords",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "date('now')");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrencyRate",
                table: "FinancialRecords",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldDefaultValue: 1.0m);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "FinancialRecords",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "PLN");
        }
    }
}
