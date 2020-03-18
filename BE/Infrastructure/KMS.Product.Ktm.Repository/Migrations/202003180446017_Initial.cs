namespace KMS.Product.Ktm.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        Email = c.String(),
                        SlackAccount = c.String(),
                        JoinedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.EmployeeTeam",
                c => new
                    {
                        EmployeeTeamID = c.Int(nullable: false, identity: true),
                        JoinedDate = c.DateTime(nullable: false),
                        ReleseadDate = c.DateTime(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        TeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeTeamID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.Kudo",
                c => new
                    {
                        KudoID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        SenderID = c.Int(nullable: false),
                        ReceiverID = c.Int(nullable: false),
                        KudoTypeID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KudoID)
                .ForeignKey("dbo.KudoType", t => t.KudoTypeID, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeTeam", t => t.ReceiverID)
                .ForeignKey("dbo.EmployeeTeam", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.ReceiverID)
                .Index(t => t.KudoTypeID);
            
            CreateTable(
                "dbo.KudoType",
                c => new
                    {
                        KudoTypeID = c.Int(nullable: false),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.KudoTypeID);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeTeam", "TeamID", "dbo.Team");
            DropForeignKey("dbo.Kudo", "SenderID", "dbo.EmployeeTeam");
            DropForeignKey("dbo.Kudo", "ReceiverID", "dbo.EmployeeTeam");
            DropForeignKey("dbo.Kudo", "KudoTypeID", "dbo.KudoType");
            DropForeignKey("dbo.EmployeeTeam", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.Kudo", new[] { "KudoTypeID" });
            DropIndex("dbo.Kudo", new[] { "ReceiverID" });
            DropIndex("dbo.Kudo", new[] { "SenderID" });
            DropIndex("dbo.EmployeeTeam", new[] { "TeamID" });
            DropIndex("dbo.EmployeeTeam", new[] { "EmployeeID" });
            DropTable("dbo.Team");
            DropTable("dbo.KudoType");
            DropTable("dbo.Kudo");
            DropTable("dbo.EmployeeTeam");
            DropTable("dbo.Employee");
        }
    }
}
