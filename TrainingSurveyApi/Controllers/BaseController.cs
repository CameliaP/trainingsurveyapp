using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingSurveyApi.Repository;

namespace TrainingSurveyApi.Controllers
{
    public class BaseController : ApiController
    {

        protected DataRepo dataRepo= null;

        public DataRepo DataRepo
        {
            get {
                return this.dataRepo;
            }
            set { 
                this.dataRepo = value;
            }
        }

        public IHttpActionResult ErrorResponse(Exception xx) {
            if (xx == null) { return Ok();}
            switch (xx.GetType().Name)
            {
                case "ArgumentException":
                case "ArgumentNullException":
                    return BadRequest(xx.Message);
                case "InvalidCastException":
                case "IOException":
                    return InternalServerError(xx);
                default:
                    break;
            }
            return InternalServerError(xx);
        }

    }
}
