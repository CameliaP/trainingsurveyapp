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
        internal HttpResponseMessage GETUnitOfEmployee(int id)
        {
            WebUnit result = default(WebUnit);
            using (academyContext db  = new academyContext())
            {
                var employee = db.Employees.Where(x => x.Id == id).FirstOrDefault();
                if (employee == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new ArgumentException("Employee not found"));
                }
                result = Mapping.ToWeb<Unit, WebUnit>(employee.Unit);
                if (result == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new InvalidCastException("Serialization failed"));
                }
                result = HateMapping.Map<WebUnit>(result, request);
            }
            return request.CreateResponse(HttpStatusCode.OK,
                result);
        }

        internal HttpResponseMessage GETUnitOfCode(string code)
        {
            WebUnit result = default(WebUnit);
            if (String.IsNullOrEmpty(code)) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new ArgumentNullException("Unit code is null"));
            }
            using (academyContext db = new academyContext())
            {
                var unit = db.Units.Where(u => u.Code == code).FirstOrDefault();
                if (unit == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("No matching unit found"));
                }
                result = Mapping.ToWeb<Unit, WebUnit>(unit);
                if (result == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new InvalidCastException("Serialization failed"));
                }
                result = HateMapping.Map<WebUnit>(result, request);
                
            }
            return request.CreateResponse(HttpStatusCode.OK, 
                result);
        }

        internal HttpResponseMessage GETUnitsIndex(int page)
        {
            //TODO: this returns the page number for the prev and the next despite having no pages around it. 
            List<WebUnit> result = new List<WebUnit>();
            using (academyContext  db = new academyContext())
            {
                //interim query
                var units = db.Units.Select(x=>x);
                //pagination and calculations
                
                int totalPages = units.Count() % unitsPerPage == 0 ?
                    units.Count() / unitsPerPage :
                    (units.Count() / unitsPerPage) + 1;
                string prevPage = page == 1 ? "" : new UrlHelper(request).Link("units", new { page = page - 1 });
                string nextPage = page == totalPages ? "" : new UrlHelper(request).Link("units", new { page = page + 1 });

                
                units = units.OrderBy(x => x.Code).Skip((page - 1) * unitsPerPage).Take(unitsPerPage);

                foreach (var u in units)
                {
                    WebUnit wu = Mapping.ToWeb<Unit, WebUnit>(u);
                    if (wu == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("Serialization failed")); }
                    wu = HateMapping.Map<WebUnit>(wu,request);
                    result.Add(wu);
                }
                return request.CreateResponse(HttpStatusCode.OK, new { 
                    TotalPages =totalPages,
                    PrevPage = prevPage,
                    nextPage = nextPage,
                    Result = result
                });
            }
            
        }

        internal HttpResponseMessage PUTUnit(efAcademy.Entities.Web.WebUnit toUpdate)
        {
            if (toUpdate == null || String.IsNullOrEmpty(toUpdate.Code)) { 
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("Unit to be updated is null"));
            }
            Unit unit = Mapping.ToPoco<Unit, WebUnit>(toUpdate);

            if (unit == null) { return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                new InvalidCastException("Deserialization failed"));
            }
            using (academyContext db = new academyContext())
            {
                var unitIndb = db.Units.Where(x => x.Code == unit.Code).FirstOrDefault();
                unitIndb.Title = unit.Title;
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

        internal HttpResponseMessage POSTUnit(efAcademy.Entities.Web.WebUnit toAdd)
        {
            if (toAdd == null) { return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new ArgumentNullException("Unit to add is null"));
            }
            using (academyContext db = new academyContext())
            {
                if (db.Units.Where(x=>x.Code == toAdd.Code).Count()!=0) { 
                    request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException("Unit to be added is already added"));
                }
                Unit u = Mapping.ToPoco<Unit, WebUnit>(toAdd);
                if (u == null) { request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    new InvalidCastException("Serialization failed"));
                }
                db.Units.Add(u);
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception xx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("database operation failed", xx));
                }
            }
        }

        internal HttpResponseMessage DELETEUnit(string code)
        {
            if (String.IsNullOrEmpty(code)) { request.CreateErrorResponse(HttpStatusCode.BadRequest,
               new ArgumentNullException("Code of the unit to delete is null"));
            }
            using (academyContext db = new academyContext())
            {
                if (db.Units.Where(x => x.Code == code).Count() == 0) { 
                    request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException("Code of the unit to delete not found")); }
                db.Units.Remove(db.Units.Where(x => x.Code == code).FirstOrDefault());
                try
                {
                    db.SaveChanges();
                    
                }
                catch (Exception xx) 
                {
                    request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }
            }
            return request.CreateResponse(HttpStatusCode.NoContent);
        }
       
    }
}