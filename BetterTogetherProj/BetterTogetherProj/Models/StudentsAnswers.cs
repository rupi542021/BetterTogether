using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class StudentsAnswers
    {
        string mail; 
        List<bool> studAns;  
        int questionnaireNum;
        int questionnum;
        string openAnswer;


        public StudentsAnswers() { }

        public StudentsAnswers(string mail, List<bool> studAns, int questionnaireNum, int questionnum, string openAnswer)
        {
            Mail = mail;
            StudAns = studAns;
            QuestionnaireNum = questionnaireNum;
            Questionnum = questionnum;
            OpenAnswer = openAnswer;
        }

        public string Mail { get => mail; set => mail = value; }
        public List<bool> StudAns { get => studAns; set => studAns = value; }
        public int QuestionnaireNum { get => questionnaireNum; set => questionnaireNum = value; }
        public int Questionnum { get => questionnum; set => questionnum = value; }
        public string OpenAnswer { get => openAnswer; set => openAnswer = value; }

        public List<StudentsAnswers> GetStudentAns(int numQr,int depcode, int depyear)
        {
            DBServices db = new DBServices();
            List<StudentsAnswers> SAns = db.GetStudentAns(numQr, depcode, depyear);
            return SAns;
        }
        public void InsertQuestionnairAns()
        {
            DBServicesUnit dbs = new DBServicesUnit();
            dbs.InsertQuestionnairAns(this);
        }
    }

}