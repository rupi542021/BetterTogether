using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class Subject
    {
        
        string subName;
        List<string> subSubject;


        public Subject() {}

        public Subject(string subName, List<string> subSubject)
        {
            SubName = subName;
            SubSubject = subSubject;
        }

        public string SubName { get => subName; set => subName = value; }
        public List<string> SubSubject { get => subSubject; set => subSubject = value; }

        public List<Subject> Getsub()
        {
            DBServices dbs = new DBServices();
            List<Subject> subList = dbs.Getsub();
            return subList;

        }

        public void InsertSubject()
        {
            DBServices dbs = new DBServices();
            dbs.InsertSubject(this);
        }

        public List<string> Getsamename(string subname)
        {
            DBServices dbs = new DBServices();
            List<string> subsubList = dbs.Getsamename(subname);
            return subsubList;

        }

        public void InsertSubsub()
        {
            DBServices dbs = new DBServices();
            dbs.InsertSubsub(this);
        }
    }
}