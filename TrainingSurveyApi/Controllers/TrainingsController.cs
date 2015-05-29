using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using efAcademy;
using System.Data.Entity;

using efAcademy.Entities;
using efAcademy.Entities.Web;
using efAcademy.Context;
using WebObjectMapping.Attributes;
using WebObjectMapping.Services;
namespace TrainingSurveyApi.Controllers
{
    [RoutePrefix("api/Trainings")]
    public class TrainingsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Paged(int top = 10, int skip = 0)
        {
            ICollection<WebTraining> result = new List<WebTraining>();

            using (academyContext db = new academyContext())
            {
                //inside the database, getting all the Trainings. 
                var trainings = db.Trainings.OrderBy(x => x.Start).Skip(skip).Take(top).ToArray<Training>();
                Array.ForEach(trainings, t => { result.Add(Mapping.ToWeb<Training, WebTraining>(t)); });
            }
            return Ok(result);
        }
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult OfId(int id)
        {
            WebTraining result = null;
            using (academyContext db = new academyContext())
            {
                var traning = db.Trainings.Where(x => x.Id == id).FirstOrDefault();
                result = Mapping.ToWeb<Training, WebTraining>(traning); 
            }

            return Ok(result);
        }
        [Route("{start}/{end}")]
        [HttpGet]
        public IHttpActionResult BetweenDates(DateTime start, DateTime end)
        {
            ICollection<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext())
            {
                //getting all the Trainings between the dates
                var trainings = db.Trainings.Where(x => x.Start >= start && x.End <= end).Select(x => x).ToArray<Training>();
                Array.ForEach(trainings, t =>
                {
                    result.Add(Mapping.ToWeb<Training, WebTraining>(t));
                });
            }
            return Ok(result);
        }
        [Route("course/{code}")]
        [HttpGet]
        public IHttpActionResult OfCourse(string code)
        {
            //get all the Trainings that have the course id as  .. 
            ICollection<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext())
            {
                var trainings = db.Trainings.Where(x => x.Course.Code == code).Select(x => x).ToArray<Training>();
                Array.ForEach(trainings, t =>
                {
                    result.Add(Mapping.ToWeb<Training, WebTraining>(t));
                });
            }
            return Ok(result);
        }
        [Route("tutor/{id}")]
        [HttpGet]
        public IHttpActionResult ByTutor(int id)
        {
            ICollection<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext())
            {
                var trainings = db.Trainings.Where(x => x.Tutors.Where(t => t.Id == id).Count() != 0).Select(x => x).ToArray<Training>();
                Array.ForEach(trainings, t =>
                {
                    result.Add(Mapping.ToWeb<Training, WebTraining>(t));
                });
            }
            return Ok(result);
        }
        [Route("employee/{id}")]
        [HttpGet]
        public IHttpActionResult AttendedByEmployee(int id)
        {
            ICollection<WebTraining> result = new List<WebTraining>();
            using (academyContext db = new academyContext())
            {
                var trainings = db.Trainings.Where(x => x.Attendees.Where(a => a.Id == id).Count() != 0).Select(x => x).ToArray<Training>();
                Array.ForEach(trainings, t =>
                {
                    result.Add(Mapping.ToWeb<Training, WebTraining>(t));
                });
            }
            return Ok(result);
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(WebTraining t)
        {
            using (academyContext db = new academyContext())
            {
                db.Trainings.Add(Mapping.ToPoco<Training, WebTraining>(t));
                db.SaveChanges();
            }
            return Ok();
        }
        [Route("")]
        [HttpPut]
        public IHttpActionResult Update(WebTraining t)
        {
            using (academyContext db = new academyContext())
            {
                Training tt = Mapping.ToPoco<Training, WebTraining>(t);

                var toUpdate = db.Trainings.Where(x => x.Id == tt.Id).FirstOrDefault();
                toUpdate.Start = tt.Start;
                toUpdate.End = tt.End;
            }
            return Ok();
        }
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (academyContext db = new academyContext())
            {
                var toDelete = db.Trainings.Where(x => x.Id == id).FirstOrDefault();
                db.Trainings.Remove(toDelete);
                db.SaveChanges();
            }
            return Ok();
        }
        /// <summary>
        /// this method takes in the id of the traning and the employees to be added as attendess
        /// </summary>
        /// <param name="id">Id of the traning to be updated</param>
        /// <param name="attendees">list of employees to be added to the traning for the attendance</param>
        /// <returns>standard web response</returns>
        [Route("~/api/training/{id}/attendees")]
        [HttpPut]
        public IHttpActionResult UpdateAttendance(int id, Employee[] attendees)
        {
            using (academyContext db = new academyContext())
            {
                //training to be updated
                var toUpdate = db.Trainings.Where(x => x.Id == id).FirstOrDefault();
                //removing the attendance already recorded
                Array.ForEach(toUpdate.Attendees.ToArray<Employee>(), e => toUpdate.Attendees.Remove(e));
                //refilling the attendance
                Array.ForEach(attendees, a =>
                {
                    var emp =
                    db.Employees.Where(e => e.Id == a.Id).FirstOrDefault();
                    if (toUpdate.Attendees.Where(x => x.Id == emp.Id).Count() == 0)
                    {
                        toUpdate.Attendees.Add(emp);
                        db.SaveChanges();
                    }
                });
            }
            return Ok();
        }
        /// <summary>
        /// gets the attenadnce of the training given the id of the training
        /// </summary>
        /// <param name="id">id of the training for which the attendance is sought</param>
        /// <returns>std web response</returns>
        [Route("~/api/training/{id}/attendees")]
        [HttpGet]
        public IHttpActionResult Attendance(int id)
        {
            ICollection<WebEmployee> result = new List<WebEmployee>();
            using (academyContext db = new academyContext())
            {
                var attendance = db.Trainings.Where(x => x.Id == id).FirstOrDefault().Attendees.ToArray<Employee>();
                Array.ForEach(attendance, e =>
                {
                    result.Add(Mapping.ToWeb<Employee, WebEmployee>(e));//type conversion to serializable object
                });
            }
            return Ok(result);
        }
     


    }
}
