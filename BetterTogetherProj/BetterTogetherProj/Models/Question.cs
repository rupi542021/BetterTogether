using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Question
    {
        int questionnum;
        string questionText;
        string questionType;
        Questionnaire qr;


        public Question() { }

        public Question(int questionnum, string questionText, string questionType, Questionnaire qr)
        {
            Questionnum = questionnum;
            QuestionText = questionText;
            QuestionType = questionType;
            Qr = qr;
        }

        public int Questionnum { get => questionnum; set => questionnum = value; }
        public string QuestionText { get => questionText; set => questionText = value; }
        public string QuestionType { get => questionType; set => questionType = value; }
        public Questionnaire Qr { get => qr; set => qr = value; }

        //public void Insertquestion()
        //{
        //    DBServices dbs = new DBServices();
        //    dbs.Insertquestion(this);

        //}
    }
}