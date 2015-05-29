using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using efAcademy.Context;
using efAcademy.Entities;
using efAcademy.Entities.Web;
using System.Data;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http.Routing;
using WebObjectMapping.Services;
using WebObjectMapping.Attributes;
using System.IO;

namespace TrainingSurveyApi.Repository
{
    public sealed partial class DataRepo
    {
        public IEnumerable<WebEmployee> GETEmployeesOfRole(string title)
        {
            List<WebEmployee> result = new List<WebEmployee>();
            if (String.IsNullOrEmpty(title)) { throw new ArgumentException("role title cannot be null"); }
            
            using (academyContext db = new academyContext())
            {
                var employees = db
                    .Roles
                    .Where(r => r.Title == title)
                    .FirstOrDefault()
                    .Employees;
                if (employees == null) { throw new ArgumentException(String.Format("Not employees with role title :{0} found", title)); }
                Array.ForEach<Employee>(employees.ToArray<Employee>(), e =>
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null) { throw new InvalidCastException("Serialization failed"); }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                });
            }
            return result.ToArray<WebEmployee>();
        }
        public IEnumerable<WebEmployee> GETEmployeesIndex(int top, int skip)
        {
            List<WebEmployee> result = new List<WebEmployee>();
            using (academyContext db = new academyContext())
            {
                var employees = db.Employees.OrderBy(e => e.Id).Skip(skip).Take(top);
                if (employees == null || employees.Count() == 0) { throw new ArgumentException("Skipped too many records, or not enough records in the database"); }
                Array.ForEach(employees.ToArray<Employee>(), e =>
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null) { throw new InvalidCastException("Serialization failed"); }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                });
            }
            return result.ToArray<WebEmployee>();
        }

        public IEnumerable<WebEmployee> GETEmployeesContainingPhrase(string phrase)
        {
            List<WebEmployee>result = new List<WebEmployee>();
            using (academyContext db = new academyContext())
            {
                var employees = db.Employees.Where(x => x.Alias.ToLower().Contains(phrase.ToLower())).ToArray<Employee>();
                Array.ForEach<Employee>(employees, e =>
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null) { throw new InvalidCastException("Serialization failed"); }
                    we = HateMapping.Map<WebEmployee>(we,request);
                    result.Add(we);
                });
            }

            return result.ToArray<WebEmployee>();
        }

        public WebEmployee GETEmployeeOfId(int pk)
        {
            WebEmployee result = default(WebEmployee);
            using (academyContext db = new academyContext())
            {
                var employee = db.Employees.Where(x => x.Id == pk).FirstOrDefault();
                if (employee == null) { throw new ArgumentException("employee of the id not found"); }
                result = Mapping.ToWeb<Employee, WebEmployee>(employee);
                if (result == null) { throw new InvalidCastException("Serialization failed"); }
                result = HateMapping.Map<WebEmployee>(result, request);
            }
            return result;

        }

        public void PUTEmployee(WebEmployee toUpdate)
        {
            if (toUpdate == null) { return; } //no need for any update
            Employee e = Mapping.ToPoco<Employee, WebEmployee>(toUpdate);
            if (e == null) { throw new InvalidCastException("Deserialization failed"); }
            using (academyContext db = new academyContext())
            {
                Employee inDb = db.Employees.Where(x => x.Id == e.Id).FirstOrDefault();
                if (inDb == null) { throw new ArgumentException(String.Format("Employee to update :{0} not found", toUpdate.Id)); }
                inDb.Alias = e.Alias;
                inDb.Email = e.Email;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception xx)
                {
                    return;
                }
            }
        }

        public void POSTEmployee(WebEmployee newItem)
        {
            if (newItem == null) { throw new ArgumentNullException("Item to add cannot be null"); }
            Employee e = Mapping.ToPoco<Employee, WebEmployee>(newItem);
            if (e == null) { throw new InvalidCastException("Deserialization failed"); }//this is when the mapping has fail
            using (academyContext db = new academyContext())
            {
                Employee inDb = db.Employees.Where(x => x.Id == e.Id).FirstOrDefault();
                if (inDb != null) { return; }//this is when the employee is already in the database
                if (e.Id == 0 || String.IsNullOrEmpty(e.Alias) || String.IsNullOrEmpty(e.Email)) {
                    throw new ArgumentException("Item to add is invalid");
                }
                db.Employees.Add(e);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception xx)
                {
                    throw new IOException("Datatabase operation failed", xx); 
                }
            }
        }

        public void DELETEEmployee(WebEmployee toDelete)
        {
            if (toDelete == null) { throw new ArgumentNullException("Invalid item to delete"); }//nothing to delete
            Employee e = Mapping.ToPoco<Employee, WebEmployee>(toDelete);
            if (e == null) { throw new InvalidCastException("Deserialization failed"); }
            using (academyContext db = new academyContext())
            {
                Employee inDb = db.Employees.Where(x => x.Id == e.Id).FirstOrDefault();
                if (inDb == null) { throw new ArgumentException("Item to delete not found"); }
                db.Employees.Remove(inDb);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception xx)
                {
                    throw new IOException("Database operation failed", xx);
                }
            }
        }

        internal IEnumerable<WebEmployee> GETEmployeeOfLocation(string code)
        {
            List<WebEmployee> result = new List<WebEmployee>();
            if (String.IsNullOrEmpty(code)) { throw new ArgumentNullException("location code for the employee is null"); }//invalid request
            using (academyContext db = new academyContext())
            {
                var location = db.Locations.Where(l => l.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (location == null) { return null; }//location requested not found
                var employees = db.Employees.Where(e => e.Location.Code == location.Code).Select(x=>x);
                Array.ForEach<Employee>(employees.ToArray<Employee>(), e =>
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we != null)
                    {
                        we = HateMapping.Map<WebEmployee>(we, request);
                    }
                    result.Add(we);
                });
            }
            return result.ToArray<WebEmployee>();
        }

        internal IEnumerable<WebEmployee> GETEmployeeOfUnit(string code)
        {
            List<WebEmployee> result = new List<WebEmployee>();
            if (String.IsNullOrEmpty(code)) { throw new ArgumentNullException("Invalid unit code"); }
            using (academyContext db = new academyContext())
            {
                var unit = db.Units.Where(u => u.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (unit == null) { throw new ArgumentException("Invalid unit"); }
                var employees = db.Employees.Where(x => x.Unit.Code == unit.Code).Select(x => x);
                Array.ForEach<Employee>(employees.ToArray<Employee>(), e =>
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null) { throw new InvalidCastException("Serialization failed"); }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                });
            }
            return result.ToArray<WebEmployee>();
        }

        internal IEnumerable<WebEmployee> GETEmployeeOfProject(string code)
        {
            List<WebEmployee> result = new List<WebEmployee>();

            if (String.IsNullOrEmpty(code)) { throw new ArgumentNullException("Project code is invalid"); }//invalid request
            using (academyContext db = new academyContext())
            {
                var project = db.Projects.Where(u => u.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (project == null) { throw new ArgumentException("Project not found"); }
                var employees = db.Employees.Where(x => x.Project.Code == project.Code).Select(x => x);
                Array.ForEach<Employee>(employees.ToArray<Employee>(), e =>
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null) { throw new InvalidCastException("Serialization failed"); }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                });
            }
            return result.ToArray<WebEmployee>();
        }
       
    }
}