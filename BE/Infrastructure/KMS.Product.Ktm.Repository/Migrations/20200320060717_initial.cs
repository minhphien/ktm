using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KMS.Product.Ktm.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    EmployeeBadgeId = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FirstMidName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SlackAccount = table.Column<string>(nullable: true),
                    JoinedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "KudoTypes",
                columns: table => new
                {
                    KudoTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KudoTypes", x => x.KudoTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTeams",
                columns: table => new
                {
                    EmployeeTeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    JoinedDate = table.Column<DateTime>(nullable: false),
                    ReleseadDate = table.Column<DateTime>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTeams", x => x.EmployeeTeamId);
                    table.ForeignKey(
                        name: "FK_EmployeeTeams_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTeams_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kudos",
                columns: table => new
                {
                    KudoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    SenderId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    KudoTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kudos", x => x.KudoId);
                    table.ForeignKey(
                        name: "FK_Kudos_KudoTypes_KudoTypeId",
                        column: x => x.KudoTypeId,
                        principalTable: "KudoTypes",
                        principalColumn: "KudoTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kudos_EmployeeTeams_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "EmployeeTeams",
                        principalColumn: "EmployeeTeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kudos_EmployeeTeams_SenderId",
                        column: x => x.SenderId,
                        principalTable: "EmployeeTeams",
                        principalColumn: "EmployeeTeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTeams_EmployeeID",
                table: "EmployeeTeams",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTeams_TeamID",
                table: "EmployeeTeams",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Kudos_KudoTypeId",
                table: "Kudos",
                column: "KudoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Kudos_ReceiverId",
                table: "Kudos",
                column: "ReceiverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kudos_SenderId",
                table: "Kudos",
                column: "SenderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kudos");

            migrationBuilder.DropTable(
                name: "KudoTypes");

            migrationBuilder.DropTable(
                name: "EmployeeTeams");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
