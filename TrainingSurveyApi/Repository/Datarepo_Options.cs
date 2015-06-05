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

        internal HttpResponseMessage GETOptionsIndex(int page) {
            if (page<=0) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("page number for the index of options cannot be zero or negative"));
            }
            List<WebFdbOption> result = new List<WebFdbOption>();
            using (academyContext db = new academyContext()) {
                var options = db.FdbOptions.Select(xx=>xx);
                int totalPages = options.Count() % optionsPerPage == 0 ? 
                    options.Count() / optionsPerPage :
                    (options.Count() / optionsPerPage) + 1;
                string prev = default(string);
                string next = default(string);

                if (page >1 && page <=totalPages+1) {
                    prev = urlHelper.Link("options", new { page = page - 1 });
                }
                if (page >1 && page <totalPages) {
                    next = urlHelper.Link("options", new { page = page + 1 });
                }

                //pagination and refining the query
                options = options
                    .OrderBy(xx => xx.Id)
                    .Skip((page - 1) * optionsPerPage)
                    .Take(optionsPerPage);

                //serialization and hateoas mapping
                foreach (var o in options) {
                    WebFdbOption wo = Mapping.ToWeb<FdbOption, WebFdbOption>(o);
                    if (wo==null) {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new InvalidCastException("Serialization failed"));
                    }
                    wo = HateMapping.Map<WebFdbOption>(wo,request);
                    result.Add(wo);
                }
                //forming the http response for dispatch
                return request.CreateResponse(HttpStatusCode.OK,
                    new {
                        totalPages = totalPages,
                        prevPage = prev,
                        nextPage = next,
                        data = result
                    });
            }
        }

        internal HttpResponseMessage GETOptionOfId(int id) {
            using (academyContext db = new academyContext()) {
                var option = db.FdbOptions.Where(x => x.Id == id).FirstOrDefault();
                if (option ==null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("Option of the id {0} not found", id)));
                }
                //mapping onto the web objects
                WebFdbOption wo = Mapping.ToWeb<FdbOption, WebFdbOption>(option);
                if (wo ==null) {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("Serialization failed"));
                }
                wo = HateMapping.Map<WebFdbOption>(wo,request);
                //dispatching the result in http response message
                return request.CreateResponse(HttpStatusCode.OK, new {
                    data = wo
                });
            }
        }

        internal HttpResponseMessage GETOptionsOfQuestion(int id) {
            List<WebFdbOption> result = new List<WebFdbOption>();
            using (academyContext db = new academyContext()) {
                var question = db.FdbQuestions.Where(x => x.Id==id).FirstOrDefault();
                if (question ==null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("No feedback question of the id {0} found")));
                }
                foreach (var o in question.Options) {
                    WebFdbOption wo = Mapping.ToWeb<FdbOption, WebFdbOption>(o);
                    if (wo == null) {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("serialization failed"));
                    }
                    wo = HateMapping.Map<WebFdbOption>(wo, request);
                    result.Add(wo);
                }
                return request.CreateResponse(HttpStatusCode.OK, 
                    new {
                        data = result
                    });
            }
        }

        internal HttpResponseMessage PUTOption(WebFdbOption toUpdate) {
            if (toUpdate ==null || String.IsNullOrWhiteSpace(toUpdate.Text)) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("Option to update or text of the option to update cannot be null"));
            }
            using (academyContext db = new academyContext()) {
                var option = db.FdbOptions.Where(x => x.Id == toUpdate.Id).FirstOrDefault();
                if (option==null) {
                     return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException(String.Format("Option with the id :{0} not found", toUpdate.Id)));
                }
                option.Text = toUpdate.Text;
                option.Sentiment = toUpdate.Sentiment;
                try {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception) {

                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                     new IOException("Database operation failed"));
                }
            }
        }

        internal HttpResponseMessage POSTOption(WebFdbOption toAdd) {
            if (toAdd == null || String.IsNullOrWhiteSpace(toAdd.Text)) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentNullException("Option to add or text of the option to add cannot be null"));
            }
            using (academyContext db = new academyContext()) {
                var option = db.FdbOptions.Where(x => 
                    x.Text.ToLower() == toAdd.Text.ToLower()).FirstOrDefault();
                if (option != null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException(String.Format("Option with the text : {0} already exists", toAdd.Text)));
                }
                FdbOption o = Mapping.ToPoco<FdbOption, WebFdbOption>(toAdd);
                db.FdbOptions.Add(o);
                try {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception) {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                      new IOException("Database operation failed"));
                }
            }
        }

        internal HttpResponseMessage DELETEOption(int id) {
            using (academyContext db = new academyContext()) {
                var option = db.FdbOptions.Where(x => x.Id == id).FirstOrDefault();
                if (option ==null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException(String.Format("Option with the id :{0} not found", id))); 
                }
                db.FdbOptions.Remove(option);
                try {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception) {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                       new IOException("Database operation failed"));
                }
            }
        }
    }
}