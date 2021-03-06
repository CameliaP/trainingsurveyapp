﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using efAcademy.Entities;
using efAcademy.Entities.Web;
using efAcademy.Context;
using TrainingSurveyApi.Repository;
using TrainingSurveyApi.Controllers;
using TrainingSurveyApi.Filters;


namespace TrainingSurveyApi.Controllers
{
    //this is now modified to have the basic features for the trainings over the api
    [RoutePrefix("api/trainings")]
    [RepoReqIocFilter]
    public class TrainingsController : BaseController
    {
        [HttpGet]
        [Route("",Name="trainings")]
        public IHttpActionResult Index(int page = 1){
           return ResponseMessage(dataRepo.GETTrainingsIndex(page));
        }
        [HttpGet]
        [Route("{id:int}", Name = "training")]
        public IHttpActionResult OfId(int id) {
            return ResponseMessage(dataRepo.GETTrainingOfId(id));
        }
        [HttpGet]
        [Route("~/api/courses/{code}/trainings", Name = "courseToTrainings")]
        public IHttpActionResult OfId(string code) {
            return ResponseMessage(dataRepo.GETTrainingsOfCourse(code));
        }
        [HttpGet]
        [Route("~/api/questions/{id}/trainings", Name = "questionToTrainings")]
        public IHttpActionResult OfQuestion(int id) {
            return ResponseMessage(dataRepo.GETTrainingsForQuestion(id));
        }
        [HttpGet]
        [Route("~/api/employees/{id}/trainings", Name = "employeeToTrainings")]
        public IHttpActionResult AttendedByEmployee(int id) {
            return ResponseMessage(dataRepo.GETTrainingsAttended(id));
        }
        [HttpGet]
        [Route("~/api/tutors/{id}/trainings", Name = "tutorToTrainings")]
        public IHttpActionResult AnchoredByTutor(int id) {
            return ResponseMessage(dataRepo.GETTrainingsAnchored(id));
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(WebTraining toAdd) {
            return ResponseMessage(dataRepo.POSTTraining(toAdd));

        }
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(WebTraining toUpdate) {
            return ResponseMessage(dataRepo.PUTTraining(toUpdate));

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id) {
            return ResponseMessage(dataRepo.DELETETraining(id));
        }
    }
}
