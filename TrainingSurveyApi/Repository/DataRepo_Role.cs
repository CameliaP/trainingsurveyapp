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
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;
namespace TrainingSurveyApi.Repository
{
    public sealed partial class DataRepo
    {
        internal HttpResponseMessage SearchRoles(string phrase, int level, int page)
        {
            using (academyContext db = new academyContext())
            {
                var roles = db.Roles.Select(x => x);
                if (!String.IsNullOrEmpty(phrase)) { roles = roles.Where(x => x.Title.ToLower().Contains(phrase.ToLower())).Select(x => x); }
                if (level >= 1) { roles = roles.Where(x => x.Level == level).Select(x => x); }

                int totalPages = roles.Count() % rolesPerPage == 0 ?
                    roles.Count() / rolesPerPage :
                    (roles.Count() / rolesPerPage) + 1;
                string prevPage = page == 1 ? "" : new UrlHelper(request).Link("roles", new { phrase = phrase, level = level, page = page - 1 });
                string nextPage = page == totalPages ? "" : new UrlHelper(request).Link("roles", new { phrase = phrase, level = level, page = page + 1 });

                roles = roles.OrderBy(r => r.Title).Skip((page - 1) * rolesPerPage).Take(rolesPerPage);

                List<WebRole> result = new List<WebRole>();
                foreach (var r in roles)
                {
                    WebRole wr = Mapping.ToWeb<Role, WebRole>(r);
                    if (wr == null) return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                           new InvalidCastException("Serialization failed"));
                    wr = HateMapping.Map<WebRole>(wr, request);
                    result.Add(wr);
                }
                return request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      TotalPages = totalPages,
                      PrevPage = prevPage,
                      NextPage = nextPage,
                      Result =result
                  });
            }

        }
        internal HttpResponseMessage GETRolesIndex(int page)
        {

            List<WebRole> result = new List<WebRole>();
            //Fix #13 for having invalid page number
            if (page <= 0)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    new ArgumentException(String.Format("page number cannot be zero or negative"))
                    );
            }
            using (academyContext db = new academyContext())
            {
                try
                {
                    var query = db.Roles;
                    UrlHelper urlHelper = new UrlHelper(request);

                    int totalPages = query.Count() % rolesPerPage == 0 ?
                        query.Count() / rolesPerPage :
                        (query.Count() / rolesPerPage) + 1;
                    string prevPage = default(string);
                    string nextPage = default(string);

                    if (page > 1 && page <= totalPages + 1) {
                        prevPage = urlHelper.Link("roles", new { page = page - 1 });                   
                    }
                    if (page >= 0 && page < totalPages) {
                        nextPage = urlHelper.Link("roles", new { page = page + 1 });
                    }

                    var roles = query.OrderBy(r => r.Title)
                        .Skip((page - 1) * rolesPerPage)
                        .Take(rolesPerPage)
                        .ToArray<Role>();
                    Array.ForEach(roles, r =>
                    {
                        WebRole wr = Mapping.ToWeb<Role, WebRole>(r);
                        if (wr == null)
                        {
                            request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                new InvalidCastException("Failed to serialize"));
                        }
                        wr = HateMapping.Map<WebRole>(wr, request);
                        result.Add(wr);
                    });

                    return request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           totalPages = totalPages,
                           prevPage = prevPage,
                           nextPage = nextPage,
                           result = result
                       });
                }
                catch (Exception)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }
            }

        }

        internal HttpResponseMessage GETRolesOfTitleLike(string phrase, int page)
        {
            List<WebRole> result = new List<WebRole>();
            using (academyContext db = new academyContext())
            {
                if (String.IsNullOrEmpty(phrase))
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException("search phrase cannot be null"));
                }
                var roles = db.Roles.Where(x => x.Title.ToLower().Contains(phrase)).Select(x => x);
                int totalPages = roles.Count() % rolesPerPage == 0 ?
                    roles.Count() / rolesPerPage :
                    (roles.Count() / rolesPerPage) + 1;
                string prevPage = page == 1 ? "" : new UrlHelper(request).Link("rolesOfTitle", new { title = phrase, exact = false, page = page - 1 });
                string nextPage = page == totalPages ? "" : new UrlHelper(request).Link("rolesOfTitle", new { title = phrase, exact = false, page = page + 1 });

                try
                {
                    roles = roles.OrderBy(x => x.Title).Skip((page - 1) * rolesPerPage).Take(rolesPerPage);

                    foreach (var r in roles)
                    {
                        WebRole we = Mapping.ToWeb<Role, WebRole>(r);
                        if (we == null)
                        {
                            return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                new InvalidCastException("Serialization failed"));
                        }
                        we = HateMapping.Map<WebRole>(we, request);
                        result.Add(we);
                    }
                    //massagin the reponse
                    return request.CreateResponse(HttpStatusCode.OK, new
                    {
                        TotalPages = totalPages,
                        urlPrevPage = prevPage,
                        urlNextPage = nextPage,
                        result = result,
                    });
                }
                catch (Exception xx)
                {

                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError, xx);
                }
            }

        }

        internal HttpResponseMessage GETRoleOfLevel(int param, int page)
        {
            List<WebRole> result = new List<WebRole>();
            using (academyContext db = new academyContext())
            {
                try
                {
                    var roles = db.Roles.Where(x => x.Level == param).Select(x => x);
                    int totalPages = roles.Count() % rolesPerPage == 0 ?
                        roles.Count() / rolesPerPage :
                        (roles.Count() / rolesPerPage) + 1;
                    string prevPage = page == 1 ? "" : new UrlHelper(request).Link("rolesOfLevel", new { level = param, page = page - 1 });
                    string nextPage = page == totalPages ? "" : new UrlHelper(request).Link("rolesOfLevel", new { level = param, page = page + 1 });
                    roles = roles.OrderBy(x => x.Title).Skip((page - 1) * rolesPerPage).Take(rolesPerPage);
                    foreach (var r in roles)
                    {
                        WebRole we = Mapping.ToWeb<Role, WebRole>(r);
                        if (we == null)
                        {
                            return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                new InvalidCastException("Web mapping failed"));
                        }
                        we = HateMapping.Map<WebRole>(we, request);
                        result.Add(we);
                    }
                    return request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          TotalPages = totalPages,
                          PrevPage = prevPage,
                          NextPage = nextPage,
                          Result = result
                      });
                }
                catch (Exception x)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed", x));
                }
            }

        }

        internal HttpResponseMessage GETRoleOfTitle(string pk)
        {
            WebRole result = null;
            if (String.IsNullOrEmpty(pk))
            {
                request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    new ArgumentNullException(
                        "Code of the role cannot be null"));
            }
            using (academyContext db = new academyContext())
            {
                var r = db.Roles
                    .Where(x => x.Title.ToLower() == pk.ToLower())
                    .FirstOrDefault();
                if (r == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format(
                        "Did not get the role of the code : {0}", pk)));
                }
                result = Mapping.ToWeb<Role, WebRole>(r);
                if (result == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("Web mapping failed"));
                }
                result = HateMapping.Map<WebRole>(result, request);
            }

            return request.CreateResponse<WebRole>(HttpStatusCode.OK, result);
        }

        internal HttpResponseMessage GETRoleOfEmployee(int id)
        {
            WebRole result = null;

            using (academyContext db = new academyContext())
            {
                var employee = db.Employees.Where(e => e.Id == id).FirstOrDefault();
                if (employee == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("employee with the id:{0} not found", id)));
                }
                result = Mapping.ToWeb<Role, WebRole>(employee.Role);
                if (result == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                         new InvalidCastException("Web mapping failed"));

                }
                result = HateMapping.Map<WebRole>(result, request);

            }
            return request.CreateResponse<WebRole>(HttpStatusCode.OK, result);
        }

        internal HttpResponseMessage POSTRole(WebRole newItem)
        {
            //no null items are added
            if (newItem == null || String.IsNullOrEmpty(newItem.Title))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("Invalid Role to add"));
            }
            Role toAdd = Mapping.ToPoco<Role, WebRole>(newItem);
            if (toAdd == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                  new InvalidCastException("Deserialization failed"));
            }
            using (academyContext db = new academyContext())
            {
                if (db.Roles.Where(x => x.Title == toAdd.Title).Count() != 0)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      new ArgumentException("Role cannot be duplicated, is already present"));
                }

                db.Roles.Add(toAdd);
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse<WebRole>(HttpStatusCode.Created,
                        Mapping.ToWeb<Role, WebRole>(toAdd));
                }
                catch (Exception xx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }

            }

        }

        internal HttpResponseMessage PUTRole(WebRole toUpdate)
        {
            if (toUpdate == null || string.IsNullOrEmpty(toUpdate.Title) || toUpdate.Level <= 0) { throw new ArgumentNullException("Invalid role to update"); }
            using (academyContext db = new academyContext())
            {
                var role = db.Roles.Where(x => x.Title == toUpdate.Title).Select(x => x).FirstOrDefault();
                if (role == null) { throw new ArgumentException("Role to update could not be found"); }
                role.Level = toUpdate.Level;
                try
                {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception xx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Error saving changes to the database", xx));
                }
            }

        }

        internal HttpResponseMessage DELETERole(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("Invalid role to delete"));
            }
            using (academyContext db = new academyContext())
            {
                var role = db.Roles.Where(x => x.Title == title).FirstOrDefault();
                if (role == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException("Role to delete could not be found"));
                }
                db.Roles.Remove(role);
                try
                {
                    db.SaveChanges();
                    //item has been deleted HTTP204
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception xx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Error saviing changes to the database", xx));
                }
            }
        }
    }
}