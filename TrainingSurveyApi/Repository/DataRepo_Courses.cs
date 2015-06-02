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
        
        internal HttpResponseMessage GETCourseIndex(int page)
        {
            //handling the code for invalid page values
            if (page <= 0) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("page for the index of the courses cannot be zero or negative"));
            }
            List<WebCourse> result = new List<WebCourse>();

            using (academyContext db = new academyContext())
            {
                var courses = db.Courses.Select(x => x);

                int totalPages = courses.Count() % coursesPerPage == 0 ?
                    courses.Count() / coursesPerPage :
                    (courses.Count() / coursesPerPage) + 1;
                string prevPage = default(string);
                string nextPage = default(string);

                if (page > 1 && page <= totalPages + 1) {
                    prevPage = new UrlHelper(request).Link("courses", new { page = page - 1 });
                }
                if (page >= 1 && page < totalPages ){
                    nextPage = new UrlHelper(request).Link("courses", new { page = page + 1 });
                }
                //now modifying the query to get the paged results
                courses = courses.OrderBy(c => c.Code).Skip((page - 1) * coursesPerPage).Take(coursesPerPage);
                foreach (var c in courses)
                {
                    //serialization and mapping the hateoas links to the result
                    WebCourse wc = Mapping.ToWeb<Course, WebCourse>(c);
                    wc = HateMapping.Map<WebCourse>(wc, request);
                    result.Add(wc);
                }
                //well formed result being sent back with httpOK
                return request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       totalPages = totalPages,
                       prevPage = prevPage,
                       nextPage = nextPage,
                       result = result
                   });
            }
           
           
        }

        internal HttpResponseMessage GETCourseOfCode(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                return request.CreateResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("code for the code cannot be null or empty"));
            }
            //TODO : need to do error handling for this function
            using (academyContext db = new academyContext())
            {
                var course = db.Courses.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (course == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format(
                        "course of the code :{0} not found", code)));
                }
                WebCourse wc = Mapping.ToWeb<Course, WebCourse>(course);
                if (wc == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("serialization failed"));
                }
                wc = HateMapping.Map<WebCourse>(wc, request);

                return request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        result = wc
                    });
            }
        }

        internal HttpResponseMessage DELETECourseOfCode(string code)
        {
            using (academyContext db = new academyContext())
            {
                var course = db.Courses.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (course == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format(
                            "Course of the code :{0} not found", code)));
                }
                db.Courses.Remove(course);
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Failed to do database operation"));
                }
            }
        }

        internal HttpResponseMessage POSTCourse(WebCourse toAdd)
        {
            if (toAdd == null || String.IsNullOrEmpty(toAdd.Code))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("The course / course code cannot be null"));
            }
            using (academyContext db = new academyContext())
            {
                Course c = Mapping.ToPoco<Course, WebCourse>(toAdd);
                if (db.Courses.Where(x => x.Code.ToLower() == toAdd.Code).Count() != 0){

                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format(
                            "Course with the code : {0} already present", toAdd.Code)));
                }
                db.Courses.Add(c);
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception){
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }
            }
        }

        internal HttpResponseMessage PUTCourse(WebCourse toUpdate)
        {
            if (toUpdate==null || String.IsNullOrEmpty(toUpdate.Code))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Course to update cannot be null or have null code"));
            }
            using (academyContext db = new academyContext())
            {
               
                Course inDb = db.Courses.Where(x => x.Code.ToLower() == toUpdate.Code).FirstOrDefault();
                if (inDb ==null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("course to update could not be found"));
                }
                inDb.Title = toUpdate.Title;
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