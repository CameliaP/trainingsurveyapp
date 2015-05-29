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
    [RepoReqIocFilter]
    [RoutePrefix("api/projects")]
    public class ProjectsController : BaseController
    {
        [HttpGet]
        [Route("", Name="projects")]
        public IHttpActionResult Index(int page=1)
        {
            return ResponseMessage(dataRepo.GETProjectsIndex(page));
        }

        [HttpGet]
        [Route("~/api/employees/{id}/project", Name="employeeToProject")]
        public IHttpActionResult OfEmployee(int id)
        {
            return ResponseMessage(dataRepo.GETProjectOfEmployee(id));
        }

        [HttpGet]
        [Route("{code}", Name="project")]
        public IHttpActionResult OfCode(string code)
        {
            return ResponseMessage(dataRepo.GETProjectOfCode(code));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(WebProject toUpdate)
        {
            return ResponseMessage(dataRepo.PUTProject(toUpdate));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(WebProject toAdd)
        {
            return ResponseMessage(dataRepo.POSTPRoject(toAdd));
        }

        [HttpDelete]
        [Route("{code}")]
        public IHttpActionResult Delete(string code)
        {
            return ResponseMessage(dataRepo.DELETEProject(code));
        }

    }
}
