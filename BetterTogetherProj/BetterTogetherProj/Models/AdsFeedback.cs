using BetterTogetherProj.Models.DAL;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class AdsFeedback
    {
        Student student;
        DateTime commentDate;
        string commentText;
        string managercomment;
        
  

        public AdsFeedback() { }

        public AdsFeedback(Student student, DateTime commentDate, string commentText, string managercomment)
        {
            Student = student;
            CommentDate = commentDate;
            CommentText = commentText;
            Managercomment = managercomment;
        }

        public Student Student { get => student; set => student = value; }
        public DateTime CommentDate { get => commentDate; set => commentDate = value; }
        public string CommentText { get => commentText; set => commentText = value; }
        public string Managercomment { get => managercomment; set => managercomment = value; }





        //public void Insertcomment()
        //{
        //    DBServices dbs = new DBServices();
        //    dbs.Insertcomment(this);
        //}
    }
}