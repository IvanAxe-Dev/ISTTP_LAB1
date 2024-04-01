using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancesMVC.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTable_CascadeDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__transacti__expen__4BAC3F29",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "FK__transacti__expen__4BAC3F29",
                table: "transactions",
                column: "expenditureCategoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__transacti__expen__4BAC3F29",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "FK__transacti__expen__4BAC3F29",
                table: "transactions",
                column: "expenditureCategoryId",
                principalTable: "categories",
                principalColumn: "id");
        }
    }
}
