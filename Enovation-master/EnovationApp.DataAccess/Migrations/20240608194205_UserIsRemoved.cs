using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnovationApp.DataAccess.Migrations
{
    public partial class UserIsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "walletId",
                table: "Users",
                newName: "WalletId");

            migrationBuilder.RenameColumn(
                name: "isDistributor",
                table: "Users",
                newName: "IsDistributor");

            migrationBuilder.RenameColumn(
                name: "accountId",
                table: "Users",
                newName: "AccountId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Users",
                newName: "walletId");

            migrationBuilder.RenameColumn(
                name: "IsDistributor",
                table: "Users",
                newName: "isDistributor");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Users",
                newName: "accountId");
        }
    }
}
