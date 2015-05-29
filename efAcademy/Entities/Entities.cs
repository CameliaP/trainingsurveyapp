using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LINQtoCSV;

namespace efAcademy.Entities
{
    [Table("Units")]
    public class Unit
    {
        [Key]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        //navigation properties
        public virtual ICollection<Employee> Employees { get; set; }
    }
    [Table("Roles")]
    public class Role
    {
        [Key]
        [StringLength(255)]
        public string Title { get; set; }
        public int Level { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
    [Table("Projects")]
    public class Project
    {
        [Key]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(255)]
        public string CustomerCode { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
    [Table("Locations")]
    public class Location
    {
        [StringLength(50)]
        [Key]
        public string Code { get; set; }
        [StringLength(255)]

        public string Title { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
    [Table("FdbOptions")]
    public class FdbOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(255)]
        public string Text { get; set; }
        public short Sentiment { get; set; }

        public virtual ICollection<FdbQuestion> Questions { get; set; }
    }
    [Table("FdbCategories")]
    public class FdbCategory
    {
        [Key]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(255)]
        public string Title { get; set; }

        public virtual ICollection<FdbQuestion> Questions { get; set; }
    }
    [Table("Courses")]
    public class Course
    {
        [Key]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(255)]
        public string Title { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }
    [Table("FdbQuestions")]
    public class FdbQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(255)]
        public string Text { get; set; }
        //navigation properties
        public virtual FdbCategory Category { get; set; }
        public virtual ICollection<FdbOption> Options { get; set; }
        public virtual ICollection<Training> Trainings { get; set; }

    }
    [Table("FdbResponses")]
    public class FdbResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Training Training { get; set; }

        public virtual FdbQuestion Question { get; set; }

        public virtual FdbOption Response { get; set; }

    }
    public class Training
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Employee> Attendees { get; set; }
        public virtual ICollection<Tutor> Tutors { get; set; }

        public virtual ICollection<FdbQuestion> Feedback { get; set; }

    }
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(255)]
        public string Alias { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        //navigation properties
        public virtual Location Location { get; set; }
        public virtual Role Role { get; set; }
        public virtual Project Project { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Training> Attended { get; set; }

    }
    [Table("Tutors")]
    public class Tutor : Employee
    {
        public virtual ICollection<Training> Anchored { get; set; }
    }
}