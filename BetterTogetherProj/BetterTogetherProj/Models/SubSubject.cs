//using BetterTogetherProj.Models.DAL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace BetterTogetherProj.Models
//{
//    public class SubSubject
//    {
//        string subSubjectName;
//        Subject subject;

//        public SubSubject(string subSubjectName, Subject subject)
//        {
//            SubSubjectName = subSubjectName;
//            Subject = subject;
//        }

//        public string SubSubjectName { get => subSubjectName; set => subSubjectName = value; }
//        public Subject Subject { get => subject; set => subject = value; }

//        public SubSubject() { }

//        public List<SubSubject> Getsamename(string subname)
//        {
//            DBServices dbs = new DBServices();
//            List<SubSubject> subsubList = dbs.Getsamename(subname);
//            return subsubList;

//        }

//        public void InsertSubsub()
//        {
//            DBServices dbs = new DBServices();
//            dbs.InsertSubsub(this);
//        }
//    }
//}