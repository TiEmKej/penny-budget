using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PennyBudget.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountAndBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialRecords_RecordCategory_CategoryId",
                table: "FinancialRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecordCategory",
                table: "RecordCategory");

            migrationBuilder.RenameTable(
                name: "RecordCategory",
                newName: "RecordCategories");

            migrationBuilder.RenameColumn(
                name: "IsCash",
                table: "FinancialRecords",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_RecordCategory_Name",
                table: "RecordCategories",
                newName: "IX_RecordCategories_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordCategories",
                table: "RecordCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "PLN")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    LimitAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "PLN"),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_RecordCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "RecordCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecords_AccountId",
                table: "FinancialRecords",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId_Month_Year",
                table: "Budgets",
                columns: new[] { "CategoryId", "Month", "Year" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialRecords_Accounts_AccountId",
                table: "FinancialRecords",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialRecords_RecordCategories_CategoryId",
                table: "FinancialRecords",
                column: "CategoryId",
                principalTable: "RecordCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialRecords_Accounts_AccountId",
                table: "FinancialRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialRecords_RecordCategories_CategoryId",
                table: "FinancialRecords");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_FinancialRecords_AccountId",
                table: "FinancialRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecordCategories",
                table: "RecordCategories");

            migrationBuilder.RenameTable(
                name: "RecordCategories",
                newName: "RecordCategory");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "FinancialRecords",
                newName: "IsCash");

            migrationBuilder.RenameIndex(
                name: "IX_RecordCategories_Name",
                table: "RecordCategory",
                newName: "IX_RecordCategory_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordCategory",
                table: "RecordCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialRecords_RecordCategory_CategoryId",
                table: "FinancialRecords",
                column: "CategoryId",
                principalTable: "RecordCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
