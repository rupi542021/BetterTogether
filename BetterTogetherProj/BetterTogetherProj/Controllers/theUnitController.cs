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
    public class theUnitController : ApiController
    {
        [HttpGet]
        [Route("api/theUnit/getAllEvents")]
        public List<Events> Geteventdetail()
        {
            Events e = new Events();
            return e.GetAllEvents();

        }

        [Route("api/theUnit/AddToArrivals")]
        [HttpPost]
        public HttpResponseMessage AddToArrivals([FromBody] StudentInEvent se)
        {
            try
            {
                se.InsertEventArrival();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        [HttpDelete]
        [Route("api/theUnit/DeleteArrival")]
        public IHttpActionResult DeleteArrival([FromBody] StudentInEvent se)
        {
            try
            {
                se.Delete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("api/theUnit/getAllAds")]
        public List<Ads> GetAllAds()
        {
            Ads a = new Ads();
            return a.GetAllAds();

        }
        [HttpGet]
        [Route("api/theUnit/getAllQuestionnaires/{DepCode}/{Year}")]
        public List<Questionnaire> GetAllQuestionnaire(int DepCode,int Year)
        {
            Questionnaire q = new Questionnaire();
            return q.GetAllQuestionnaire(DepCode,Year);

        }
        [HttpPost]
        [Route("api/theUnit/eventComment")]
        public HttpResponseMessage EventComment([FromBody] EventsFeedback ec)
        {
            try
            {
                ec.InsertEventComment();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/theUnit/adComment")]
        public HttpResponseMessage AdComment([FromBody] AdsFeedback ac)
        {
            try
            {
                ac.InsertAdComment();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/theUnit/getAllQuestions/{Qcode}")]
        public List<Question> getAllQuestions(int Qcode)
        {
            Question q = new Question();
            return q.GetAllQuestions(Qcode);

        }
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}