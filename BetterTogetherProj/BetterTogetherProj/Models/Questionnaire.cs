using BetterTogetherProj.Models.DAL;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Questionnaire
    {
        int questionnaireNum;
        DateTime questionnairePublish;
        DateTime endPublishDate;
        string subQr;
        Department dep;
        int numResponders;
        bool status;
        int questionnaireYear;
        List<Question> queslist;



        public Questionnaire() { }

        public Questionnaire(int questionnaireNum, DateTime questionnairePublish, DateTime endPublishDate, string subQr, Department dep, int numResponders, bool status, int questionnaireYear, List<Question> queslist)
        {
            QuestionnaireNum = questionnaireNum;
            QuestionnairePublish = questionnairePublish;
            EndPublishDate = endPublishDate;
            SubQr = subQr;
            Dep = dep;
            NumResponders = numResponders;
            Status = status;
            QuestionnaireYear = questionnaireYear;
            Queslist = queslist;
        }

        public int QuestionnaireNum { get => questionnaireNum; set => questionnaireNum = value; }
        public DateTime QuestionnairePublish { get => questionnairePublish; set => questionnairePublish = value; }
        public DateTime EndPublishDate { get => endPublishDate; set => endPublishDate = value; }
        public string SubQr { get => subQr; set => subQr = value; }
        public Department Dep { get => dep; set => dep = value; }
        public int NumResponders { get => numResponders; set => numResponders = value; }
        public bool Status { get => status; set => status = value; }
        public int QuestionnaireYear { get => questionnaireYear; set => questionnaireYear = value; }
        public List<Question> Queslist { get => queslist; set => queslist = value; }

        public List<Questionnaire> GetQuestionnaire()
        {
            DBServices dbs = new DBServices();
            List<Questionnaire> qrList = dbs.GetQuestionnaire();
            return qrList;

        }
        public void InsertQuestionnaire()
        {
            DBServices dbs = new DBServices();
            dbs.InsertQuestionnaire(this);

        }
    }
}