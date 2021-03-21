using BetterTogetherProj.Models.DAL;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class EventsFeedback
    {
        int fbEventNum;
        Student student;
        DateTime commentDate;
        string commentText;
        string managercomment;



        public EventsFeedback() { }

        public EventsFeedback(int fbEventNum, Student student, DateTime commentDate, string commentText, string managercomment)
        {
            FbEventNum = fbEventNum;
            Student = student;
            CommentDate = commentDate;
            CommentText = commentText;
            Managercomment = managercomment;
        }

        public int FbEventNum { get => fbEventNum; set => fbEventNum = value; }
        public Student Student { get => student; set => student = value; }
        public DateTime CommentDate { get => commentDate; set => commentDate = value; }
        public string CommentText { get => commentText; set => commentText = value; }
        public string Managercomment { get => managercomment; set => managercomment = value; }







        //public List<StudentFeedToEvents> GetFBEvents(string evtypeFB)
        //{
        //    DBServices dbs = new DBServices();
        //    List<StudentFeedToEvents> studentFBList = dbs.GetFBEvents(evtypeFB);
        //    return studentFBList;

        //}

        public void Insertcomment()
        {
            DBServices dbs = new DBServices();
            dbs.Insertcomment(this);
        }
    }
}