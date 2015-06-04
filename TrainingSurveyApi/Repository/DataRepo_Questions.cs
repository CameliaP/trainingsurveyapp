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

namespace TrainingSurveyApi.Repository {
    public sealed partial class DataRepo {
        


        internal HttpResponseMessage GETQuestionsIndex(int page) {
            //check for invalid page numbers
            if (page <= 0) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("page number for the questions index cannot be zero or negative"));
            }
            List<WebFdbQuestion> result = new List<WebFdbQuestion>();
            using (academyContext db = new academyContext()) {
                var questions = db.FdbQuestions.Select(x => x);
                //creating pagination
                int totalPages = questions.Count() % questionsPerPage == 0 ?
                    questions.Count() / questionsPerPage :
                    (questions.Count() / questionsPerPage) + 1;
                string prev = default(string);
                string next = default(string);
                if (page > 1 && page <= totalPages + 1) {
                    prev = new UrlHelper(request).Link("questions", new { page = page - 1 });
                }
                if (page >= 1 && page < totalPages) {
                    next = new UrlHelper(request).Link("questions", new { page = page + 1 });
                }
                //refining the query to get the paged results
                questions = questions.OrderBy(x => x.Id).Skip((page - 1) * questionsPerPage).Take(questionsPerPage);
                foreach (var t in questions) {
                    //preparing the results
                    WebFdbQuestion wq = Mapping.ToWeb<FdbQuestion, WebFdbQuestion>(t);
                    if (wq == null) {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new InvalidCastException("Serialization failed"));
                    }
                    wq = HateMapping.Map<WebFdbQuestion>(wq, request);
                    result.Add(wq);
                }
                //http ok , result is sent as json
                return request.CreateResponse(HttpStatusCode.OK,
                    new {
                        totalPages = totalPages,
                        prevPage = prev,
                        nextPage = next,
                        results = result
                    });
            }
        }

        internal HttpResponseMessage GETQuestionOfId(int id) {
            //check for invalid page numbers
            WebFdbQuestion result = default(WebFdbQuestion);
            using (academyContext db = new academyContext()) {

                //getting the training with certain id
                var question = db.FdbQuestions.Select(x => x).Where(x => x.Id == id)
                    .FirstOrDefault();
                if (question == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format(
                            "question of the id :{0} not found", id)));
                }
                //serialization compatible question
                result = Mapping.ToWeb<FdbQuestion, WebFdbQuestion>(question);
                if (result == null) {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("failed to serialize the object"));
                }
                result = HateMapping.Map<WebFdbQuestion>(result, request);

                //check to see if training is nothing
                return request.CreateResponse(HttpStatusCode.OK,
                    new {
                        results = result
                    });
            }
        }

        internal HttpResponseMessage POSTQuestion(WebFdbQuestion toAdd) {
            if (toAdd == null || String.IsNullOrEmpty(toAdd.Text)) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("New training to be added cannot be null or have empty text"));
            }
            using (academyContext db = new academyContext()) {
                if (db.FdbQuestions.Where(x => x.Text.ToLower() == toAdd.Text.ToLower()).Count() != 0) {
                    //same titled training is already in the database
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format(
                            "Question with the text : {0} already exists",
                            toAdd.Text)));
                }
                FdbQuestion t = Mapping.ToPoco<FdbQuestion, WebFdbQuestion>(toAdd);
                db.FdbQuestions.Add(t);
                try {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception) {
                    //handle the database saving exception
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                }
            }
        }

        internal HttpResponseMessage PUTQuestion(WebFdbQuestion toUpdate) {
            using (academyContext db = new academyContext()) {
                var question = db.FdbQuestions.Where(x => x.Id == toUpdate.Id).FirstOrDefault();
                if (toUpdate == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("question with the id :{0} not found", toUpdate.Id)));
                }
                question.Text = toUpdate.Text;
              
                try {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception) {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("Database operation failed"));
                    throw;
                }
            }
        }

        internal HttpResponseMessage DELETEQuestion(int id) {
            using (academyContext db = new academyContext()) {
                var question = db.FdbQuestions.Where(x => x.Id == id).FirstOrDefault();
                if (question == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("Question with the id :{0} not found", id)));
                }
                db.FdbQuestions.Remove(question);
                try {
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception) {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new IOException("database operation failed"));
                }
            }
        }

    }
}