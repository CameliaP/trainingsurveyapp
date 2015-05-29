using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using efAcademy;
using efAcademy.Context;
using efAcademy.Entities.Web;
using efAcademy.Entities;
using System.Data.Entity;
using WebObjectMapping.Attributes;
using WebObjectMapping.Services;



namespace TrainingSurveyApi.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        /// <summary>
        /// gets the paged index of the courses 
        /// </summary>
        /// <param name="top">number of the results from the top when arranged ascending with code</param>
        /// <param name="skip">number of results being skipped from the top when arranged ascending by the code</param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult Index(int top = 10, int skip = 0)
        {
            ICollection<WebCourse> result = new List<WebCourse>();
            using (academyContext db = new academyContext())
            {
                var courses = db.Courses.OrderBy(c => c.Code).Skip(skip).Take(top).ToArray<Course>();
                Array.ForEach(courses, c =>
                {
                    result.Add(Mapping.ToWeb<Course, WebCourse>(c));
                });
            }
            return Ok(result);
        }
        /// <summary>
        /// searches in the code and the title of the course and returns the same
        /// </summary>
        /// <param name="like">search phrase that gets you the result</param>
        /// <returns>std web result</returns>
        [Route("{like}")]
        [HttpGet]
        public IHttpActionResult IndexOfLikely(string like)
        {
            ICollection<WebCourse> result = new List<WebCourse>();
            using (academyContext db = new academyContext())
            {
                var courses = db.Courses
                    .Where(c =>
                        c.Title.ToLower().Contains(like.ToLower()) ||
                        c.Code.ToLower().Contains(like.ToLower()))
                    .Select(x => x)
                    .ToArray<Course>();
                Array.ForEach(courses, c =>
                {
                    result.Add(Mapping.ToWeb<Course, WebCourse>(c));
                });
            }
            return Ok(result);
        }
        /// <summary>
        /// given the course code gets all the tranings that are hosted
        /// </summary>
        /// <param name="code">course code that is needed to search</param>
        /// <returns>std web response</returns>
        [Route("{code}/trainings")]
        [HttpGet]
        public IHttpActionResult TrainingsUnderCode(string code)
        {
            ICollection<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext())
            {
                var course = db.Courses.Where(c => c.Code == code).FirstOrDefault();
                var tranings = course.Trainings.ToArray<Training>();
                Array.ForEach(tranings, t =>
                {
                    result.Add(Mapping.ToWeb<Training, WebTraining>(t));
                });
            }
            return Ok(result);
        }
    }
}
