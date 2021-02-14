using BetterTogetherProj.Models.DAL;
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
        string picon;
        List<Student> studlist;

        public Pleasure(){}

        public Pleasure(int pcode, string pname, string picon, List<Student> studlist)
        {
            Pcode = pcode;
            Pname = pname;
            Picon = picon;
            Studlist = studlist;
        }

        public int Pcode { get => pcode; set => pcode = value; }
        public string Pname { get => pname; set => pname = value; }
        public string Picon { get => picon; set => picon = value; }
        public List<Student> Studlist { get => studlist; set => studlist = value; }

        public List<Pleasure> Read()
        {
            DBServicesReact db = new DBServicesReact();
            List<Pleasure> PList = db.GetAllPleasures();
            return PList;
        }
    }
}