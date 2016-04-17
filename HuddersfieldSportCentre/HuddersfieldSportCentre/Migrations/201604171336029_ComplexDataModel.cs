namespace HuddersfieldSportCentre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        TrainerID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Trainer", t => t.TrainerID)
                .Index(t => t.TrainerID);
            
            CreateTable(
                "dbo.Trainer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        StartedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourtAssignment",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Trainer", t => t.CourseID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.CourseTrainer",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        TrainerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.TrainerID })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Trainer", t => t.TrainerID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.TrainerID);

           
            Sql("INSERT INTO dbo.Department (Name, Budget, StartDate) VALUES ('Temp', 0.00, GETDATE())");
            
            AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false, defaultValue: 1)); 
           // AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "LastName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Course", "DepartmentID");
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseTrainer", "TrainerID", "dbo.Trainer");
            DropForeignKey("dbo.CourseTrainer", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Department", "TrainerID", "dbo.Trainer");
            DropForeignKey("dbo.CourtAssignment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropIndex("dbo.CourseTrainer", new[] { "TrainerID" });
            DropIndex("dbo.CourseTrainer", new[] { "CourseID" });
            DropIndex("dbo.CourtAssignment", new[] { "TrainerID" });
            DropIndex("dbo.Department", new[] { "TrainerID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            AlterColumn("dbo.Customer", "LastName", c => c.String(maxLength: 50));
            DropColumn("dbo.Course", "DepartmentID");
            DropTable("dbo.CourseTrainer");
            DropTable("dbo.CourtAssignment");
            DropTable("dbo.Trainer");
            DropTable("dbo.Department");
        }
    }
}
