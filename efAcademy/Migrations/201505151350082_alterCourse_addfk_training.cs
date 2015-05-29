namespace efAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterCourse_addfk_training : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TrainingEmployees", newName: "EmployeeTrainings");
            DropPrimaryKey("dbo.EmployeeTrainings");
            AddPrimaryKey("dbo.EmployeeTrainings", new[] { "Employee_Id", "Training_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.EmployeeTrainings");
            AddPrimaryKey("dbo.EmployeeTrainings", new[] { "Training_Id", "Employee_Id" });
            RenameTable(name: "dbo.EmployeeTrainings", newName: "TrainingEmployees");
        }
    }
}
