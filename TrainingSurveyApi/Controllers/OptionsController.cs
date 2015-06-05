using efAcademy.Entities.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingSurveyApi.Repository;
using TrainingSurveyApi.Filters;

namespace TrainingSurveyApi.Controllers
{
    [RoutePrefix("api/options")]
    [RepoReqIocFilter]
    public class OptionsController : BaseController
    {
        [HttpGet]
        [Route("", Name = "options")]
        public IHttpActionResult Index(int page=1) {
          return ResponseMessage(base.dataRepo.GETOptionsIndex(page));
        }

        [HttpGet]
        [Route("~/api/options/{id}", Name = "option")]
        public IHttpActionResult OfId(int id)
        {
            return ResponseMessage(base.dataRepo.GETOptionOfId(id));
        }
        [HttpGet]
        [Route("~/api/questions/{id}/options", Name = "questionToOptions")]
        public IHttpActionResult OfQuestions(int id) {
            return ResponseMessage(base.dataRepo.GETOptionsOfQuestion(id));
        }
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(WebFdbOption toUpdate) {
            return ResponseMessage(base.dataRepo.PUTOption(toUpdate));
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(WebFdbOption toAdd){
            return ResponseMessage(base.dataRepo.POSTOption(toAdd));
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(int id){
            return ResponseMessage(base.dataRepo.DELETEOption(id));
        }
    }
}
