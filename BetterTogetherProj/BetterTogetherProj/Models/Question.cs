﻿using BetterTogetherProj.Models.DAL;
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
        List<string> anslist;

        public Question() { }

        public Question(int questionnum, string questionText, string questionType, Questionnaire qr, List<string> anslist)
        {
            Questionnum = questionnum;
            QuestionText = questionText;
            QuestionType = questionType;
            Qr = qr;
            Anslist = anslist;
        }

        public int Questionnum { get => questionnum; set => questionnum = value; }
        public string QuestionText { get => questionText; set => questionText = value; }
        public string QuestionType { get => questionType; set => questionType = value; }
        public Questionnaire Qr { get => qr; set => qr = value; }
        public List<string> Anslist { get => anslist; set => anslist = value; }


        public List<Question> GetAllQuestions(int Qcode)
        {
            DBServices dbs = new DBServices();
            List<Question> qList = dbs.getQuestionsbyNumqr(Qcode);
            return qList;
        }

        public Question getAnsList()
        {
            DBServices dbs = new DBServices();
            Question SumStuAns = dbs.getAnsList();
            return SumStuAns;

        }
    }
}