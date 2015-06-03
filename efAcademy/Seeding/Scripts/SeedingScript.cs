using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using efAcademy.Entities;
using efAcademy.Context;
using System.Diagnostics;
using efAcademy.Entities.Web;
using efAcademy.Entities.Csv;
using WebObjectMapping.Services;
using WebObjectMapping.Attributes;

namespace efAcademy.Seeding.Scripts
{
    
    public static class Cleanup
    {
        public static void Courses()
        {
            //removing all the courses  from the database
            using (academyContext db = new academyContext())
            {
                Trace.WriteLine(String.Format("Clearing off the {0} course from the database", db.Courses.Count()));
                db.Courses.RemoveRange(db.Courses);
                db.SaveChanges();
                Trace.WriteLine(String.Format("Courses cleared from the database"));
            }
        }
        public static void Units()
        {
            using (academyContext db = new academyContext())
            {
                Trace.WriteLine(String.Format("Clearing off the {0} units from the database", db.Units.Count()));
                db.Units.RemoveRange(db.Units);
                db.SaveChanges();
                Trace.WriteLine(String.Format("Units cleared from the database"));
            }
        }
        public static void Locations()
        {
            using (academyContext db = new academyContext())
            {
                Trace.WriteLine(String.Format("Clearing off the {0} locations from the database", db.Locations.Count()));
                db.Locations.RemoveRange(db.Locations);
                db.SaveChanges();
                Trace.WriteLine(String.Format("locations cleared from the database"));
            }
        }
        public static void Projects()
        {
            using (academyContext db = new academyContext())
            {
                Trace.WriteLine(String.Format("Clearing off the {0} projects from the database", db.Projects.Count()));
                db.Projects.RemoveRange(db.Projects);
                db.SaveChanges();
                Trace.WriteLine(String.Format("projects cleared from the database"));
            }
        }
        public static void FdbQuestions()
        {
            using (academyContext db = new academyContext())
            {
                Trace.WriteLine(String.Format("Clearing off the {0} fdbquestions from the database", db.FdbQuestions.Count()));
                db.FdbQuestions.RemoveRange(db.FdbQuestions);
                db.SaveChanges();
                Trace.WriteLine(String.Format("fdbquestions cleared from the database"));
            }
        }
        public static void FdbCategories()
        {
            using (academyContext db = new academyContext())
            {
                Trace.WriteLine(String.Format("Clearing off the {0} fdbcategories from the database", db.FdbCategories.Count()));
                db.FdbCategories.RemoveRange(db.FdbCategories);
                db.SaveChanges();
                Trace.WriteLine(String.Format("fdbcategories cleared from the database"));
            }
        }
        public static void FdbOptions()
        {
            using (academyContext db = new academyContext())
            {
                Trace.WriteLine(String.Format("Clearing off the {0} FdbOptions from the database", db.FdbOptions.Count()));
                db.FdbOptions.RemoveRange(db.FdbOptions);
                db.SaveChanges();
                Trace.WriteLine(String.Format("FdbOptions cleared from the database"));
            }
        }
        public static void Employees()
        {
            using (academyContext db = new academyContext())
            {
                Trace.WriteLine(String.Format("Clearing off the {0} employees from the database", db.Employees.Count()));
                db.Employees.RemoveRange(db.Employees);
                db.SaveChanges();
                Trace.WriteLine(String.Format("employees cleared from the database"));
            }
        }
        public static void Trainings()
        {
            using (academyContext db = new academyContext())
            {
                db.Trainings.RemoveRange(db.Trainings);
                db.SaveChanges();
            }
        }

        public static void Roles()
        {
            using (academyContext db = new academyContext())
            {
                db.Roles.RemoveRange(db.Roles);
                db.SaveChanges();
            }
        }
    }
    public static class Upload
    {

        private static CsvFileDescription fileDescription = new CsvFileDescription()
        {
            SeparatorChar = ',',
            FirstLineHasColumnNames = true,
            IgnoreUnknownColumns = true
        };
        private static CsvContext context = new CsvContext();

