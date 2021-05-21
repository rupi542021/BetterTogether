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


        // PUT api/<controller>/5
        public void Put( [FromBody] Questionnaire Qr)
        {
            Qr.UpdateQr();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("api/Questionnaire/qr")]
        public List<Questionnaire> GetQuestionnaire(int statusQR)
        {
            Questionnaire qr = new Questionnaire();
            return qr.GetQuestionnaire(statusQR);

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
        [HttpGet]
        [Route("api/Questionnaire/analyze")]
        public List<StudentsAnswers> GetStudentAns(int numQr, int depcode)
        {
            StudentsAnswers SAns = new StudentsAnswers();
            return SAns.GetStudentAns(numQr, depcode);

        }


        [HttpGet]
        [Route("api/Questionnaire/GetQNum")]
        public int GetQuestionnaireNum()
        {
            Questionnaire qr = new Questionnaire();
            return qr.GetQuestionnaireNum();

        }

        [HttpPut]
        [Route("api/Questionnaire/updatestatus")]
        public void UpdateStatusQr( int QrId)
        {
            Questionnaire qr = new Questionnaire();
            qr.UpdateStatusQr(QrId);
        }
    }
}