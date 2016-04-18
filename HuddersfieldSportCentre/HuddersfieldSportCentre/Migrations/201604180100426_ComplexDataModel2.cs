namespace HuddersfieldSportCentre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourtAssignment", "CourseID", "dbo.Trainer");
            DropIndex("dbo.CourtAssignment", new[] { "CourseID" });
            DropColumn("dbo.Course", "Level");
            DropColumn("dbo.Enrollment", "Skills");
            DropTable("dbo.CourtAssignment");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourtAssignment",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.CourseID);
            
            AddColumn("dbo.Enrollment", "Skills", c => c.Int());
            AddColumn("dbo.Course", "Level", c => c.Int(nullable: false));
            CreateIndex("dbo.CourtAssignment", "CourseID");
            AddForeignKey("dbo.CourtAssignment", "CourseID", "dbo.Trainer", "ID");
        }
    }
}
