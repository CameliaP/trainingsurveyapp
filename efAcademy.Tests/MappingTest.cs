using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using efAcademy.Entities;
using efAcademy.Entities.Web;
using System.Diagnostics;

namespace efAcademy.Tests
{
    [TestClass]
    public class MappingTest
    {
        [TestMethod]
        public void CoursesTest() {
            WebCourse[] webCourses = new WebCourse[] { 
                new WebCourse(){Code ="Code1", Title="Sample title 1"},
                new WebCourse(){Code ="Code2", Title="Sample title 2"},
                new WebCourse(){Code ="Code3", Title="Sample title 3"},
                new WebCourse(){Code ="Code4", Title="Sample title 4"},
                new WebCourse(){Code ="Code5", Title="Sample title 5"},
            };
            Array.ForEach(webCourses, w =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for course code: {0} title: {1}", w.Code, w.Title));
                Course c = Mapping.ToPoco<Course, WebCourse>(w);
                Assert.IsNotNull(c, "result object was found to be null");
                Assert.IsTrue(c.Code == w.Code, "the code is not matching");
                Assert.IsTrue(c.Title == w.Title, "title of the course is not matching");
            });
            Course[] courses = new Course[] { 
                new Course(){Code ="Code1", Title="Sample title 1"},
                new Course(){Code ="Code2", Title="Sample title 2"},
                new Course(){Code ="Code3", Title="Sample title 3"},
                new Course(){Code ="Code4", Title="Sample title 4"},
                new Course(){Code ="Code5", Title="Sample title 5"},
            };
            Array.ForEach(courses, c =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Web conversion test for course code: {0} title: {1}", c.Code, c.Title));
                WebCourse w = Mapping.ToWeb<Course, WebCourse>(c);
                Assert.IsNotNull(w, "result object was found to be null");
                Assert.IsTrue(w.Code == c.Code, "the code is not matching");
                Assert.IsTrue(w.Title == c.Title, "title of the course is not matching");
            });
            Trace.WriteLine(String.Format("End of test"));

        }

        [TestMethod]
        public void UnitsTests() { 
            //test for conversion to poco
            WebUnit[] wunits = new WebUnit[] { 
                new WebUnit(){Code =  "Unit1", Title = "Unit1"},
                new WebUnit(){Code =  "Unit2", Title = "Unit2"},
                new WebUnit(){Code =  "Unit3", Title = "Unit3"},
                new WebUnit(){Code =  "Unit4", Title = "Unit4"},
                new WebUnit(){Code =  "Unit5", Title = "Unit5"},
                new WebUnit(){Code =  "Unit6", Title = "Unit6"},
            };
            Trace.WriteLine(String.Format("testing for poco conversion"));
            Array.ForEach(wunits, wu =>{
                Trace.WriteLine(String.Format("Test for webunit: {0}", wu.Title));
                Unit u = Mapping.ToPoco<Unit, WebUnit>(wu);
                Assert.IsNotNull(u, String.Format("unit coversion failed, result is null"));
                Assert.IsTrue(u.Code == wu.Code, string.Format("unit conversion failed, code property is not matched"));
                Assert.IsTrue(u.Title== wu.Title, string.Format("unit conversion failed, title property not mappped"));
            });
            Unit[] units= new Unit[] { 
                new Unit(){Code =  "Unit1", Title = "Unit1"},
                new Unit(){Code =  "Unit2", Title = "Unit2"},
                new Unit(){Code =  "Unit3", Title = "Unit3"},
                new Unit(){Code =  "Unit4", Title = "Unit4"},
                new Unit(){Code =  "Unit5", Title = "Unit5"},
                new Unit(){Code =  "Unit6", Title = "Unit6"},
            };
            Trace.WriteLine(String.Format("testing for webo conversion"));
            Array.ForEach(units, u =>
            {
                Trace.WriteLine(String.Format("Test for unit: {0}", u.Title));
                WebUnit wu = Mapping.ToWeb<Unit, WebUnit>(u);
                Assert.IsNotNull(wu, String.Format("unit coversion failed, result is null"));
                Assert.IsTrue(u.Code == wu.Code, string.Format("unit conversion failed, code property is not matched"));
                Assert.IsTrue(u.Title == wu.Title, string.Format("unit conversion failed, title property not mappped"));
            });
            Trace.WriteLine(String.Format("End of test"));
        }

        [TestMethod]
        public void LocationsTest() {
            WebLocation[] webLocations = new WebLocation[] { 
                new WebLocation(){Code="SampleCode1", Title= "Sample location 1"},
                new WebLocation(){Code="SampleCode2", Title= "Sample location 2"},
                new WebLocation(){Code="SampleCode3", Title= "Sample location 3"},
                new WebLocation(){Code="SampleCode4", Title= "Sample location 4"},
                new WebLocation(){Code="SampleCode5", Title= "Sample location 5"},
                new WebLocation(){Code="SampleCode6", Title= "Sample location 6"},
            };
            Array.ForEach(webLocations, w =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for location code :{0} title: {1}", w.Code, w.Title));
                Location c = Mapping.ToPoco<Location, WebLocation>(w);
                Assert.IsNotNull(c, "result object was found to be null");
                Assert.IsTrue(c.Code == w.Code, "the code is not matching");
                Assert.IsTrue(c.Title == w.Title, "title of the course is not matching");

            });
            Location[] locations = new Location[] { 
                new Location(){Code="SampleCode1", Title= "Sample location 1"},
                new Location(){Code="SampleCode2", Title= "Sample location 2"},
                new Location(){Code="SampleCode3", Title= "Sample location 3"},
                new Location(){Code="SampleCode4", Title= "Sample location 4"},
                new Location(){Code="SampleCode5", Title= "Sample location 5"},
                new Location(){Code="SampleCode6", Title= "Sample location 6"},
            };
            Array.ForEach(locations, l =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for location code :{0} title: {1}", l.Code, l.Title));
                WebLocation wc = Mapping.ToWeb<Location, WebLocation>(l);
                Assert.IsNotNull(wc, "result object was found to be null");
                Assert.IsTrue(wc.Code == l.Code, "the code is not matching");
                Assert.IsTrue(wc.Title == l.Title, "title of the course is not matching");

            });
        }

        [TestMethod]
        public void RolesTest()
        {
            WebRole[] webroles = new WebRole[] { 
                new WebRole(){Title= "Sample location 1",Level=5},
                new WebRole(){Title= "Sample location 2",Level=5 },
                new WebRole(){Title= "Sample location 3",Level=5},
                new WebRole(){Title= "Sample location 4",Level=5},
                new WebRole(){Title= "Sample location 5",Level=5},
                new WebRole(){Title= "Sample location 6",Level=5},
            };
            Array.ForEach(webroles, wr =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for location code :{0} title: {1}"
                    , wr.Title, wr.Level));
                Role l = Mapping.ToPoco<Role, WebRole>(wr);
                Assert.IsNotNull(l, "result object was found to be null");
                Assert.IsTrue(l.Title == wr.Title, "the title is not matching");
                Assert.IsTrue(l.Level == wr.Level, "the role is not matching");
            });
            Role[] roles = new Role[] { 
                new Role(){Title= "Sample location 1",Level=3},
                new Role(){Title= "Sample location 2",Level=3},
                new Role(){Title= "Sample location 3",Level=3},
                new Role(){Title= "Sample location 4",Level=3},
                new Role(){Title= "Sample location 5",Level=3},
                new Role(){Title= "Sample location 6",Level=3},
            };
            Array.ForEach(roles, l =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for location title :{0} level: {1}", l.Title, l.Level));
                WebRole wc = Mapping.ToWeb<Role, WebRole>(l);
                Assert.IsNotNull(wc, "result object was found to be null");
                Assert.IsTrue(wc.Title == l.Title, "the code is not matching");
                Assert.IsTrue(wc.Level == l.Level, "the level is not matching");
            });
        }

        [TestMethod]
        public void EmployeesTest()
        {
            WebEmployee[] webEmployees = new WebEmployee[] {
                new WebEmployee(){Id=1, Alias="alias1", Email= "email1"},
                new WebEmployee(){Id=2, Alias="alias2", Email= "email2"},
                new WebEmployee(){Id=3, Alias="alias3", Email= "email3"},
                new WebEmployee(){Id=4, Alias="alias4", Email= "email4"},
                new WebEmployee(){Id=5, Alias="alias5", Email= "email5"},
                new WebEmployee(){Id=6, Alias="alias6", Email= "email6"},};
            Array.ForEach(webEmployees, w =>
                {
                    //test here if it can be converted 
                    Trace.WriteLine(String.Format("Poco conversion test for employees id: {0} email: {1} alias: {2}", w.Id, w.Email, w.Alias));
                    Employee c = Mapping.ToPoco<Employee, WebEmployee>(w);
                    Assert.IsNotNull(c, "result object was found to be null");
                    Assert.IsTrue(c.Id == w.Id, "the code is not matching");
                    Assert.IsTrue(c.Email== w.Email, "title of the course is not matching");
                    Assert.IsTrue(c.Alias== w.Alias, "title of the course is not matching");
                });
            Employee[] employees = new Employee[] {
                new Employee(){Id=1, Alias="alias1", Email= "email1"},
                new Employee(){Id=2, Alias="alias2", Email= "email2"},
                new Employee(){Id=3, Alias="alias3", Email= "email3"},
                new Employee(){Id=4, Alias="alias4", Email= "email4"},
                new Employee(){Id=5, Alias="alias5", Email= "email5"},
                new Employee(){Id=6, Alias="alias6", Email= "email6"},};
            Array.ForEach(employees, e =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for employees id: {0} email: {1} alias: {2}", e.Id, e.Email, e.Alias));
                WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                Assert.IsNotNull(we, "result object was found to be null");
                Assert.IsTrue(we.Id == e.Id, "the code is not matching");
                Assert.IsTrue(we.Email == e.Email, "title of the course is not matching");
                Assert.IsTrue(we.Alias == e.Alias, "title of the course is not matching");
            });
           
        }

        [TestMethod]
        public void FdbOptionsTest()
        {
            WebFdbOption[] webfdboptions = new WebFdbOption[] {
                new WebFdbOption(){ Text="alias1", Sentiment= 5},
                new WebFdbOption(){ Text="alias2", Sentiment= 5},
                new WebFdbOption(){ Text="alias3", Sentiment= 5},
                new WebFdbOption(){ Text="alias4", Sentiment= 5},
                new WebFdbOption(){ Text="alias5", Sentiment= 5},
                new WebFdbOption(){ Text="alias6", Sentiment= 5},};
            Array.ForEach(webfdboptions, w =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for fdboptions text: {0} sentiment: {1} "
                    , w.Text, w.Sentiment));
                FdbOption c = Mapping.ToPoco<FdbOption, WebFdbOption>(w);
                Assert.IsNotNull(c, "result object was found to be null");
                Assert.IsTrue(c.Text == w.Text, "the code is not matching");
                Assert.IsTrue(c.Sentiment == w.Sentiment, "title of the course is not matching");
            });
            FdbOption[] options = new FdbOption[] {
                new FdbOption(){Text="alias1", Sentiment= 2},
                new FdbOption(){Text="alias2", Sentiment= 3},
                new FdbOption(){Text="alias3", Sentiment= 4},
                new FdbOption(){Text="alias4", Sentiment= 5},
                new FdbOption(){Text="alias5", Sentiment= 0},
                new FdbOption(){Text="alias6", Sentiment= 1},};
            Array.ForEach(options, e =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for options text: {0} sentiment: {1}", e.Text, e.Sentiment));
                WebFdbOption we = Mapping.ToWeb<FdbOption, WebFdbOption>(e);
                Assert.IsNotNull(we, "result object was found to be null");
                Assert.IsTrue(we.Text == e.Text, "the code is not matching");
                Assert.IsTrue(we.Sentiment == e.Sentiment, "title of the course is not matching");
            });

        }

        [TestMethod]
        public void FdbCategoriesTest()
        {
            WebFdbCategory[] webfdboptions = new WebFdbCategory[] {
                new WebFdbCategory(){ Title="alias1", Code= "sample code"},
                new WebFdbCategory(){ Title="alias2", Code= "sample code"},
                new WebFdbCategory(){ Title="alias3", Code= "sample code"},
                new WebFdbCategory(){ Title="alias4", Code= "sample code"},
                new WebFdbCategory(){ Title="alias5", Code= "sample code"},
                new WebFdbCategory(){ Title="alias6", Code= "sample code"},};
            Array.ForEach(webfdboptions, w =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for fdboptions text: {0} sentiment: {1} "
                    , w.Code, w.Title));
                FdbCategory c = Mapping.ToPoco<FdbCategory, WebFdbCategory>(w);
                Assert.IsNotNull(c, "result object was found to be null");
                Assert.IsTrue(c.Code == w.Code, "the code is not matching");
                Assert.IsTrue(c.Title == w.Title, "title of the course is not matching");
            });
            FdbCategory[] options = new FdbCategory[] {
                new FdbCategory(){Code="alias1", Title= "Sample title"},
                new FdbCategory(){Code="alias2", Title= "Sample title"},
                new FdbCategory(){Code="alias3", Title= "Sample title"},
                new FdbCategory(){Code="alias4", Title= "Sample title"},
                new FdbCategory(){Code="alias5", Title= "Sample title"},
                new FdbCategory(){Code="alias6", Title= "Sample title"},};
            Array.ForEach(options, e =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for options code: {0} title: {1}"
                    , e.Code, e.Title));
                WebFdbCategory we = Mapping.ToWeb<FdbCategory, WebFdbCategory>(e);
                Assert.IsNotNull(we, "result object was found to be null");
                Assert.IsTrue(we.Code == e.Code, "the code is not matching");
                Assert.IsTrue(we.Title == e.Title, "title of the course is not matching");
            });

        }

        [TestMethod]
        public void TrainingsTest()
        {
            
            WebTraining[] webfdboptions = new WebTraining[] {
                new WebTraining(){Title="sample title1", Start=DateTime.Parse("2012/03/28"), End= DateTime.Parse("2012/04/28")},
                new WebTraining(){Title="sample title1", Start=DateTime.Parse("2012/03/28"), End= DateTime.Parse("2012/04/28")},
                new WebTraining(){Title="sample title1", Start=DateTime.Parse("2012/03/28"), End= DateTime.Parse("2012/04/28")},
                new WebTraining(){Title="sample title1", Start=DateTime.Parse("2012/03/28"), End= DateTime.Parse("2012/04/28")},
                new WebTraining(){Title="sample title1", Start=DateTime.Parse("2012/03/28"), End= DateTime.Parse("2012/04/28")},
                new WebTraining(){Title="sample title1", Start=DateTime.Parse("2012/03/28"), End= DateTime.Parse("2012/04/28")},};
            Array.ForEach(webfdboptions, w =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for training:starting {0} ending: {1} "
                    , w.Start, w.End));
                Training c = Mapping.ToPoco<Training, WebTraining>(w);
                Assert.IsNotNull(c, "result object was found to be null");
                Assert.IsTrue(c.Title == w.Title, "the code is not matching");
                Assert.IsTrue(c.Start == w.Start, "the code is not matching");
                Assert.IsTrue(c.End == w.End, "title of the course is not matching");
            });
            Training[] options = new Training[] {
                new Training(){Title="sample training title1",Start=DateTime.Parse("2015/05/28"), End= DateTime.Parse("2015/06/28")},
                new Training(){Title="sample training title1",Start=DateTime.Parse("2015/05/28"), End= DateTime.Parse("2015/06/28")},
                new Training(){Title="sample training title1",Start=DateTime.Parse("2015/05/28"), End= DateTime.Parse("2015/06/28")},
                new Training(){Title="sample training title1",Start=DateTime.Parse("2015/05/28"), End= DateTime.Parse("2015/06/28")},
                new Training(){Title="sample training title1",Start=DateTime.Parse("2015/05/28"), End= DateTime.Parse("2015/06/28")},
                new Training(){Title="sample training title1",Start=DateTime.Parse("2015/05/28"), End= DateTime.Parse("2015/06/28")},};
            Array.ForEach(options, e =>
            {
                //test here if it can be converted 
                Trace.WriteLine(String.Format("Poco conversion test for options code: {0} title: {1}"
                    , e.Start, e.End));
                WebTraining we = Mapping.ToWeb<Training, WebTraining>(e);
                Assert.IsNotNull(we, "result object was found to be null");
                Assert.IsTrue(we.Title == e.Title, "the code is not matching");
                Assert.IsTrue(we.Start == e.Start, "the code is not matching");
                Assert.IsTrue(we.End == e.End, "title of the course is not matching");
            });

        }
        
    }
}