        public static void Units()
        {
            //not sure if the file location works
            string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\units.csv";
            IEnumerable<CsvUnit> units = context.Read<CsvUnit>(csvLocation, fileDescription);
            if (units == null || units.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            using (academyContext db = new academyContext())
            {
                //getting only those not in the database
                IEnumerable<CsvUnit> notInDb = units
                    .Where(x => db.Units
                        .Where(y => y.Code == x.Code).Count() == 0)
                    .Select(z => z);

                Array.ForEach(notInDb.ToArray<CsvUnit>(), wu =>
                {
                    Unit u = Mapping.ToPoco<Unit, CsvUnit>(wu);
                    if (u != null) { db.Units.Add(u); db.SaveChanges(); }
                });
            }
        }
        public static void Courses()
        {
            string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\courses.csv";
            IEnumerable<CsvCourse> courses = context.Read<CsvCourse>(csvLocation, fileDescription);
            if (courses == null || courses.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            using (academyContext db = new academyContext())
            {
                //getting all the courses not already in the database
                IEnumerable<CsvCourse> coursesNotInDb = courses.Where(x => db.Courses.Where(y => y.Code == x.Code).Count() == 0);
                Array.ForEach(coursesNotInDb.ToArray<CsvCourse>(), wc =>
                {
                    //converting to poco 
                    Course c = Mapping.ToPoco<Course, CsvCourse>(wc);
                    if (c != null) { db.Courses.Add(c); db.SaveChanges(); }//adding the course to base
                }); //saving as a bulk transaction, persistance
            }
        }
        public static void Roles()
        {
            string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\roles.csv";
            IEnumerable<CsvRole> courses = context.Read<CsvRole>(csvLocation, fileDescription);
            if (courses == null || courses.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            using (academyContext db = new academyContext())
            {
                //getting all the courses not already in the database
                IEnumerable<CsvRole> coursesNotInDb = courses.Where(x =>
                    db.Courses.Where(y => y.Title == x.Title).Count() == 0);
                Array.ForEach(coursesNotInDb.ToArray<CsvRole>(), wc =>
                {
                    //converting to poco 
                    Role c = Mapping.ToPoco<Role, CsvRole>(wc);
                    if (c != null) { db.Roles.Add(c); db.SaveChanges(); }//adding the course to base
                }); //saving as a bulk transaction, persistance
            }
        }
        public static void Projects()
        {
            //not sure if the file location works
            string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\projects.csv";
            IEnumerable<CsvProject> projects = context.Read<CsvProject>(csvLocation, fileDescription);
            if (projects == null || projects.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            using (academyContext db = new academyContext())
            {
                IEnumerable<CsvProject> notInDb = projects.Where(x => db.Projects.Where(y => y.Code == x.Code).Count() == 0);
                //pushing all the projects into the database
                Array.ForEach(notInDb.ToArray<CsvProject>(), wp =>
                {
                    Project p = Mapping.ToPoco<Project, CsvProject>(wp);
                    if (p != null) { db.Projects.Add(p); db.SaveChanges(); }
                });
            }
        }
        public static void Employees()
        {
            //not sure if the file location works
            string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\employees.csv";
            IEnumerable<CsvEmployee> employees = context.Read<CsvEmployee>(csvLocation, fileDescription);
            if (employees == null || employees.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            using (academyContext db = new academyContext())
            {
                IEnumerable<CsvEmployee> noInDb = employees.Where(x => db.Employees.Where(y => y.Id == x.Id).Count() == 0);
                Array.ForEach(noInDb.ToArray<CsvEmployee>(), we =>
                {
                    Employee e = Mapping.ToPoco<Employee, CsvEmployee>(we);
                    if (e != null)
                    {
                        e.Location = db.Locations.Where(l => l.Code == we.Location).FirstOrDefault();
                        //e.Role = db.Roles.Where(r => r.Id == we.Role).FirstOrDefault();
                        e.Unit = db.Units.Where(u => u.Code == we.Unit).FirstOrDefault();
                        e.Project = db.Projects.Where(p => p.Code == we.Project).FirstOrDefault();
                        db.Employees.Add(e);
                        db.SaveChanges();
                    }
                });
            }
            //}
            //public static void RunAll()
            //{
            //    Courses();
            //    Locations();
            //    Projects();
            //    Units();
            //    FdbCategories();
            //    FdbOptions();

            //}
            ///// <summary>
            ///// Uploads all the courses from the csv location
            ///// </summary>


            //public static void Locations()
            //{
            //    //not sure if the file location works
            //    string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\locations.csv";
            //    IEnumerable<WebLocation> locations = context.Read<WebLocation>(csvLocation, fileDescription);
            //    Trace.WriteLine(String.Format("Finished reading the csv file  .."));
            //    if (locations == null || locations.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            //    using (academyContext db = new academyContext())
            //    {
            //        //getting locations that are not in Db
            //        IEnumerable<WebLocation> notInDb = locations.Where(x => db.Locations.Where(y => y.Code == x.Code).Count() == 0);

            //        Array.ForEach(notInDb.ToArray<WebLocation>(), wl =>
            //        {
            //            Location l = Mapping.ToPoco<Location, WebLocation>(wl);
            //            if (l != null) { db.Locations.Add(l); db.SaveChanges(); }
            //        });

            //        Trace.WriteLine(string.Format("Added {0} locations to the database", db.Locations.Count()));
            //    }
            //}

            //public static void FdbQuestions()
            //{
            //    //not sure if the file location works
            //    string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\fdbquestions.csv";
            //    IEnumerable<WebFdbQuestion> questions = context.Read<WebFdbQuestion>(csvLocation, fileDescription);
            //    if (questions == null || questions.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            //    using (academyContext db = new academyContext())
            //    {
            //        IEnumerable<WebFdbQuestion> notInDb = questions.Where(x => db.FdbQuestions.Where(y => y.Text == x.Text).Count() == 0);

            //        Array.ForEach(notInDb.ToArray<WebFdbQuestion>(), wq =>
            //        {
            //            FdbQuestion q = Mapping.ToPoco<FdbQuestion, WebFdbQuestion>(wq);

            //            if (q != null) {
            //                q.Category = db.FdbCategories.Where(x => x.Code == wq.Category).FirstOrDefault();
            //                db.FdbQuestions.Add(q); db.SaveChanges(); 
            //            }
            //        });
            //    }
            //}
            //public static void FdbCategories()
            //{
            //    //not sure if the file location works
            //    string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\fdbcategories.csv";
            //    IEnumerable<WebFdbCategory> categories = context.Read<WebFdbCategory>(csvLocation, fileDescription);
            //    if (categories == null || categories.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            //    using (academyContext db = new academyContext())
            //    {
            //        IEnumerable<WebFdbCategory> notInDb = categories.Where(x => db.FdbCategories.Where(y => y.Code == x.Code).Count() == 0);
            //        Array.ForEach(categories.ToArray<WebFdbCategory>(), wc =>
            //        {
            //            FdbCategory c = Mapping.ToPoco<FdbCategory, WebFdbCategory>(wc);
            //            if (c != null) { db.FdbCategories.Add(c); db.SaveChanges(); }
            //        });
            //    }
            //}
            //public static void FdbOptions()
            //{
            //    //not sure if the file location works
            //    string csvLocation = "D:\\coderunjun.visualstudio.com\\trainingsurvey\\efAcademy\\Seeding\\fdboptions.csv";
            //    IEnumerable<WebFdbOption> options = context.Read<WebFdbOption>(csvLocation, fileDescription);
            //    if (options == null || options.Count() == 0) { Trace.WriteLine("Either the file is empty or could not locate the file"); }
            //    using (academyContext db = new academyContext())
            //    {
            //        IEnumerable<WebFdbOption> notInDb = options.Where(x => db.FdbOptions.Where(y => y.Text == x.Text).Count() == 0);
            //        Array.ForEach(notInDb.ToArray<WebFdbOption>(), wo =>
            //        {
            //            FdbOption o = Mapping.ToPoco<FdbOption, WebFdbOption>(wo);
            //            if (o != null)
            //            {
            //                db.FdbOptions.Add(o); db.SaveChanges();
            //            }
            //        });
            //    }
            //}


        }
    }
}
