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
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        //[HttpGet]
        //[Route("api/Ads/ad")]
        //public List<Ads> Getads()
        //{
        //    Ads ad = new Ads();
        //    return ad.Getads();

        //}
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

                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
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

                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
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

                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpGet]
        [Route("api/Ads/getFB")]
        public List<Ads> GetAdBySub(string subnameFB)
        {
            Ads ad = new Ads();
            return ad.GetAdBySub(subnameFB);

        }

        [HttpPut]
        [Route("api/Ads/addcomment")]
        public HttpResponseMessage Insertcomment([FromBody] AdsFeedback addmngcom)
        {
            try
            {
                addmngcom.Insertcomment();

                return Request.CreateResponse(HttpStatusCode.OK, "Comment added successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }
}