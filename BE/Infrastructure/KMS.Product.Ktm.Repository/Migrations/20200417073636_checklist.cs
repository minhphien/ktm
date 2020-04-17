using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KMS.Product.Ktm.Repository.Migrations
{
    public partial class checklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MentorId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckListItems",
                columns: table => new
                {
                    CheckListItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Tilte = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListItems", x => x.CheckListItemId);
                    table.ForeignKey(
                        name: "FK_CheckListItems_Employees_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckListItems_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckListStatus",
                columns: table => new
                {
                    CheckListStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListStatus", x => x.CheckListStatusId);
                });

            migrationBuilder.CreateTable(
                name: "CheckListAssigns",
                columns: table => new
                {
                    CheckListAssignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    AssigneeId = table.Column<int>(nullable: false),
                    AssigneeComment = table.Column<string>(nullable: true),
                    MentorComment = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListAssigns", x => x.CheckListAssignId);
                    table.ForeignKey(
                        name: "FK_CheckListAssigns_Employees_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckListAssigns_CheckListStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "CheckListStatus",
                        principalColumn: "CheckListStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CheckListStatus",
                columns: new[] { "CheckListStatusId", "Created", "Modified", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "To Do" },
                    { 2, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In Progress" },
                    { 3, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Done" }
                });

            migrationBuilder.UpdateData(
                table: "EmployeeRoles",
                keyColumn: "EmployeeRoleId",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "KudoTypes",
                keyColumn: "KudoTypeId",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MentorId",
                table: "Employees",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListAssigns_AssigneeId",
                table: "CheckListAssigns",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListAssigns_StatusId",
                table: "CheckListAssigns",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItems_CreatorId",
                table: "CheckListItems",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItems_TeamId",
                table: "CheckListItems",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_MentorId",
                table: "Employees",
                column: "MentorId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_MentorId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "CheckListAssigns");

            migrationBuilder.DropTable(
                name: "CheckListItems");

            migrationBuilder.DropTable(
                name: "CheckListStatus");

            migrationBuilder.DropIndex(
                name: "IX_Employees_MentorId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MentorId",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "EmployeeRoles",
                keyColumn: "EmployeeRoleId",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2020, 4, 16, 15, 33, 8, 796, DateTimeKind.Local).AddTicks(2038), new DateTime(2020, 4, 16, 15, 33, 8, 796, DateTimeKind.Local).AddTicks(2509) });

            migrationBuilder.UpdateData(
                table: "KudoTypes",
                keyColumn: "KudoTypeId",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2020, 4, 16, 15, 33, 8, 797, DateTimeKind.Local).AddTicks(4060), new DateTime(2020, 4, 16, 15, 33, 8, 797, DateTimeKind.Local).AddTicks(4072) });
        }
    }
}
