using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PennyBudget.Migrations
{
    /// <inheritdoc />
    public partial class UniqueCategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RecordCategory_Name",
                table: "RecordCategory",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecordCategory_Name",
                table: "RecordCategory");
        }
    }
}
