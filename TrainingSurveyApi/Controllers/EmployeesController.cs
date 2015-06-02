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
    [RoutePrefix("api/employees")]
    [RepoReqIocFilter]
    public class EmployeesController : BaseController
    {
        [Route("", Name = "employees")]
        [HttpGet]
        public IHttpActionResult Index(int page=1)
        {
            return ResponseMessage(base.dataRepo.GETEmployeesIndex(page));
        }
        [HttpGet]
        [Route("{id:int}", Name = "employee")]
        public IHttpActionResult OfId(int id)
        {
            return ResponseMessage(base.dataRepo.GETEmployeeOfId(id));
        }
        [Route("{like:alpha}", Name="employeesLike")]
        [HttpGet]
        public IHttpActionResult Likely(string like = "", int page=1)
        {
            return ResponseMessage(base.dataRepo.GETEmployeesContainingPhrase(like, page));
        }
        [HttpGet]
        [Route("~/api/projects/{code}/employees", Name = "projectToEmployees")]
        public IHttpActionResult OfProject(string code)
        {
            return ResponseMessage(base.dataRepo.GETEmployeeOfProject(code));
        }
        [HttpGet]
        [Route("~/api/units/{code}/employees", Name = "unitToEmployees")]
        public IHttpActionResult OfUnit(string code)
        {
            return ResponseMessage(base.dataRepo.GETEmployeeOfUnit(code));
        }
        [HttpGet]
        [Route("~/api/locations/{code}/employees", Name = "locationToEmployees")]
        public IHttpActionResult OfLocation(string code)
        {
            return ResponseMessage(base.dataRepo.GETEmployeeOfLocation(code));
        }
        [HttpGet]
        //GET: api/roles/{title}/employees
        [Route("~/api/roles/{title}/employees", Name = "roleToEmployees")]
        public IHttpActionResult EmployeesOfRole(string title)
        {
            return ResponseMessage(base.dataRepo.GETEmployeesOfRole(title));
        }
       
       
        [Route("")]
        [HttpPost]
        public IHttpActionResult AddNew(WebEmployee toAdd) {
            return ResponseMessage(base.dataRepo.POSTEmployee(toAdd));
        }
        [Route("")]
        [HttpPut]
        public IHttpActionResult Update(WebEmployee toUpdate) {

            return ResponseMessage(base.dataRepo.PUTEmployee(toUpdate));
        }
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id) 
        {
            return ResponseMessage(base.dataRepo.DELETEEmployee(id)); 
        }
      
    }
}
