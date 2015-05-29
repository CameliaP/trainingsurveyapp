namespace efAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtables_initialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 50),
                        Title = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Alias = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        Location_Code = c.String(maxLength: 50),
                        Project_Code = c.String(maxLength: 50),
                        Role_Title = c.String(maxLength: 255),
                        Unit_Code = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Code)
                .ForeignKey("dbo.Projects", t => t.Project_Code)
                .ForeignKey("dbo.Roles", t => t.Role_Title)
                .ForeignKey("dbo.Units", t => t.Unit_Code)
                .Index(t => t.Location_Code)
                .Index(t => t.Project_Code)
                .Index(t => t.Role_Title)
                .Index(t => t.Unit_Code);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Course_Code = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Code)
                .Index(t => t.Course_Code);
            
            CreateTable(
                "dbo.FdbQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 255),
                        Category_Code = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FdbCategories", t => t.Category_Code)
                .Index(t => t.Category_Code);
            
            CreateTable(
                "dbo.FdbCategories",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 50),
                        Title = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.FdbOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 255),
                        Sentiment = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 50),
                        Title = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 50),
                        CustomerCode = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Title = c.String(nullable: false, maxLength: 255),
                        Level = c.String(),
                    })
                .PrimaryKey(t => t.Title);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 50),
                        Title = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.FdbResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Employee_Id = c.Int(),
                        Question_Id = c.Int(),
                        Response_Id = c.Int(),
                        Training_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.FdbQuestions", t => t.Question_Id)
                .ForeignKey("dbo.FdbOptions", t => t.Response_Id)
                .ForeignKey("dbo.Trainings", t => t.Training_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.Response_Id)
                .Index(t => t.Training_Id);
            
            CreateTable(
                "dbo.TrainingEmployees",
                c => new
                    {
                        Training_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Training_Id, t.Employee_Id })
                .ForeignKey("dbo.Trainings", t => t.Training_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Training_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.FdbOptionFdbQuestions",
                c => new
                    {
                        FdbOption_Id = c.Int(nullable: false),
                        FdbQuestion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FdbOption_Id, t.FdbQuestion_Id })
                .ForeignKey("dbo.FdbOptions", t => t.FdbOption_Id, cascadeDelete: true)
                .ForeignKey("dbo.FdbQuestions", t => t.FdbQuestion_Id, cascadeDelete: true)
                .Index(t => t.FdbOption_Id)
                .Index(t => t.FdbQuestion_Id);
            
            CreateTable(
                "dbo.FdbQuestionTrainings",
                c => new
                    {
                        FdbQuestion_Id = c.Int(nullable: false),
                        Training_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FdbQuestion_Id, t.Training_Id })
                .ForeignKey("dbo.FdbQuestions", t => t.FdbQuestion_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.Training_Id, cascadeDelete: true)
                .Index(t => t.FdbQuestion_Id)
                .Index(t => t.Training_Id);
            
            CreateTable(
                "dbo.TutorTrainings",
                c => new
                    {
                        Tutor_Id = c.Int(nullable: false),
                        Training_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tutor_Id, t.Training_Id })
                .ForeignKey("dbo.Tutors", t => t.Tutor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.Training_Id, cascadeDelete: true)
                .Index(t => t.Tutor_Id)
                .Index(t => t.Training_Id);
            
            CreateTable(
                "dbo.Tutors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tutors", "Id", "dbo.Employees");
            DropForeignKey("dbo.FdbResponses", "Training_Id", "dbo.Trainings");
            DropForeignKey("dbo.FdbResponses", "Response_Id", "dbo.FdbOptions");
            DropForeignKey("dbo.FdbResponses", "Question_Id", "dbo.FdbQuestions");
            DropForeignKey("dbo.FdbResponses", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Unit_Code", "dbo.Units");
            DropForeignKey("dbo.Employees", "Role_Title", "dbo.Roles");
            DropForeignKey("dbo.Employees", "Project_Code", "dbo.Projects");
            DropForeignKey("dbo.Employees", "Location_Code", "dbo.Locations");
            DropForeignKey("dbo.TutorTrainings", "Training_Id", "dbo.Trainings");
            DropForeignKey("dbo.TutorTrainings", "Tutor_Id", "dbo.Tutors");
            DropForeignKey("dbo.FdbQuestionTrainings", "Training_Id", "dbo.Trainings");
            DropForeignKey("dbo.FdbQuestionTrainings", "FdbQuestion_Id", "dbo.FdbQuestions");
            DropForeignKey("dbo.FdbOptionFdbQuestions", "FdbQuestion_Id", "dbo.FdbQuestions");
            DropForeignKey("dbo.FdbOptionFdbQuestions", "FdbOption_Id", "dbo.FdbOptions");
            DropForeignKey("dbo.FdbQuestions", "Category_Code", "dbo.FdbCategories");
            DropForeignKey("dbo.Trainings", "Course_Code", "dbo.Courses");
            DropForeignKey("dbo.TrainingEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.TrainingEmployees", "Training_Id", "dbo.Trainings");
            DropIndex("dbo.Tutors", new[] { "Id" });
            DropIndex("dbo.TutorTrainings", new[] { "Training_Id" });
            DropIndex("dbo.TutorTrainings", new[] { "Tutor_Id" });
            DropIndex("dbo.FdbQuestionTrainings", new[] { "Training_Id" });
            DropIndex("dbo.FdbQuestionTrainings", new[] { "FdbQuestion_Id" });
            DropIndex("dbo.FdbOptionFdbQuestions", new[] { "FdbQuestion_Id" });
            DropIndex("dbo.FdbOptionFdbQuestions", new[] { "FdbOption_Id" });
            DropIndex("dbo.TrainingEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.TrainingEmployees", new[] { "Training_Id" });
            DropIndex("dbo.FdbResponses", new[] { "Training_Id" });
            DropIndex("dbo.FdbResponses", new[] { "Response_Id" });
            DropIndex("dbo.FdbResponses", new[] { "Question_Id" });
            DropIndex("dbo.FdbResponses", new[] { "Employee_Id" });
            DropIndex("dbo.FdbQuestions", new[] { "Category_Code" });
            DropIndex("dbo.Trainings", new[] { "Course_Code" });
            DropIndex("dbo.Employees", new[] { "Unit_Code" });
            DropIndex("dbo.Employees", new[] { "Role_Title" });
            DropIndex("dbo.Employees", new[] { "Project_Code" });
            DropIndex("dbo.Employees", new[] { "Location_Code" });
            DropTable("dbo.Tutors");
            DropTable("dbo.TutorTrainings");
            DropTable("dbo.FdbQuestionTrainings");
            DropTable("dbo.FdbOptionFdbQuestions");
            DropTable("dbo.TrainingEmployees");
            DropTable("dbo.FdbResponses");
            DropTable("dbo.Units");
            DropTable("dbo.Roles");
            DropTable("dbo.Projects");
            DropTable("dbo.Locations");
            DropTable("dbo.FdbOptions");
            DropTable("dbo.FdbCategories");
            DropTable("dbo.FdbQuestions");
            DropTable("dbo.Trainings");
            DropTable("dbo.Employees");
            DropTable("dbo.Courses");
        }
    }
}
