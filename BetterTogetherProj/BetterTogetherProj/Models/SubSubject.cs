using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class SubSubject
    {
        int subSubjectCode;
        string subSubjectName;
        Subject subject;

        public SubSubject(int subSubjectCode, string subSubjectName, Subject subject)
        {
            SubSubjectCode = subSubjectCode;
            SubSubjectName = subSubjectName;
            Subject = subject;
        }

        public int SubSubjectCode { get => subSubjectCode; set => subSubjectCode = value; }
        public string SubSubjectName { get => subSubjectName; set => subSubjectName = value; }
        public Subject Subject { get => subject; set => subject = value; }

        public SubSubject() { }
    }
}