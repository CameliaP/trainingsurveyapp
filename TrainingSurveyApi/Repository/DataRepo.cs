using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using efAcademy.Context;
using efAcademy.Entities;
using efAcademy.Entities.Web;
using System.Data;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http.Routing;
using WebObjectMapping.Services;
using WebObjectMapping.Attributes;
using System.IO;


namespace TrainingSurveyApi.Repository
{
    public sealed partial class DataRepo
    {
        //derivitives need not access the request
        protected const int rolesPerPage = 5;
        protected const int employeesPerPage = 10;
        protected const int locationsPerPage = 5;
        protected const int unitsPerPage = 10;
        protected const int projectsPerPage = 20;
        protected const int coursesPerPage=20;
        protected const int trainingsPerPage = 20;

        protected HttpRequestMessage request;
        protected UrlHelper urlHelper;

        public HttpRequestMessage Request
        {
            set
            {
                request = value;
                urlHelper = new UrlHelper(request);//setting the url helper
            }
        }

    }
}