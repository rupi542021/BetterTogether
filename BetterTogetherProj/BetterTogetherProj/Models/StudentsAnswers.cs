using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class StudentsAnswers
    {
        string mail; 
        List<string> studAns;
        DateTime answerdate;
        int questionnaireNum;
        int questionnum;



        public StudentsAnswers() { }

        public StudentsAnswers(string mail, List<string> studAns, DateTime answerdate, int questionnaireNum, int questionnum)
        {
            Mail = mail;
            StudAns = studAns;
            Answerdate = answerdate;
            QuestionnaireNum = questionnaireNum;
            Questionnum = questionnum;
        }

        public string Mail { get => mail; set => mail = value; }
        public List<string> StudAns { get => studAns; set => studAns = value; }
        public DateTime Answerdate { get => answerdate; set => answerdate = value; }
        public int QuestionnaireNum { get => questionnaireNum; set => questionnaireNum = value; }
        public int Questionnum { get => questionnum; set => questionnum = value; }
    }
}