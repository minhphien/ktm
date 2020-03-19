namespace KMS.Product.Ktm.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeBadgeId = c.String(),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        Email = c.String(),
                        SlackAccount = c.String(),
                        JoinedDate = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeTeams",
                c => new
                    {
                        EmployeeTeamId = c.Int(nullable: false, identity: true),
                        JoinedDate = c.DateTime(nullable: false),
                        ReleseadDate = c.DateTime(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        TeamID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeTeamId)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.Kudoes",
                c => new
                    {
                        KudoId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        KudoTypeId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KudoId)
                .ForeignKey("dbo.KudoTypes", t => t.KudoTypeId, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeTeams", t => t.ReceiverId)
                .ForeignKey("dbo.EmployeeTeams", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.KudoTypeId);
            
            CreateTable(
                "dbo.KudoTypes",
                c => new
                    {
                        KudoTypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KudoTypeId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeTeams", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Kudoes", "SenderId", "dbo.EmployeeTeams");
            DropForeignKey("dbo.Kudoes", "ReceiverId", "dbo.EmployeeTeams");
            DropForeignKey("dbo.Kudoes", "KudoTypeId", "dbo.KudoTypes");
            DropForeignKey("dbo.EmployeeTeams", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Kudoes", new[] { "KudoTypeId" });
            DropIndex("dbo.Kudoes", new[] { "ReceiverId" });
            DropIndex("dbo.Kudoes", new[] { "SenderId" });
            DropIndex("dbo.EmployeeTeams", new[] { "TeamID" });
            DropIndex("dbo.EmployeeTeams", new[] { "EmployeeID" });
            DropTable("dbo.Teams");
            DropTable("dbo.KudoTypes");
            DropTable("dbo.Kudoes");
            DropTable("dbo.EmployeeTeams");
            DropTable("dbo.Employees");
        }
    }
}
