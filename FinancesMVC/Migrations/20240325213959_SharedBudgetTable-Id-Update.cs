using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancesMVC.Migrations
{
    /// <inheritdoc />
    public partial class SharedBudgetTableIdUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__shared_addedUserId",
                table: "sharedBudget");

            migrationBuilder.DropIndex(
                name: "IX_sharedBudget_addedUserId",
                table: "sharedBudget");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "sharedBudget",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "addedUserId",
                table: "sharedBudget",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "sharedBudget",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_sharedBudget_OwnerId",
                table: "sharedBudget",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_sharedBudget_OwnerUser",
                table: "sharedBudget",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sharedBudget_OwnerUser",
                table: "sharedBudget");

            migrationBuilder.DropIndex(
                name: "IX_sharedBudget_OwnerId",
                table: "sharedBudget");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "sharedBudget");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "sharedBudget",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "addedUserId",
                table: "sharedBudget",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sharedBudget_addedUserId",
                table: "sharedBudget",
                column: "addedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK__shared_addedUserId",
                table: "sharedBudget",
                column: "addedUserId",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
