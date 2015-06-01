using System;
using System.Net;
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

        internal HttpResponseMessage GETLocationsIndex(int page)
        {
            if (page <= 0)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    new ArgumentException("page number cannot be zero or negative")
                    );
            }
            List<WebLocation> result = new List<WebLocation>();
            using (academyContext db = new academyContext())
            {
                var locations = db.Locations.Select(x => x);
                int totalPages = locations.Count() % locationsPerPage == 0 ?
                    locations.Count() / locationsPerPage :
                    (locations.Count() / locationsPerPage) + 1;
                //this would give the ahteoas links for those only when necessary
                string prevPage = default(string);
                string nextPage = default(string);

                if (page > 1 && page <= totalPages +1) { 
                    prevPage = new UrlHelper(request).Link("locations", new { page = page - 1 });
                }
                if (page >= 0 && page < totalPages) { 
                    nextPage = new UrlHelper(request).Link("locations", new { page = page + 1 });
                }

                locations = locations.OrderBy(x => x.Code).Skip((page - 1) * locationsPerPage).Take(locationsPerPage);

                foreach (var l in locations)
                {
                    WebLocation wl = Mapping.ToWeb<Location, WebLocation>(l);
                    if (wl == null)
                    {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new InvalidCastException("serialization fail"));
                    }//webmapping failed
                    wl = HateMapping.Map<WebLocation>(wl, request);
                    result.Add(wl);
                }
                return request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        TotalPages = totalPages,
                        PrevPage = prevPage,
                        NextPage = nextPage,
                        Result = result
                    }); // result is packed up to be sent as https resposne
            }
        }

        internal HttpResponseMessage GETLocationOfCode(string code)
        {
            if (String.IsNullOrEmpty(code)) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new ArgumentNullException("Code for the location cannot be null"));
            }
            WebLocation wl = default(WebLocation);
            using (academyContext db = new academyContext())
            {
                var location = db.Locations.Where(x => x.Code == code).FirstOrDefault();
                if (location == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("location not found"));
                }
                wl = Mapping.ToWeb<Location, WebLocation>(location);
                if (wl == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError, 
                    new InvalidCastException("Serialization failed")); }
                wl = HateMapping.Map<WebLocation>(wl, request);
            }
            return request.CreateResponse(HttpStatusCode.OK,
                wl);
        }

        internal HttpResponseMessage GETLocationsContainingPhrase(string code, int page)
        {
            if (String.IsNullOrEmpty(code)) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new ArgumentNullException("code for the location cannot be null"));
            }
            List<WebLocation> result = new List<WebLocation>();
            using (academyContext db = new academyContext())
            {
                var locations = db.Locations.Where(x => x.Title.ToLower().Contains(code.ToLower())).Select(x => x);

                int totalPages = locations.Count() % locationsPerPage == 0 ?
                    locations.Count() / locationsPerPage :
                    (locations.Count() / locationsPerPage) + 1;
                string prevPage = new UrlHelper(request).Link("location", new
                {
                    code = code,
                    exact = false,
                    page  = page -1
                });
                string nextPage = new UrlHelper(request).Link("location", new
                {
                    code = code,
                    exact = false,
                    page = page + 1
                });
                foreach (var l in locations)
                {
                    WebLocation wl = Mapping.ToWeb<Location, WebLocation>(l);

                    if (wl == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("serialization failed"));
                    }
                    wl = HateMapping.Map<WebLocation>(wl, request);
                    result.Add(wl);
                }
                return request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    TotalPages = totalPages,
                    PrevPage = prevPage,
                    NextPage  = nextPage,
                    Result = result
                });
            }
            
        }

        internal HttpResponseMessage GETLocationOfEmployee(int id)
        {
            WebLocation wl = default(WebLocation);
            using (academyContext db = new academyContext())
            {
                var employee = db.Employees.Where(x => x.Id == id).FirstOrDefault();
                if (employee == null) {return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException(String.Format("Employee with the id : {0} not found", id)));
                }
                wl = Mapping.ToWeb<Location, WebLocation>(employee.Location);
                if (wl == null) {  return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new InvalidCastException("Serialization failed"));
                }
                wl = HateMapping.Map<WebLocation>(wl, request);
            }
            return request.CreateResponse(HttpStatusCode.OK,
                wl);
        }

        internal HttpResponseMessage PUTLocation(WebLocation toUpdate)
        {
            if (toUpdate == null || String.IsNullOrEmpty(toUpdate.Code)) { 
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("Location to update cannot be null"));
            }
            using (academyContext db = new academyContext())
            {
                var location = db.Locations.Where(x => x.Code == toUpdate.Code).FirstOrDefault();
                if (location == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("Location with the code :{0} not found", toUpdate.Code)));
                }
                location.Title = toUpdate.Title;
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception xx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Failed database operation"));
                }

            }
        }

        internal HttpResponseMessage POSTLocation(WebLocation toAdd)
        {
            if (toAdd == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("location to add cannot be null"));
            }
            using (academyContext db = new academyContext())
            {
                var location = db.Locations.Where(x => x.Code == toAdd.Code).FirstOrDefault();
                if (location != null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException(String.Format("Location with the code : {0} already present", toAdd.Code)));
                }
                Location newLocation = Mapping.ToPoco<Location, WebLocation>(toAdd);
                if (newLocation == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new InvalidCastException("Serialiation failed"));
                }
                db.Locations.Add(newLocation);
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

        internal HttpResponseMessage DELETELocation(string code)
        {
            if (String.IsNullOrEmpty(code)) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                 new ArgumentNullException("Code for the location to delete is null")); }
            using (academyContext db = new academyContext())
            {
                var location = db.Locations.Where(x => x.Code == code).FirstOrDefault();
                if (location == null) { 
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                         new ArgumentException(String.Format("Location with the code: {0} not found", code))); }
                db.Locations.Remove(location);
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception xx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                         new IOException("Database operation failed", xx));
                }
            }
        }
    }
}