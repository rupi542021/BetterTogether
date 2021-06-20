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
    public class AdsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Ads adss)
        {
            try
            {
                adss.UpdateAd();
                return Request.CreateResponse(HttpStatusCode.OK, "!הפרסום עודכן בהצלחה");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("api/Ads/getsub")]
        public List<Subject> Getsub()
        {
            Subject sub = new Subject();
            return sub.Getsub();

        }

        [HttpGet]
        [Route("api/Ads/getsamename")]
        public List<string> Getsamename(string subname)
        {

            Subject sub = new Subject();
            return sub.Getsamename(subname);

        }

        [HttpPost]
        [Route("api/Ads/addad")]
        public HttpResponseMessage InsertToTable([FromBody] Ads ad)
        {
            try
            {
                ad.InsertToTable();

                return Request.CreateResponse(HttpStatusCode.OK, "הפרסום נוסף בהצלחה");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Ads/addsub")]
        public HttpResponseMessage InsertSubject([FromBody] Subject sub)
        {
            try
            {
                sub.InsertSubject();

                return Request.CreateResponse(HttpStatusCode.OK, "הנושא נוסף בהצלחה");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpPost]
        [Route("api/Ads/addsubsub")]
        public HttpResponseMessage InsertSubsub([FromBody] Subject subsub)
        {
            try
            {
                subsub.InsertSubsub();

                return Request.CreateResponse(HttpStatusCode.OK, "תת הנושא נוסף בהצלחה");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpGet]
        [Route("api/Ads/getFB")]
        public List<Ads> GetAdBySub(int statusAd, string subnameFB)
        {
            Ads ad = new Ads();
            return ad.GetAdBySub(statusAd, subnameFB);

        }

        [HttpPut]
        [Route("api/Ads/addcomment")]
        public HttpResponseMessage Insertcomment([FromBody] AdsFeedback addmngcom)
        {
            try
            {
                addmngcom.Insertcomment();

                return Request.CreateResponse(HttpStatusCode.OK, "התגובה עודכנה בהצלחה");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpPut]
        [Route("api/Ads/updatestatus")]
        public HttpResponseMessage Put(int AdId)
        {
            try
            {
                Ads ad = new Ads();
                ad.UpdateStatusAd(AdId);
                return Request.CreateResponse(HttpStatusCode.OK, "הסטטוס עודכן בהצלחה");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Ads/getStudentsByActivity")]
        public IEnumerable<List<Student>> GetStudentsByActivity()
        {
            Ads stu = new Ads();
            return stu.GetStudentsByActivity();

        }



        [HttpPut]
        [Route("api/Ads/blockS")]
        public HttpResponseMessage Put(string StudentMail, bool active)
        {
            try
            {
                Ads stud = new Ads();
                stud.UpdateActivity(StudentMail, active);
                if(active==true)
                return Request.CreateResponse(HttpStatusCode.OK, "המשתמש נחסם בהצלחה");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "המשתמש שוחרר בהצלחה");


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}