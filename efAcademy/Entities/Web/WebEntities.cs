using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using WebObjectMapping.Attributes;
using WebObjectMapping.Services;
namespace efAcademy.Entities.Web
{
    [DataContract]
    public class WebUnit
    {
        [DataMember]
        [HateoasRoute("unit")]
        public string url { get; set; }

        [DataMember][MapsTo("Code")][CsvColumn(Name = "Code")][HatesoasRouteToken]
        public string Code { get; set; }
        
        [DataMember][MapsTo("Title")][CsvColumn(Name = "Title")]
        public string Title { get; set; }

        [DataMember]
        [HateoasRoute("unitToEmployees")]
        public string urlEmployees { get; set; }
    }
    //all the entities here are the ones that get serialized
    [DataContract]
    public class WebCourse
    {
        [DataMember]
        [HateoasRoute("course")]
        public string url { get; set; }
        [DataMember]
        [MapsTo("Code")]
        [HatesoasRouteToken]
        public string Code { get; set; }
        [DataMember]
        [MapsTo("Title")]
        
        public string Title { get; set; }

        [DataMember]
        [HateoasRoute("courseToTrainings")]
        public string Trainings { get; set; }
    }
    [DataContract]
    public class WebEmployee
        
    {
        [DataMember]
        [HateoasRoute("employee")]
        public string url { get; set; }

        [DataMember]
        [MapsTo("Id")]
        [HatesoasRouteToken]
        public int Id { get; set; }
        [DataMember]
        [MapsTo("Alias")]
        
        public string Alias { get; set; }
        [DataMember]
        [MapsTo("Email")]
        
        public string Email { get; set; }

        //foreign keys
        [DataMember]
        [HateoasRoute("employeeToLocation")]
        //[CsvColumn(Name = "Location_Code")]
        public string urlLocation{ get; set; }

        [DataMember]
        [HateoasRoute("employeeToRole")]
        public string urlRole { get; set; }
        
        [DataMember]
        [HateoasRoute("employeeToProject")]
        public string  urlProject{ get; set; }
        
        [DataMember]
        [HateoasRoute("employeeToUnit")]
        public string urlUnit{ get; set; }
    }

    [DataContract]
    public class WebTutor :WebEmployee
    {
        
    }
    [DataContract]
    public class WebLocation
    {
        [DataMember]
        [HateoasRoute("location")]
        public string url { get; set; }
        [DataMember()]
        [MapsTo("Code")]
        [CsvColumn(Name = "Code")]
        [HatesoasRouteToken]
        public string Code { get; set; }
        
        [DataMember]
        [MapsTo("Title")]
        
        public string Title { get; set; }

        [DataMember]
        [HateoasRoute("locationToEmployees")]
        public string Employees { get; set; }
    }
    [DataContract]
    
    public class WebRole
    {
        public WebRole() {
            
        }
        [DataMember]
        [HateoasRoute("rolesOfTitle")]
        public string url { get; set; }
        
        [DataMember]
        [MapsTo("Title")]
        [HatesoasRouteToken]
        public string Title { get; set; }
        
        [DataMember]
        [MapsTo("Level")]
        public int Level { get; set; }
        
        [DataMember]
        [HateoasRoute("roleToEmployees")]
        public string urlEmployees { get; set; }

    }
    
   
    [DataContract]
    public class WebProject
    {
        [DataMember]
        [HateoasRoute("project")]
        public string url { get; set; }
        [DataMember]
        [MapsTo("Code")]
        [HatesoasRouteToken]
        public string Code { get; set; }
        [DataMember]
        [MapsTo("CustomerCode")]
        public string CustomerCode { get; set; }

        [DataMember]
        [HateoasRoute("projectToEmployees")]
        public string urlEmployees { get; set; }
    }
    [DataContract]
    public class WebTraining
    {
        [DataMember]
        [HateoasRoute("training")]
        public string url { get; set; }
        [DataMember]
        [MapsTo("Id")]
        [HatesoasRouteToken]
        public int Id { get; set; }
        [DataMember]
        [MapsTo("Title")]
        public string Title { get; set; }
        [DataMember]
        [MapsTo("Start")]
        public DateTime Start { get; set; }
        [DataMember]
        [MapsTo("End")]
        public DateTime End { get; set; }
        [DataMember]
        [HateoasRoute("trainingToCourse")]
        public string Course { get; set; }
        [DataMember]
        [HateoasRoute("trainingToEmployees")]
        public string Attendees { get; set; }
        [DataMember]
        [HateoasRoute("trainingToTutors")]
        public string Tutors { get; set; }
        [DataMember]
        [HateoasRoute("trainingToQuestions")]
        public string FdbQuestions { get; set; }
    }
    [DataContract]
    public class WebFdbCategory
    {
        [DataMember]
        [HateoasRoute("category")]
        public string url { get; set; }
        [DataMember]
        [MapsTo("Code")]
        [HatesoasRouteToken]
        public string Code { get; set; }
        [DataMember]
        [MapsTo("Title")]
        
        public string Title { get; set; }
        [DataMember]
        [HateoasRoute("categoryToQuestions")]
        public string Questions { get; set; }
    }
    [DataContract]
    public class WebFdbOption
    {
        [DataMember]
        [HateoasRoute("option")]
        public string url { get; set; }
     
        [DataMember]
        [MapsTo("Id")]
        [HatesoasRouteToken]
        public int Id { get; set; }

        [DataMember]
        [MapsTo("Text")]
        
        public string Text { get; set; }

        [DataMember]
        [MapsTo("Sentiment")]
        
        public short Sentiment { get; set; }

        [DataMember]
        [HateoasRoute("optionToQuestions")]
        public string Questions { get; set; }
    }
    [DataContract]
    public class WebFdbQuestion
    {   //Text,Category_Code,Feedback_Id
        [DataMember]
        [HateoasRoute("question")]
        public string url { get; set; }
        [DataMember]
        [MapsTo("Id")]
        [HatesoasRouteToken]
        public int Id { get; set; }
        [DataMember]
        [MapsTo("Text")]
        
        public string Text { get; set; }
        [DataMember]
        [HateoasRoute("questionToCategory")]
        public string Category { get; set; }
        [DataMember]
        [HateoasRoute("questionToOptions")]
        public string Options { get; set; }
        [DataMember]
        [HateoasRoute("questionToTrainings")]
        public string Trainings { get; set; }
    }
    [DataContract]        
    public class WebFdbResponse
    {
        [DataMember]
        [HateoasRoute("response")]
        public string url { get; set; }
        [DataMember]
        [MapsTo("Id")]
        [HatesoasRouteToken]
        public int Id { get; set; }
        [DataMember]
        [HateoasRoute("responseToEmployee")]
        public string Employee { get; set; }
        [DataMember]
        [HateoasRoute("responseToTraining")]
        public string Training { get; set; }
        [DataMember]
        [HateoasRoute("responseToQuestion")]
        public string Question { get; set; }
        [DataMember]
        public int Response { get; set; }
    }
}