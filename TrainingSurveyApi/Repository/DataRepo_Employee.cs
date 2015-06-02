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
using System.Net;

namespace TrainingSurveyApi.Repository
{
    public sealed partial class DataRepo
    {
        public HttpResponseMessage GETEmployeesOfRole(string title)
        {
            //invalid input
            if (String.IsNullOrEmpty(title))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("role title cannot be null"));
            }
            List<WebEmployee> result = new List<WebEmployee>();//result set
            using (academyContext db = new academyContext())
            {
                try
                {
                    var employees = db
                              .Roles
                              .Where(r => r.Title == title)
                              .FirstOrDefault()
                              .Employees;//getting all the employees of the role
                    if (employees == null)
                    {
                        return request.CreateResponse(HttpStatusCode.OK,
                            new { Results = new WebEmployee[0] });
                    }

                    foreach (var e in employees)
                    {
                        WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                        if (we == null)
                        {
                            return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                new InvalidCastException("Serialization failed"));
                        }
                        we = HateMapping.Map<WebEmployee>(we, request);
                        result.Add(we);
                    }
                }
                catch (Exception x)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }

            }
            return request.CreateResponse(HttpStatusCode.OK,
                new { results = result.ToArray<WebEmployee>() });
        }
        public HttpResponseMessage GETEmployeesIndex(int page)
        {
            //for invalid page numbers ?
            if (page <= 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Page number for the employees cannot be zero or negative"));
            }
            List<WebEmployee> result = new List<WebEmployee>();
            using (academyContext db = new academyContext())
            {

                var employees = db.Employees.Select(x => x);
                //check for null query and return for internal sever error

                //calculating the pagination
                int totalPages = employees.Count() % employeesPerPage == 0 ?
                    employees.Count() / employeesPerPage :
                    (employees.Count() / employeesPerPage) + 1;

                string prevPage = default(string);
                string nextPage = default(string);

                if (page > 1 && page <= totalPages + 1)
                {
                    prevPage = new UrlHelper(request).Link("employees", new { page = page - 1 });
                }
                if (page >= 1 && page < totalPages)
                {
                    nextPage = new UrlHelper(request).Link("employees", new { page = page + 1 });

                }// hateoas links for the pagination

                //getting the paged results
                employees = employees.OrderBy(x => x.Id).Skip((page - 1) * employeesPerPage).Take(employeesPerPage);

                foreach (var e in employees)
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null)
                    {
                        return request.CreateErrorResponse(
                            HttpStatusCode.InternalServerError,
                            new InvalidCastException("Serialization failed"));
                    }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                }
                return request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    totalPages = totalPages,
                    prevPage = prevPage,
                    nextPage = nextPage,
                    results = result.ToArray<WebEmployee>()
                });
            }

        }

        public HttpResponseMessage GETEmployeesContainingPhrase(string phrase, int page)
        {
            if (String.IsNullOrEmpty(phrase))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Search phrase cannot be null or empty"));
            }
            List<WebEmployee> result = new List<WebEmployee>();
            using (academyContext db = new academyContext())
            {
                var employees = db.Employees.Where(x => x.Alias.ToLower()
                    .Contains(phrase.ToLower())).Select(x => x);

                int totalPages = employees.Count() % employeesPerPage == 0 ?
                    employees.Count() / employeesPerPage :
                    (employees.Count() / employeesPerPage) + 1;
                string prevPage = default(string);
                string nextPage = default(string);

                if (page > 1 && page <= totalPages + 1)
                {
                    prevPage = new UrlHelper(request).Link("employeesLike",
                        new { phrase = phrase, page = page - 1 });
                }
                if (page > 0 && page < totalPages)
                {
                    nextPage = new UrlHelper(request).Link("employeesLike",
                        new { phrase = phrase, page = page + 1 });
                }
                employees = employees.OrderBy(e => e.Id)
                    .Skip((page - 1) * employeesPerPage)
                    .Take(employeesPerPage).Select(e=>e); //getting the paged results

                foreach (var e in employees)
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null)
                    {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new InvalidCastException("Serialization failed"));
                    }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                }
                return request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      totalPages = totalPages,
                      prevPage = prevPage,
                      nextPage = nextPage,
                      results = result.ToArray<WebEmployee>()
                  });
            }


        }

        public HttpResponseMessage GETEmployeeOfId(int pk)
        {
            WebEmployee result = default(WebEmployee);
            using (academyContext db = new academyContext())
            {
                var employee = db.Employees.Where(x => x.Id == pk).FirstOrDefault();
                if (employee == null) { return  request.CreateErrorResponse(HttpStatusCode.BadRequest,
                     new ArgumentException("employee of the id not found"));}
                result = Mapping.ToWeb<Employee, WebEmployee>(employee);
                if (result == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new InvalidCastException("Serialization failed"));}
                result = HateMapping.Map<WebEmployee>(result, request);
            }
            return request.CreateResponse(HttpStatusCode.OK,
                new  {
                    result = result
                });
        }

        public HttpResponseMessage PUTEmployee(WebEmployee toUpdate)
        {
            //bad request
            if (toUpdate == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new ArgumentException("Employee to update cannot be null")); } 
            Employee e = Mapping.ToPoco<Employee, WebEmployee>(toUpdate);
            //deserialization exception
            if (e == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                new InvalidCastException("Deserialization failed"));
            }
            using (academyContext db = new academyContext())
            {
                Employee inDb = db.Employees.Where(x => x.Id == e.Id).FirstOrDefault();
                if (inDb == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException(String.Format("Employee to update :{0} not found", toUpdate.Id)));
                }
                //this is patching and not putting the entity
                inDb.Alias = e.Alias;
                inDb.Email = e.Email;
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Error saving changes to the database"));
                }
            }
        }

        public HttpResponseMessage POSTEmployee(WebEmployee newItem)
        {
            if (newItem == null) { 
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("Item to add cannot be null"));
            }
            Employee e = Mapping.ToPoco<Employee, WebEmployee>(newItem);
            if (e == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                new InvalidCastException("Deserialization failed"));
            }//this is when the mapping has fail
            using (academyContext db = new academyContext())
            {
                Employee inDb = db.Employees.Where(x => x.Id == e.Id).FirstOrDefault();
                if (inDb != null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Duplicate employee found")); }

                if (e.Id == 0 || String.IsNullOrEmpty(e.Alias) || String.IsNullOrEmpty(e.Email))
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException("Email or alias for the employee to add cannot be null"));
                }
                db.Employees.Add(e);
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception xx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Datatabase operation failed", xx));
                }
            }
        }

        public HttpResponseMessage DELETEEmployee(int toDelete)
        {
            if (toDelete == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new ArgumentNullException("Invalid item to delete"));
            }//nothing to delete
           
            using (academyContext db = new academyContext())
            {
                Employee inDb = db.Employees.Where(x => x.Id == toDelete).FirstOrDefault();
                if (inDb == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Item to delete not found"));
                }
                db.Employees.Remove(inDb);
                try
                {
                    db.SaveChanges();
                    //changes successful and ok
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception xx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed", xx));
                }
            }
        }

        internal HttpResponseMessage GETEmployeeOfLocation(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Unit code for the employee cannot be null"));
            }
            List<WebEmployee> result = new List<WebEmployee>();
            using (academyContext db = new academyContext())
            {
                var location = db.Locations.Where(l => l.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (location == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("location for the code :{0} not found", code)));
                }//location requested not found
                var employees = db.Employees.Where(e => e.Location.Code == location.Code).Select(x => x);
                foreach(var e in employees)
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null){
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new InvalidCastException("Serialization failed"));
                    }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                }
            }
            return request.CreateResponse(HttpStatusCode.OK,
                new { results = result });
        }

        internal HttpResponseMessage GETEmployeeOfUnit(string code)
        {
            //bad request
            if (string.IsNullOrEmpty(code))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Code for unit cannot be null"));
            }
            List<WebEmployee> result = new List<WebEmployee>();
            using (academyContext db = new academyContext())
            {
                var unit = db.Units.Where(u => u.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (unit == null) { return  request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Invalid unit"));
                }
                var employees = db.Employees.Where(x => x.Unit.Code == unit.Code).Select(x => x);
                foreach(var e in employees)
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("Serialization failed"));
                    }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                }
            }
            return request.CreateResponse(HttpStatusCode.OK,
                new { results = result});
        }

        internal HttpResponseMessage GETEmployeeOfProject(string code)
        {
            if (String.IsNullOrEmpty(code)) {  
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("Project code is invalid"));
            }//invalid request
            List<WebEmployee> result = new List<WebEmployee>();

            using (academyContext db = new academyContext())
            {
                var project = db.Projects.Where(u => u.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (project == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Project not found"));
                }
                var employees = db.Employees.Where(x => x.Project.Code == project.Code).Select(x => x);
                foreach(var e in employees)
                {
                    WebEmployee we = Mapping.ToWeb<Employee, WebEmployee>(e);
                    if (we == null) { throw new InvalidCastException("Serialization failed"); }
                    we = HateMapping.Map<WebEmployee>(we, request);
                    result.Add(we);
                }
            }
            return request.CreateResponse(HttpStatusCode.OK,
                new  {results = result });
        }

    }
}