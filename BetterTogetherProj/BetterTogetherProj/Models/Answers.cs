using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Answers
    {
        Questionnaire quest;
       int answerno;
       string  answertext;
        List<Student> studList;

        public Answers(Questionnaire quest, int answerno, string answertext, List<Student> studList)
        {
            Quest = quest;
            Answerno = answerno;
            Answertext = answertext;
            StudList = studList;
        }

        public Questionnaire Quest { get => quest; set => quest = value; }
        public int Answerno { get => answerno; set => answerno = value; }
        public string Answertext { get => answertext; set => answertext = value; }
        public List<Student> StudList { get => studList; set => studList = value; }
    }
}