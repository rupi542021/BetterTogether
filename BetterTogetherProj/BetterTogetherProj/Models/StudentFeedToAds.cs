using BetterTogetherProj.Models.DAL;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class StudentFeedToAds
    {
        Student student;
        Ads ads;
        DateTime commentDate;
        string commentText;
        string managercomment;
        
  

        public StudentFeedToAds() { }

        public StudentFeedToAds(Student student, Ads ads, DateTime commentDate, string commentText, string managercomment)
        {
            Student = student;
            Ads = ads;
            CommentDate = commentDate;
            CommentText = commentText;
            Managercomment = managercomment;
        }

        public Student Student { get => student; set => student = value; }
        public Ads Ads { get => ads; set => ads = value; }
        public DateTime CommentDate { get => commentDate; set => commentDate = value; }
        public string CommentText { get => commentText; set => commentText = value; }
        public string Managercomment { get => managercomment; set => managercomment = value; }

        //public List<StudentFeedToAds> GetFBAds(string subnameFB)
        //{
        //    DBServices dbs = new DBServices();
        //    List<StudentFeedToAds> studentFBList = dbs.GetFBAds(subnameFB);
        //    return studentFBList;

        //}

        //public void Insertcomment()
        //{
        //    DBServices dbs = new DBServices();
        //    dbs.Insertcomment(this);
        //}
    }
}