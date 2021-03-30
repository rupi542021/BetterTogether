using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BetterTogetherProj.Controllers
{
    public class QuestionnaireController : ApiController
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
        //public HttpResponseMessage Post([FromBody] Questionnaire qr)
        //{

        //    try
        //    {

        //        qr.Insertqr();

        //        return Request.CreateResponse(HttpStatusCode.OK, "Success");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
        //    }
        //}

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("api/Questionnaire/qr")]
        public List<Questionnaire> GetQuestionnaire()
        {
            Questionnaire cmp = new Questionnaire();
            return cmp.GetQuestionnaire();

        }

        [HttpPost]
        [Route("api/Questionnaire/postqr")]
        public HttpResponseMessage PostQuestionnaire([FromBody] Questionnaire qr)
        {
            try
            {
                qr.InsertQuestionnaire();

                return Request.CreateResponse(HttpStatusCode.OK, "Added to questionnaire table successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }


        }

        [HttpGet]
        [Route("api/Questionnaire/dep")]
        public List<Department> GetDepartment()
        {
            Department dep = new Department();
            return dep.GetDepartment();

        }

        //[HttpPost]
        //[Route("api/Questionnaire/postquestion")]
        //public HttpResponseMessage PostQuestion([FromBody] Question q)
        //{
        //    try
        //    {
        //        q.Insertquestion();

        //        return Request.CreateResponse(HttpStatusCode.OK, "Added to favorite page successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
        //    }


        //}
    }
}