using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Question
    {
        int qcode;
        string question1;
        List<Answers> ans;
        Questionnaire qr;

        public Question(int qcode, string question1, List<Answers> ans)
        {
            Qcode = qcode;
            Question1 = question1;
            Ans = ans;
        }

        public int Qcode { get => qcode; set => qcode = value; }
        public string Question1 { get => question1; set => question1 = value; }
        public List<Answers> Ans { get => ans; set => ans = value; }
        public Question() { }
    }
}