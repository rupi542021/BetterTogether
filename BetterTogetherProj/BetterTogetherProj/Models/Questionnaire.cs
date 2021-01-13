using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Questionnaire
    {
        int qcode;
        DateTime openingdate;
        string question;
        List<Answers> ans;

        public Questionnaire(int qcode, DateTime openingdate, string question, List<Answers> ans)
        {
            Qcode = qcode;
            Openingdate = openingdate;
            Question = question;
            Ans = ans;
        }

        public int Qcode { get => qcode; set => qcode = value; }
        public DateTime Openingdate { get => openingdate; set => openingdate = value; }
        public string Question { get => question; set => question = value; }
        public List<Answers> Ans { get => ans; set => ans = value; }
    }
}