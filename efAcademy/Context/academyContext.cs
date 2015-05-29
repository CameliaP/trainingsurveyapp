using efAcademy.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efAcademy.Context
{
    public class academyContext :DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project>Projects { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Training> Trainings { get; set; }
        
        public DbSet<Course> Courses{ get; set; }
        public DbSet<FdbOption> FdbOptions { get; set; }
        public DbSet<FdbCategory> FdbCategories { get; set; }
        public DbSet<FdbQuestion> FdbQuestions { get; set; }
        public DbSet<FdbResponse> FdbResponses { get; set; }
    }
}
