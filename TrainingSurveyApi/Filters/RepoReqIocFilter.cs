using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net.Http;
using TrainingSurveyApi.Repository;
using TrainingSurveyApi.Controllers;

namespace TrainingSurveyApi.Filters
{
    public class RepoReqIocFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //setting up the base.dataRepository IOC with request
            (actionContext.ControllerContext.Controller as BaseController).DataRepo = new DataRepo();
            (actionContext.ControllerContext.Controller as BaseController).DataRepo.Request = actionContext.Request;

        }
    }

    
}