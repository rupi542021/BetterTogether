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
        int fbAdsNum;
        Student student;
        DateTime commentDate;
        string commentText;
        string managercomment;
        
  

        public AdsFeedback() { }

        public AdsFeedback(int fbAdsNum, Student student, DateTime commentDate, string commentText, string managercomment)
        {
            FbAdsNum = fbAdsNum;
            Student = student;
            CommentDate = commentDate;
            CommentText = commentText;
            Managercomment = managercomment;
        }

        public int FbAdsNum { get => fbAdsNum; set => fbAdsNum = value; }
        public Student Student { get => student; set => student = value; }
        public DateTime CommentDate { get => commentDate; set => commentDate = value; }
        public string CommentText { get => commentText; set => commentText = value; }
        public string Managercomment { get => managercomment; set => managercomment = value; }



        public void Insertcomment()
        {
            DBServices dbs = new DBServices();
            dbs.Insertcomment(this);
        }
    }
}