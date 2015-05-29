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

        internal HttpResponseMessage GETProjectsIndex(int page)
        {
            List<WebProject> result = new List<WebProject>();
            using (academyContext db = new academyContext())
            {
                var projects = db.Projects.Select(x => x);
                int totalPages = projects.Count() % projectsPerPage == 0 ?
                    projects.Count() / projectsPerPage :
                    (projects.Count() / projectsPerPage) + 1;
                string prevPage = page == 1 ? "" : new UrlHelper(request).Link("projects", new { page = page - 1 });
                string nextPage = page == totalPages ? "" : new UrlHelper(request).Link("projects", new { page = page + 1 });

                projects = projects.OrderBy(x => x.Code).Skip((page - 1) * projectsPerPage).Take(projectsPerPage);

                foreach (var p in projects)
                {
                    WebProject wp = Mapping.ToWeb<Project, WebProject>(p);
                    if (wp == null) {  
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new InvalidCastException("Serialization failed"));
                    }
                    wp = HateMapping.Map<WebProject>(wp, request);
                    result.Add(wp);
                }
                return request.CreateResponse(HttpStatusCode.OK, new { 
                    TotalPages = totalPages,
                    PrevPage = prevPage,
                    NextPage= nextPage,
                    Result = result
                });
            }
            
        }

        internal HttpResponseMessage GETProjectOfEmployee(int id)
        {
            WebProject result = default(WebProject);
            using (academyContext db = new academyContext())
            {
                if (db.Employees.Where(x => x.Id == id).Count() == 0) { 
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("Employee of the id :{0} not found", id)));
                }
                var employee = db.Employees.Where(x => x.Id == id).FirstOrDefault();
                result = Mapping.ToWeb<Project, WebProject>(employee.Project);
                if(result==null){return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new InvalidCastException("Serialization failed"));
                }
                result = HateMapping.Map<WebProject>(result, request);
            }
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        internal HttpResponseMessage GETProjectOfCode(string code)
        {

            if (String.IsNullOrEmpty(code)) { return request.CreateErrorResponse(HttpStatusCode.BadRequest, 
                new ArgumentNullException("code for the project cannot be null")); }
            WebProject result = default(WebProject);

            using (academyContext db = new academyContext())
            {
                var project = db.Projects.Where(x => x.Code == code).FirstOrDefault();
                if (project == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException(String.Format("Project of the code :{0} not found", code)));
                }
                result = Mapping.ToWeb<Project, WebProject>(project);
                if (result == null) { return     request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new InvalidCastException("Serialization failed"));
                }
                result = HateMapping.Map<WebProject>(result, request);
            }
            return request.CreateResponse(HttpStatusCode.OK,
                result);
        }

        internal HttpResponseMessage PUTProject(WebProject toUpdate)
        {
            WebProject result = default(WebProject);
            if (toUpdate == null || String.IsNullOrEmpty(toUpdate.Code)) { 
                return  request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("The project to be added cannot be null"));
            }
            using (academyContext db =  new academyContext())
            {
                var project = db.Projects.Where(x => x.Code == toUpdate.Code).FirstOrDefault();
                if (project == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("Project with the code: {0} not found", toUpdate.Code)));
                }
                //gotta update the project
                project.CustomerCode = toUpdate.CustomerCode;
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);

                }
                catch (Exception)
                {
                    
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }
            }
        }

        internal HttpResponseMessage POSTPRoject(WebProject toAdd)
        {
            if (toAdd == null||String.IsNullOrEmpty(toAdd.Code)) { 
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("project to be added cannot be null or invalid project code"));
            }
            
            using (academyContext db = new academyContext())
            {
                var project = db.Projects.Where(x => x.Code == toAdd.Code).FirstOrDefault();
                if (project != null) { 
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("Project witht the code: {0} already present", toAdd.Code)));
                }
                Project p = Mapping.ToPoco<Project, WebProject>(toAdd);
                if (p == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new InvalidCastException("De serialization failed"));
                }
                db.Projects.Add(p);
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception xx)
                {
                    
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }
            }
        }

        internal HttpResponseMessage DELETEProject(string code)
        {
            if (String.IsNullOrEmpty(code)) { 
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("project with null code cannot be deleted"));
            }
            using (academyContext db = new academyContext())
            {
                var project = db.Projects.Where(x => x.Code == code).FirstOrDefault();
                if (project == null) { 
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("project with the code: {0} not found", code)));
                }
                db.Projects.Remove(project);
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception)
                {
                    
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }
            }
        }
    }
}