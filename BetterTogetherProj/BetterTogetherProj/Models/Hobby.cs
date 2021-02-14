using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Hobby
    {
        int hcode;
        string hname;
        string hicon;
        List<Student> studlist;
   

        public Hobby(){}

        public Hobby(int hcode, string hname, string hicon,List<Student> studlist)
        {
            Hcode = hcode;
            Hname = hname;
            Hicon = hicon;
            Studlist = studlist;
        }

        public int Hcode { get => hcode; set => hcode = value; }
        public string Hname { get => hname; set => hname = value; }
        public string Hicon { get => hicon; set => hicon = value; }
        public List<Student> Studlist { get => studlist; set => studlist = value; }

        public List<Hobby> Read()
        {
            DBServicesReact db = new DBServicesReact();
            List<Hobby> HList = db.GetAllHoddies();
            return HList;
        }
    }
}