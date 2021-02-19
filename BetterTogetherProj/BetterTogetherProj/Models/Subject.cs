using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class Subject
    {
        int subCode;
        string subName;

        public Subject(int subCode, string subNAme)
        {
            SubCode = subCode;
            SubName = subName;
        }

        public int SubCode { get => subCode; set => subCode = value; }
        public string SubName { get => subName; set => subName = value; }

        public Subject() {}
        public List<Subject> Getsub()
        {
            DBServices dbs = new DBServices();
            List<Subject> subList = dbs.Getsub();
            return subList;

        }
    }
}