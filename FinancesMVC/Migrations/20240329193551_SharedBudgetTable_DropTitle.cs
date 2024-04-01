using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancesMVC.Migrations
{
    /// <inheritdoc />
    public partial class SharedBudgetTable_DropTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "sharedBudget");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "sharedBudget",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
