namespace HuddersfieldSportCentre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Title = c.String(),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        Skills = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        EnrollmentDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollment", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropIndex("dbo.Enrollment", new[] { "CustomerID" });
            DropIndex("dbo.Enrollment", new[] { "CourseID" });
            DropTable("dbo.Customer");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Course");
        }
    }
}
