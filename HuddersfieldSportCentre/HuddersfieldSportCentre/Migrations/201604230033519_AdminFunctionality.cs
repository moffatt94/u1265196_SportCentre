namespace HuddersfieldSportCentre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminFunctionality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Admin");
        }
    }
}
