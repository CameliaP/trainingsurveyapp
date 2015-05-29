using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using efAcademy.Entities.Web;
using WebObjectMapping.Attributes;
using WebObjectMapping.Services;

namespace efAcademy.Entities.Csv
{
    
    public class CsvUnit
    {
        [MapsTo("Code")]
        [CsvColumn(Name = "Code")]
        public string Code { get; set; }
        
        [MapsTo("Title")]
        [CsvColumn(Name = "Title")]
        public string Title { get; set; }

    }
    public class CsvCourse {
        [MapsTo("Code")]
        [CsvColumn(Name = "Course Code")]
        public string Code { get; set; }
        
        [MapsTo("Title")]
        [CsvColumn(Name = "Course Title")]
        public string Title { get; set; }
    }
    public class CsvLocation
    {
        
        [MapsTo("Code")]
        [CsvColumn(Name = "Code")]
        public string Code { get; set; }
                
        [MapsTo("Title")]
        [CsvColumn(Name = "Title")]
        public string Title { get; set; }

    }
    public class CsvRole
    {
        
        [MapsTo("Id")]
        [CsvColumn(Name="Id")]
        public int Id { get; set; }
        [CsvColumn(Name="Title")]
        [MapsTo("Title")]
        public string Title { get; set; }
        [CsvColumn(Name="Level")]
        [MapsTo("Level")]
        public string Level { get; set; }
        
    }
    public class CsvProject
    {

        [MapsTo("Code")]
        [CsvColumn(Name = "Code")]
        public string Code { get; set; }

        [MapsTo("CustomerCode")]
        [CsvColumn(Name = "CustomerCode")]
        public string CustomerCode { get; set; }
    }
    public class CsvEmployee
        
    {
        [MapsTo("Id")][CsvColumn(Name="Id")]
        public int Id { get; set; }
        
        [MapsTo("Alias")][CsvColumn(Name = "Alias")]
        public string Alias { get; set; }
        
        [MapsTo("Email")][CsvColumn(Name = "Email")]
        public string Email { get; set; }

        [CsvColumn(Name = "Location_Code")]
        public string Location{ get; set; }

        [CsvColumn(Name = "Role_Id")]
        public int Role { get; set; }

        [CsvColumn(Name = "Project_Code")]
        public string  Project{ get; set; }

        [CsvColumn(Name="Unit")]
        public string Unit{ get; set; }
    }
    public class CsvTutor :CsvEmployee
    {
        
    }
    
    
    public class CsvTraining
    {
        
        [MapsTo("Id")]
        public int Id { get; set; }
        
        [MapsTo("Title")]
        public string Title { get; set; }
        
        [MapsTo("Start")]
        public DateTime Start { get; set; }
        
        [MapsTo("End")]
        public DateTime End { get; set; }
        
        public string Course { get; set; }
        
        public ICollection<int> Attendees { get; set; }
        
        public ICollection<int> Tutors { get; set; }
        
        public ICollection<int> FdbQuestions { get; set; }
    }
    
    public class CsvFdbCategory
    {
        
        [MapsTo("Code")]
        [CsvColumn(Name="Code")]
        public string Code { get; set; }
        
        [MapsTo("Title")]
        [CsvColumn(Name="Title")]
        public string Title { get; set; }
        
        
    }
    
    public class CsvFdbOption
    {
        [MapsTo("Id")]
        [CsvColumn(Name="Id")]
        public int Id { get; set; }

        [MapsTo("Text")]
        [CsvColumn(Name="Text")]
        public string Text { get; set; }

        [MapsTo("Sentiment")]
        [CsvColumn(Name = "SentimentValue")]
        public short Sentiment { get; set; }

        
    }
    
    public class CsvFdbQuestion
    {   //Text,Category_Code,Feedback_Id
        
        [MapsTo("Id")]
        [CsvColumn(Name = "Id")]
        public int Id { get; set; }
        
        [MapsTo("Text")]
        [CsvColumn(Name = "Text")]
        public string Text { get; set; }
        
        [CsvColumn(Name = "Category_Code")]
        public ICollection<string> Category { get; set; }
        [CsvColumn(Name="")]
        public ICollection<int> Options { get; set; }
        
    }
    
    public class CsvFdbResponse
    {
        
        [MapsTo("Id")]
        public int Id { get; set; }
        [CsvColumn(Name="Employee_Id")]
        public int Employee { get; set; }
        [CsvColumn(Name = "Training_Id")]
        public int Training { get; set; }
        [CsvColumn(Name = "Question_Id")]
        public int Question { get; set; }
        [CsvColumn(Name = "Option_Id")]
        public int Response { get; set; }
    }
}