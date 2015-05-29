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
    
    [RoutePrefix("api/roles")]
    [RepoReqIocFilter]
    public class RolesController : BaseController
    {
        [HttpGet]
        [Route("~/api/employees/{id}/role", Name = "employeeToRole")]
        public IHttpActionResult OfEmployee(int id) {
           return ResponseMessage(base.dataRepo.GETRoleOfEmployee(id));
        }
        [HttpGet]
        [Route("{title}", Name="rolesOfTitle")]
        public IHttpActionResult OfTitle(string title, bool exact = true, int page=1)
        {
            return exact == true ?
                ResponseMessage(dataRepo.GETRoleOfTitle(title)) :
                ResponseMessage(dataRepo.GETRolesOfTitleLike(title, page));
        }
        //---------------------------------------------------------------------------
        [HttpGet]
        [Route("", Name = "roles")]
        public IHttpActionResult Index(int page = 1) {
            return base.ResponseMessage(base.dataRepo.GETRolesIndex(page));
        }
        [HttpGet]
        [Route("{level:int}", Name = "rolesOfLevel")]
        public IHttpActionResult ofLevel(int level, int page=1)
        {
            return base.ResponseMessage(base.dataRepo.GETRoleOfLevel(level,page));
        }
      
        //------------------------------------------------------------------------------        
        [HttpPut]
        [Route("")]
        //PUT: api/roles
        public IHttpActionResult Update(WebRole w)
        {
            return ResponseMessage(base.dataRepo.PUTRole(w));
        }
        //------------------------------------------------------------------------------        
        [HttpPost]
        [Route("")]
        //POST: api/roles
        public IHttpActionResult Add(WebRole w)
        {
            return ResponseMessage(base.dataRepo.POSTRole(w));
        }
        //------------------------------------------------------------------------------        
        [HttpDelete]
        [Route("")]
        //DELETE api/roles/{title}
        public IHttpActionResult Delete(string title)
        {
            return ResponseMessage(base.dataRepo.DELETERole(title));
        }
        //------------------------------------------------------------------------------        

       
       
    }
}
