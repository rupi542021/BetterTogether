using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Pleasure
    {
        int pcode;
        string pname;
        List<Student> studlist;

        public Pleasure(){}

        public Pleasure(int pcode, string pname, List<Student> studlist)
        {
            Pcode = pcode;
            Pname = pname;
            Studlist = studlist;
        }

        public int Pcode { get => pcode; set => pcode = value; }
        public string Pname { get => pname; set => pname = value; }
        public List<Student> Studlist { get => studlist; set => studlist = value; }
    }
}