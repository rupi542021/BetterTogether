using BetterTogetherProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BetterTogetherProj.Controllers
{
    public class EventsController : ApiController
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
        public void Put( [FromBody] Events eventt)
        {
            eventt.UpdateEvent();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }


        [HttpGet]
        [Route("api/Events/getevent")]
        public List<EventType> Getevent()
        {
            EventType evtype = new EventType();
            return evtype.Getevtype();

        }

        [HttpGet]
        [Route("api/Events/getsametype")]
        public List<string> Getsametype(string evtypename)
        {
            EventType evname = new EventType();
            return evname.Getsametype(evtypename);

        }

        [HttpPost]
        [Route("api/Events/addevent")]
        public HttpResponseMessage InsertEvent([FromBody] Events ev)
        {
            try
            {
                ev.InsertEvent();

                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Events/addeventtype")]
        public HttpResponseMessage InsertEventtype([FromBody] EventType evtype)
        {
            try
            {
                 evtype.InsertEventtype();

                 return  Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
            }
            catch (Exception ex)
            {
                 return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            
        }

        [HttpPost]
        [Route("api/Events/addeventname")]
        public HttpResponseMessage InsertEventname([FromBody] EventType evname)
        {
            try
            {
                evname.InsertEventname();

                return Request.CreateResponse(HttpStatusCode.OK, "Added successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpGet]
        [Route("api/Events/getFB")]
        public List<Events> GetEventByevtype(string evtypeFB)
        {
            Events FB = new Events();
            return FB.GetEventByevtype(evtypeFB);

        }

        [HttpPut]
        [Route("api/Events/addcomment")]
        public HttpResponseMessage Insertcomment([FromBody] EventsFeedback addmngcom)
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

        [HttpGet]
        [Route("api/Events/geteventdetail")]
        public List<Events> Geteventdetail()
        {
            Events evdetail = new Events();
            return evdetail.Geteventdetail();

        }
    }
}