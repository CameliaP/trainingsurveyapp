using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using efAcademy;
using efAcademy.Entities;
using efAcademy.Entities.Web;
using efAcademy.Context;
using TrainingSurveyApi.Models;
using TrainingSurveyApi.Repository;
using TrainingSurveyApi.Filters;
using WebObjectMapping.Attributes;
using WebObjectMapping.Services;

namespace TrainingSurveyApi.Controllers
{
    [RoutePrefix("api/locations")]
    [RepoReqIocFilter]
    public class LocationsController : BaseController
    {
        [HttpGet]
        [Route("", Name = "locations")]
        public IHttpActionResult Index(int page=1) {
            return ResponseMessage(base.dataRepo.GETLocationsIndex(page));
        }

        [HttpGet]
        [Route("{code}",Name="location")]
        public IHttpActionResult OfCode(string code,int page=1,bool exact = true){
            
            return exact ==true ? 
                ResponseMessage(base.dataRepo.GETLocationOfCode(code)):
                ResponseMessage(base.dataRepo.GETLocationsContainingPhrase(code, page));
          
        }
        [HttpGet]
        [Route("~/employees/{id}/location", Name = "employeeToLocation")]
        public IHttpActionResult OfEmployee(int id) {
            return ResponseMessage(base.dataRepo.GETLocationOfEmployee(id));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(WebLocation toUpdate){
            return ResponseMessage(base.dataRepo.PUTLocation(toUpdate));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddNew(WebLocation toAdd){
            return ResponseMessage(base.dataRepo.POSTLocation(toAdd));
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(string code){
            return ResponseMessage(base.dataRepo.DELETELocation(code));        
        }
    }
}
