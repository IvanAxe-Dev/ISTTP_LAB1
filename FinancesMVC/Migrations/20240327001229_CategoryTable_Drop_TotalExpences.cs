using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancesMVC.Migrations
{
    /// <inheritdoc />
    public partial class CategoryTable_Drop_TotalExpences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalExpences",
                table: "categories");

            migrationBuilder.AlterColumn<bool>(
                name: "isMature",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isMature",
                table: "users",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<decimal>(
                name: "totalExpences",
                table: "categories",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
