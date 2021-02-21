using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BetterTogetherProj.Controllers
{
    public class studentsController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage GetStudentFromRuppin(string email)
        {
            Student student = new Student();
            try
            {
                student = student.checkStudentRuppin(email);
                return Request.CreateResponse(HttpStatusCode.OK,student);
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "email not found":
                        return Request.CreateResponse(HttpStatusCode.NotFound, e);
                    case "email already exists":
                        return Request.CreateResponse(HttpStatusCode.BadRequest, e);
                    default:
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
                }
                //if (e.Message == "email not found")
                //{
                //    return Request.CreateResponse(HttpStatusCode.NotFound, e);
                //}
                //return Request.CreateResponse(HttpStatusCode.BadRequest,e);
            }
        }
        [HttpGet]
        [Route("API/students/{email}/{password}")]
        public IHttpActionResult GetStudentLogin(string email,string password)
        {
            Student student1 = new Student();
            try
            {
                student1 = student1.checkStudentLogin(email, password);
                return Ok(student1);
            }
            catch (Exception e)
            {
                //return badrequest(e.message);
                return Content(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpGet]
        [Route("api/students/GetAllPleasures")]
        public IHttpActionResult GetAllPleasures()
        {
            try
            {
                Pleasure p = new Pleasure();
                List<Pleasure> PList = p.Read();
                return Ok(PList);
            }
            catch (Exception e)
            {
                //return badrequest(e.message);
                return Content(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpGet]
        [Route("api/students/GetAllHoddies")]
        public IHttpActionResult GetAllHoddies()
        {
            try
            {
                Hobby h = new Hobby();
                List<Hobby> HList = h.Read();
                return Ok(HList);
            }
            catch (Exception e)
            {
                //return badrequest(e.message);
                return Content(HttpStatusCode.BadRequest, e);
            }
        }



        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Student student)
        {
           // Student s = new Student();
            try
            {
                student.Insert();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}