﻿using BetterTogetherProj.Models;
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
                return Request.CreateResponse(HttpStatusCode.OK, student);
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
        public HttpResponseMessage GetStudentLogin(string email, string password)
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
                return Request.CreateResponse(HttpStatusCode.OK, PList);
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
                return Request.CreateResponse(HttpStatusCode.OK, student.getCurrentStudent(student.Mail));
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


        [HttpPost]
        [Route("api/students/addStudent")]
        
        public HttpResponseMessage AddToSList([FromBody] Student s)
        {
            try
            {
                s.InsertSToList();
                return Request.CreateResponse(HttpStatusCode.OK, "הסטודנט נכנס בהצלחה");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPut]
        [Route("API/students/{mail}/updateToken")]
        public IHttpActionResult updateToken(string mail, [FromBody] string token)
        {
            try
            {
                Student s = new Student();
                s.updateToken(mail, token);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
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

        [HttpPut]
        [Route("API/students/{mail}/updateUserPreferences")]
        public IHttpActionResult updateUserPreferences(string mail, [FromBody] List<Preferences> prefList)
        {
            //string mail = s.Mail;
            //List < Preferences > prefList = s.Preflist;
            try
            {
                Student s = new Student();
                s.updateUserPreferences(mail, prefList);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPut]
        [Route("API/students/{pass}/updateUserPassword")]
        public IHttpActionResult updateUserPassword(string pass, [FromBody] Student stud)
        {
            try
            {
                Student s = new Student();
                s.updateUserPassword(pass, stud);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        [Route("API/students/updateUserPrefRanges")]
        public IHttpActionResult updateUserPrefRanges([FromBody] Student stud)
        {
            try
            {
                stud.updateUserPrefRanges();
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
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
            catch (Exception e)
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
        [HttpDelete]
        [Route("api/students/{mail}/deleteUserProfile")]
        public IHttpActionResult deleteUserProfile(string mail)
        {
            try
            {
                Student stud = new Student();
                stud.deleteUserProfile(mail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/students/PostLocation")]
        [HttpPost]
        public HttpResponseMessage PostLocation([FromBody] StudLocation sl)
        {
            try
            {
                sl.Insert();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        [HttpGet]
        [Route("API/students/{mail}/GetCloseStudents")]
        public IHttpActionResult GetCloseStudents(string mail)
        {
            try
            {
                Student s = new Student();
                List<Student> studentsList = s.GetCloseStudents(mail);
                return Ok(studentsList);
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "there are no close users":
                        return Content(HttpStatusCode.NotFound, e);
                    default:
                        return Content(HttpStatusCode.BadRequest, e);
                }
            }
        }
        //[HttpGet]
        //[Route("API/students/GetCloseStudents")]
        //public IHttpActionResult GetCloseStudents()
        //{
        //    try
        //    {
        //        StudentFavorites s = new StudentFavorites();
        //        List<StudentFavorites> studentsList = s.GetCloseStudents();
        //        return Ok(studentsList);
        //    }
        //    catch (Exception e)
        //    {
        //        switch (e.Message)
        //        {
        //            case "there are no close users":
        //                return Content(HttpStatusCode.NotFound, e);
        //            default:
        //                return Content(HttpStatusCode.BadRequest, e);
        //        }
        //    }
        //}
            
        

        //[HttpGet]
        //[Route("api/students/getStudentbyDepAndYear")]
        //public List<Student> getStudentbyDepAndYear()
        //{
        //    Student qr = new Student();
        //    return qr.getStudentbyDepAndYear();

        //}

        [HttpGet]
        [Route("api/students/getpercentregistered")]
        public List<double> getpercentregistered()
        {
            Student stu = new Student();
            return stu.getpercentregistered();

        }

        [HttpGet]
        [Route("api/students/getpercentregiByDep")]
        public IEnumerable<List<int>> getpercentregiByDep()
        {
            Student stu = new Student();
            return stu.getpercentregiByDep();

        }

        [HttpGet]
        [Route("api/students/getpercentregiByYear")]
        public IEnumerable<List<int>> getpercentregiByYear()
        {
            Student stu = new Student();
            return stu.getpercentregiByYear();

        }

        [HttpGet]
        [Route("api/students/getNumOfNewUsers")]
        public List<int> getNumOfNewUsers(int numDays)
        {
            Student stu = new Student();
            return stu.getNumOfNewUsers(numDays);

        }

        [HttpGet]
        [Route("api/students/getstudentsAns")]
        public List<StudentsAnswers> getstudentsAns()
        {
            StudentsAnswers s = new StudentsAnswers();
            return s.getstudentsAns();

        }
        [HttpGet]
        [Route("api/students/{dep}/{year}/links")]
        public HttpResponseMessage ReadLinks(int dep,int year)
        {
            Links l = new Links();
            try
            {
                l = l.ReadLinks(dep, year);
                return Request.CreateResponse(HttpStatusCode.OK, l);
            }
           
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}