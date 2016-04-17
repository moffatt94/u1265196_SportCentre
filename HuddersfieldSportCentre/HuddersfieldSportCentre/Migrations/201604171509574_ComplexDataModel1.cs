namespace HuddersfieldSportCentre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CourtAssignment", name: "TrainerID", newName: "CourseID");
            RenameIndex(table: "dbo.CourtAssignment", name: "IX_TrainerID", newName: "IX_CourseID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CourtAssignment", name: "IX_CourseID", newName: "IX_TrainerID");
            RenameColumn(table: "dbo.CourtAssignment", name: "CourseID", newName: "TrainerID");
        }
    }
}
