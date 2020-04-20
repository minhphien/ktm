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
                name: "CheckLists",
                columns: table => new
                {
                    CheckListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.CheckListId);
                    table.ForeignKey(
                        name: "FK_CheckLists_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Tilte = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Employees_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "CheckListItems",
                columns: table => new
                {
                    CheckListItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    CheckListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListItems", x => x.CheckListItemId);
                    table.ForeignKey(
                        name: "FK_CheckListItems_CheckLists_CheckListId",
                        column: x => x.CheckListId,
                        principalTable: "CheckLists",
                        principalColumn: "CheckListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckListItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_Employees_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentItems",
                columns: table => new
                {
                    AssignmentItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    AssigneeComment = table.Column<string>(nullable: true),
                    MentorComment = table.Column<string>(nullable: true),
                    CheckListItemId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    AssignmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentItems", x => x.AssignmentItemId);
                    table.ForeignKey(
                        name: "FK_AssignmentItems_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentItems_CheckListItems_CheckListItemId",
                        column: x => x.CheckListItemId,
                        principalTable: "CheckListItems",
                        principalColumn: "CheckListItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentItems_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "Created", "Modified", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "To Do" },
                    { 2, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In Progress" },
                    { 3, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Done" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MentorId",
                table: "Employees",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentItems_AssignmentId",
                table: "AssignmentItems",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentItems_CheckListItemId",
                table: "AssignmentItems",
                column: "CheckListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentItems_StatusId",
                table: "AssignmentItems",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssigneeId",
                table: "Assignments",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_StatusId",
                table: "Assignments",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItems_CheckListId",
                table: "CheckListItems",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItems_ItemId",
                table: "CheckListItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_TeamId",
                table: "CheckLists",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatorId",
                table: "Items",
                column: "CreatorId");

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
                name: "AssignmentItems");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "CheckListItems");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "CheckLists");

            migrationBuilder.DropTable(
                name: "Items");

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
