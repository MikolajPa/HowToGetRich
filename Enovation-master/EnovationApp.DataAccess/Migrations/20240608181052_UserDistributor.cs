using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnovationApp.DataAccess.Migrations
{
    public partial class UserDistributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "accountId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isDistributor",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "walletId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isDistributor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "walletId",
                table: "Users");
        }
    }
}
