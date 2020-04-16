using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KMS.Product.Ktm.Repository.Migrations
{
    public partial class activeteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Teams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "EmployeeRoles",
                columns: new[] { "EmployeeRoleId", "Created", "Modified", "RoleName" },
                values: new object[] { 1, new DateTime(2020, 4, 16, 15, 33, 8, 796, DateTimeKind.Local).AddTicks(2038), new DateTime(2020, 4, 16, 15, 33, 8, 796, DateTimeKind.Local).AddTicks(2509), "Default" });

            migrationBuilder.InsertData(
                table: "KudoTypes",
                columns: new[] { "KudoTypeId", "Created", "Modified", "TypeName" },
                values: new object[] { 1, new DateTime(2020, 4, 16, 15, 33, 8, 797, DateTimeKind.Local).AddTicks(4060), new DateTime(2020, 4, 16, 15, 33, 8, 797, DateTimeKind.Local).AddTicks(4072), "Default" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumn: "EmployeeRoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "KudoTypes",
                keyColumn: "KudoTypeId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Teams");
        }
    }
}
