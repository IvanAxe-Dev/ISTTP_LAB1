using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancesMVC.Migrations
{
    /// <inheritdoc />
    public partial class UserTable_DependentTablesKeys_OnCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__categorie__userI__4D94879B",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK__stats__userId__4CA06362",
                table: "stats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Users",
                table: "userProfiles");

            migrationBuilder.AddForeignKey(
                name: "FK__categorie__userI__4D94879B",
                table: "categories",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__stats__userId__4CA06362",
                table: "stats",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Users",
                table: "userProfiles",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__categorie__userI__4D94879B",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK__stats__userId__4CA06362",
                table: "stats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Users",
                table: "userProfiles");

            migrationBuilder.AddForeignKey(
                name: "FK__categorie__userI__4D94879B",
                table: "categories",
                column: "userId",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__stats__userId__4CA06362",
                table: "stats",
                column: "userId",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Users",
                table: "userProfiles",
                column: "userId",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
