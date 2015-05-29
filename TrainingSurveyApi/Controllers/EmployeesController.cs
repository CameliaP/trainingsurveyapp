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
        [HttpGet]
        [Route("~/api/employees/{id:int}", Name = "employee")]
        public IHttpActionResult OfId(int id)
        {

            try
            {
                WebEmployee result = base.dataRepo.GETEmployeeOfId(id);
                return Ok(result);
            }
            catch (Exception xx)
            {
                return base.ErrorResponse(xx);
            }
        }
        [HttpGet]
        [Route("~/api/projects/{code}/employees", Name = "projectToEmployees")]
        public IHttpActionResult OfProject(string code)
        {
            try
            {
                WebEmployee[] result = base.dataRepo.GETEmployeeOfProject(code).ToArray<WebEmployee>();
                return Ok(result);
            }
            catch (Exception xx)
            {
                return base.ErrorResponse(xx);
            }

        }

        [HttpGet]
        [Route("~/api/units/{code}/employees", Name = "unitToEmployees")]
        public IHttpActionResult OfUnit(string code)
        {
            try
            {
                WebEmployee[] result = base.dataRepo.GETEmployeeOfUnit(code).ToArray<WebEmployee>();
                return Ok(result);
            }
            catch (Exception xx)
            {
                return base.ErrorResponse(xx);
            }
        }
        [HttpGet]
        [Route("~/api/locations/{code}/employees", Name = "locationToEmployees")]
        public IHttpActionResult OfLocation(string code)
        {
            try
            {
                WebEmployee[] result = base.dataRepo.GETEmployeeOfLocation(code).ToArray<WebEmployee>();
                return Ok(result);
            }
            catch (Exception xx)
            {
                return base.ErrorResponse(xx);
            }
        }
        [HttpGet]
        //GET: api/roles/{title}/employees
        [Route("~/api/roles/{title}/employees", Name = "roleToEmployees")]
        public IHttpActionResult Employees(string title)
        {

            IEnumerable<WebEmployee> result = null;
            try
            {
                result = base.dataRepo.GETEmployeesOfRole(title);

            }
            catch (Exception err)
            {
                throw;
            }
            return Ok(result);
        }
        [Route("",Name="employees")]
        [HttpGet]
        public IHttpActionResult Index(int top = 10, int skip = 0) {
            
            try
            {
                WebEmployee[] result = base.dataRepo.GETEmployeesIndex(top, skip).ToArray<WebEmployee>();
                return Ok(result);
            }
            catch (Exception xx)
            {
                //catch the type of the exception and then send the relevant result 
                return base.ErrorResponse(xx);
            }
        }
        [Route("{like:alpha}")]
        [HttpGet]
        public IHttpActionResult Likely(string like="")
        {
            try
            {
                WebEmployee[] result = base.dataRepo.GETEmployeesContainingPhrase(like)
                    .ToArray<WebEmployee>();
                return Ok(result);
            }
            catch (Exception xx)
            {
                return base.ErrorResponse(xx);
            }
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult AddNew(WebEmployee toAdd) {
            try
            {
                base.dataRepo.POSTEmployee(toAdd);
                return Ok();
            }
            catch (Exception xx)
            {
                return base.ErrorResponse(xx);
            }
        }
        [Route("")]
        [HttpPut]
        public IHttpActionResult Update(WebEmployee toUpdate) {
            
            if (toUpdate == null) { return BadRequest("the employee to update cannot be null"); }
            try
            {
                base.dataRepo.PUTEmployee(toUpdate);
                return Ok();
            }
            catch (Exception xx)
            {
                return base.ErrorResponse(xx);
            }
        }
        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(int id) {
            
            try
            {
                WebEmployee toDelete = base.dataRepo.GETEmployeeOfId(id);
                if (toDelete == null) { return BadRequest("employee to delete could not be found"); }
                base.dataRepo.DELETEEmployee(toDelete);
                return Ok();
            }
            catch (Exception xx)
            {
                return base.ErrorResponse(xx);
            }
        }
      
    }
}
