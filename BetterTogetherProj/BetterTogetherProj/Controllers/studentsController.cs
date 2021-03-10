using BetterTogetherProj.Models;
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
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, e);
                    case "email already exists":
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
                //if (e.Message == "email not found")
                //{
                //    return Request.CreateResponse(HttpStatusCode.NotFound, e);
                //}
                //return Request.CreateResponse(HttpStatusCode.BadRequest,e);
            }
        }
        [HttpGet]
        [Route("api/students/{email}/{password}")]
        public HttpResponseMessage GetStudentLogin(string email,string password)
        {
            Student student = new Student();
            try
            {
                student = student.checkStudentLogin(email, password);
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "email not found":
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, e);
                    case "incorrect password":
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
                    default:
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
                }
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

        [HttpGet]
        [Route("API/students/{mail}/without")]
        public IHttpActionResult GetAllUsers(string mail)
        {
            try
            {
                Student s = new Student();
                List<Student> studentsList = s.ReadAllStudent(mail);
                return Ok(studentsList);
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

        [Route("api/students/AddToFavorites")]
        [HttpPost]
        public HttpResponseMessage AddToFavorites([FromBody] StudentFavorites sf)
        {
            try
            {
                sf.Insert();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/students/updateStudentPtofile")]
        public IHttpActionResult updateStudentPtofile([FromBody] Student student)
        {
            try
            {
                student.UpdateStudentPtofile();
                    return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("api/students/GetAllResidences")]
        public IHttpActionResult GetAllResidences()
        {
            try
            {
                Residence r = new Residence();
                List<Residence> rList = r.Read();
                return Ok(rList);
            }
            catch (Exception e)
            {
                //return badrequest(e.message);
                return Content(HttpStatusCode.BadRequest, e);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}