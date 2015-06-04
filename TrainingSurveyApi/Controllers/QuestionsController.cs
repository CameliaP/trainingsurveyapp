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
    //this is now modified to have the basic features for the trainings over the api
    [RoutePrefix("api/questions")]
    [RepoReqIocFilter]
    public class QuestionsController : BaseController
    {
        [HttpGet]
        [Route("",Name="questions")]
        public IHttpActionResult Index(int page = 1){
           return ResponseMessage(dataRepo.GETQuestionsIndex(page));
        }
        [HttpGet]
        [Route("{id:int}", Name = "question")]
        public IHttpActionResult OfId(int id) {
            return ResponseMessage(dataRepo.GETQuestionOfId(id));
        }
       
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(WebFdbQuestion toAdd) {
            return ResponseMessage(dataRepo.POSTQuestion(toAdd));

        }
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(WebFdbQuestion toUpdate) {
            return ResponseMessage(dataRepo.PUTQuestion(toUpdate));

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id) {
            return ResponseMessage(dataRepo.DELETEQuestion(id));
        }
    }
}
