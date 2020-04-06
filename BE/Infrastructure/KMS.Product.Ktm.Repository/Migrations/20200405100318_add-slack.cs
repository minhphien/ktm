using Microsoft.EntityFrameworkCore.Migrations;

namespace KMS.Product.Ktm.Repository.Migrations
{
    public partial class addslack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SlackEmoji",
                table: "KudoDetails",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SlackUserId",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlackEmoji",
                table: "KudoDetails");

            migrationBuilder.DropColumn(
                name: "SlackUserId",
                table: "Employees");
        }
    }
}
