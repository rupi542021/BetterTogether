using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class Subject
    {
        int subCode;
        string subNAme;

        public Subject(int subCode, string subNAme)
        {
            SubCode = subCode;
            SubNAme = subNAme;
        }

        public int SubCode { get => subCode; set => subCode = value; }
        public string SubNAme { get => subNAme; set => subNAme = value; }

        public Subject() {}
    }
}