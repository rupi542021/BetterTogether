using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Answers
    {
        Question quest;
        int answerno;
        string  answertext;
        List <Student> studList;
        string ansType;

        public Answers(Question quest, int answerno, string answertext, List<Student> studList, string ansType)
        {
            Quest = quest;
            Answerno = answerno;
            Answertext = answertext;
            StudList = studList;
            AnsType = ansType;
        }

        public Question Quest { get => quest; set => quest = value; }
        public int Answerno { get => answerno; set => answerno = value; }
        public string Answertext { get => answertext; set => answertext = value; }
        public List<Student> StudList { get => studList; set => studList = value; }
        public string AnsType { get => ansType; set => ansType = value; }
        public Answers() { }
    }
}