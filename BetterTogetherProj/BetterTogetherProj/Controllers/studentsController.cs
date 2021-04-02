using BetterTogetherProj.Models;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace BetterTogetherProj.Controllers
{
    public class studentsController : ApiController
    {
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
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }
        [HttpGet]
        [Route("api/students/GetAllPleasures")]
        public HttpResponseMessage GetAllPleasures()
        {
            try
            {
                Pleasure p = new Pleasure();
                List<Pleasure> PList = p.Read();
                return Request.CreateResponse(HttpStatusCode.OK,PList);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpGet]
        [Route("api/students/GetAllHoddies")]
        public HttpResponseMessage GetAllHoddies()
        {
            try
            {
                Hobby h = new Hobby();
                List<Hobby> HList = h.Read();
                return Request.CreateResponse(HttpStatusCode.OK, HList);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpGet]
        [Route("API/students/{mail}/Recommend")]
        public IHttpActionResult GetStudentsWithRecommend(string mail)
        {
            try
            {
                Student s = new Student();
                List<Student> studentsList = s.GetStudentsWithRecommend(mail);
                return Ok(studentsList);
            }
            catch (Exception e)
            {
                //return badrequest(e.message);
                return Content(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpGet]
        [Route("API/students/{mail}/GetAllFavorites")]
        public IHttpActionResult GetAllFavorites(string mail)
        {
            try
            {
                Student s = new Student();
                List<Student> studentsList = s.GetAllFavorites(mail);
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
                throw (ex);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
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

        [HttpPost]
        [Route("api/students/uploadedFiles")]
        public HttpResponseMessage Post()
        {
            string imageLink;
            var httpContext = HttpContext.Current;
            string imgpath = "";
            try
            {
                if (httpContext.Request.Files.Count > 0)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[0];
                    string name = httpContext.Request.Form["name"];


                    DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(HostingEnvironment.MapPath("~/uploadedFiles"));
                    FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + name + "*.*");
                    foreach (FileInfo foundFile in filesInDir)
                    {
                        foundFile.Delete();
                    }

                    if (httpPostedFile != null)
                    {
                        string fname = httpPostedFile.FileName.Split('\\').Last();
                        string sfname = fname.Split('.').Last();
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), name + "." + sfname);
                        httpPostedFile.SaveAs(fileSavePath);
                        imgpath = fileSavePath;
                        imageLink = $"uploadedFiles/{name}.{sfname}";
                    }
                }
                return Request.CreateResponse(HttpStatusCode.Created, imgpath);
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        [Route("api/students/DeleteFromFavorites")]
        public IHttpActionResult DeleteFromFavorites([FromBody] StudentFavorites sf)
        {
            try
            {
                sf.Delete();
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}