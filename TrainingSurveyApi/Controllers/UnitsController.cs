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
    [RoutePrefix("api/units")]
    [RepoReqIocFilter]
    public class UnitsController : BaseController
    {
        [HttpGet]
        [Route("", Name = "units")]
        public IHttpActionResult Index(int page=1) {
          return ResponseMessage(base.dataRepo.GETUnitsIndex(page));
        }

        [HttpGet]
        [Route("~/api/units/{code:alpha}", Name = "unit")]
        public IHttpActionResult OfCode(string code)
        {
            return ResponseMessage(base.dataRepo.GETUnitOfCode(code));
        }
        [HttpGet]
        [Route("~/api/employees/{id}/unit", Name = "employeeToUnit")]
        public IHttpActionResult OfEmployee(int id) {
            return ResponseMessage(base.dataRepo.GETUnitOfEmployee(id));
        }
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(WebUnit toUpdate) {
            return ResponseMessage(base.dataRepo.PUTUnit(toUpdate));
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(WebUnit toAdd){
            return ResponseMessage(base.dataRepo.POSTUnit(toAdd));
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(string code){
            return ResponseMessage(base.dataRepo.DELETEUnit(code));
        }
    }
}
