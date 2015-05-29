namespace efAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertable_roles_altercol_level : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "Level", c => c.Int());
        }
    }
}
