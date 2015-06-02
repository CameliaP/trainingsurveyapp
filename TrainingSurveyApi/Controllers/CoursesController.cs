using System;
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
    [RoutePrefix("api/courses")]
    [RepoReqIocFilter]
    public class CoursesController : BaseController
    {
        [HttpGet]
        [Route("", Name = "courses")]
        public IHttpActionResult Index(int page = 1) {
            return ResponseMessage(dataRepo.GETCourseIndex(page));
        }
        [HttpGet]
        [Route("{code}", Name = "course")]
        public IHttpActionResult OfCode(string code)
        {
            return ResponseMessage(dataRepo.GETCourseOfCode(code));
        }
        [HttpGet]
        [Route("~/api/trainings/{id}/course", Name = "trainingToCourse")]
        public IHttpActionResult OfTraining(int id) {
            return ResponseMessage(dataRepo.GETCourseOfTraining(id));
        }

        [HttpDelete]
        [Route("{code}")]
        public IHttpActionResult Delete(string code)
        {
            return ResponseMessage(dataRepo.DELETECourseOfCode(code));
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddNew(WebCourse toAdd)
        {
            return ResponseMessage(dataRepo.POSTCourse(toAdd));
        }
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(WebCourse toUpdate)
        {
            return ResponseMessage(dataRepo.PUTCourse(toUpdate));
        }

       
    }
}
