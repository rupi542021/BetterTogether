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
        List<Student> studlist;
   

        public Hobby(){}

        public Hobby(int hcode, string hname, List<Student> studlist)
        {
            Hcode = hcode;
            Hname = hname;
            Studlist = studlist;
        }

        public int Hcode { get => hcode; set => hcode = value; }
        public string Hname { get => hname; set => hname = value; }
        public List<Student> Studlist { get => studlist; set => studlist = value; }
    }
}