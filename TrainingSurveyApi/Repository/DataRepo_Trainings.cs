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

        internal HttpResponseMessage GETTrainingsOfCourse(string code) {
            List<WebTraining> result = new List<WebTraining>();
            if (String.IsNullOrEmpty(code)) {
                //invalid code for the course 
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("code for the course cannnot be null or empty"));
            }
            using (academyContext db = new academyContext()) {
                var course = db.Courses.Where(c => c.Code.ToLower() == code.ToLower())
                    .FirstOrDefault();
                if (course ==null) {
                    //did not get the course with the code
                     return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("Course for the code :{0} not found", code));
                }
                foreach (var t in course.Trainings) {
                    //web mapping an hateoas mapping
                    WebTraining wt = Mapping.ToWeb<Training, WebTraining>(t);
                    if (wt ==null) {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new IOException("Serialization failed"));
                    }
                    wt = HateMapping.Map<WebTraining>(wt, request);
                    result.Add(wt);
                }
                //status ok, sending results
                return request.CreateResponse(HttpStatusCode.OK,
                    new {
                        results = result
                    });
            }
        }

        internal HttpResponseMessage GETTrainingsForQuestion(int id) {
            List<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext()) {
                var question = db.FdbQuestions.Where(q => q.Id == id).FirstOrDefault();
                if (question ==null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("quesiton with the id :{0} not found", id)));
                }
                foreach (var t in question.Trainings) {
                    WebTraining wt = Mapping.ToWeb<Training, WebTraining>(t);
                    if (wt == null) {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new IOException("Serialization failed"));
                    }
                    wt = HateMapping.Map<WebTraining>(wt, request);
                    result.Add(wt);
                }
                //status ok, sending results
                return request.CreateResponse(HttpStatusCode.OK,
                    new {
                        results = result
                    });
            }
        }

        internal HttpResponseMessage GETTrainingsAttended(int id) {
            List<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext()) {
                var employee = db.Employees.Where(e => e.Id == id).FirstOrDefault();
                if (employee == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("employee with the id :{0} not found", id)));
                }
                foreach (var t in employee.Attended) {
                    WebTraining wt = Mapping.ToWeb<Training, WebTraining>(t);
                    if (wt == null) {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new IOException("Serialization failed"));
                    }
                    wt = HateMapping.Map<WebTraining>(wt, request);
                    result.Add(wt);
                }
                //status ok, sending results
                return request.CreateResponse(HttpStatusCode.OK,
                    new {
                        results = result
                    });
            }
        }

        internal HttpResponseMessage GETTrainingsAnchored(int id) {
            List<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext()) {
                var tutor = db.Tutors.Where(e => e.Id == id).FirstOrDefault();
                if (tutor == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("tutor with the id :{0} not found", id)));
                }
                foreach (var t in tutor.Anchored) {
                    WebTraining wt = Mapping.ToWeb<Training, WebTraining>(t);
                    if (wt == null) {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new IOException("Serialization failed"));
                    }
                    wt = HateMapping.Map<WebTraining>(wt, request);
                    result.Add(wt);
                }
                //status ok, sending results
                return request.CreateResponse(HttpStatusCode.OK,
                    new {
                        results = result
                    });
            }
        }
        internal HttpResponseMessage GETTrainingsIndex(int page) {
            //check for invalid page numbers
            if (page <= 0) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("page number for the index of trainings cannot be zero or negative"));
            }
            List<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext()) {
                var trainings = db.Trainings.Select(x => x);
                //creating pagination
                int totalPages = trainings.Count() % trainingsPerPage == 0 ?
                    trainings.Count() / trainingsPerPage :
                    (trainings.Count() / trainingsPerPage) + 1;
                string prev = default(string);
                string next = default(string);
                if (page > 1 && page <= totalPages + 1) {
                    prev = new UrlHelper(request).Link("trainings", new { page = page - 1 });
                }
                if (page >= 1 && page < totalPages) {
                    next = new UrlHelper(request).Link("trainings", new { page = page + 1 });
                }
                //refining the query to get the paged results
                trainings = trainings.OrderBy(x => x.Id).Skip((page - 1) * trainingsPerPage).Take(trainingsPerPage);
                foreach (var t in trainings) {
                    //preparing the results
                    WebTraining wt = Mapping.ToWeb<Training, WebTraining>(t);
                    if (wt == null) {
                        return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new InvalidCastException("Serialization failed"));
                    }
                    wt = HateMapping.Map<WebTraining>(wt, request);
                    result.Add(wt);
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

        internal HttpResponseMessage GETTrainingOfId(int id) {
            //check for invalid page numbers
            WebTraining result = default(WebTraining);
            using (academyContext db = new academyContext()) {

                //getting the training with certain id
                var training = db.Trainings.Select(x => x).Where(x => x.Id == id).FirstOrDefault();
                if (training == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format(
                            "training of the id :{0} not found", id)));
                }
                //serialization compatible training
                result = Mapping.ToWeb<Training, WebTraining>(training);
                if (result == null) {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        new InvalidCastException("failed to serialize the object"));
                }
                result = HateMapping.Map<WebTraining>(result, request);

                //check to see if training is nothing
                return request.CreateResponse(HttpStatusCode.OK,
                    new {
                        results = result
                    });
            }
        }

        internal HttpResponseMessage POSTTraining(WebTraining toAdd) {
            if (toAdd == null || String.IsNullOrEmpty(toAdd.Title)) {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new ArgumentException("new training be added cannot be null or have null title"));
            }
            using (academyContext db = new academyContext()) {
                if (db.Trainings.Where(x => x.Title.ToLower() == toAdd.Title.ToLower()).Count() != 0) {
                    //same titled training is already in the database
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format(
                            "training with the title : {0} already exists",
                            toAdd.Title)));
                }
                Training t = Mapping.ToPoco<Training, WebTraining>(toAdd);
                db.Trainings.Add(t);
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

        internal HttpResponseMessage PUTTraining(WebTraining toUpdate) {
            using (academyContext db = new academyContext()) {
                var training = db.Trainings.Where(x => x.Id == toUpdate.Id).FirstOrDefault();
                if (toUpdate == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("training with the id :{0} not found", toUpdate.Id)));
                }
                training.Title = toUpdate.Title;
                training.Start = toUpdate.Start;
                training.End = toUpdate.End;
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

        internal HttpResponseMessage DELETETraining(int id) {
            using (academyContext db = new academyContext()) {
                var training = db.Trainings.Where(x => x.Id == id).FirstOrDefault();
                if (training == null) {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new ArgumentException(String.Format("Training with the id :{0} not found", id)));
                }
                db.Trainings.Remove(training);
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