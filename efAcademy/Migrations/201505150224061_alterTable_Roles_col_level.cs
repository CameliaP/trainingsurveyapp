namespace efAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTable_Roles_col_level : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "Level", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "Level", c => c.String());
        }
    }
}
