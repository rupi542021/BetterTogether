using BetterTogetherProj.Models.DAL;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class AddStudent
    {
        
       
        string fname;
        string lname;
        string mail;
        DateTime dateOfBirth;
        Department dep;
        int studyingYear;

        public AddStudent( string fname, string lname,string mail, DateTime dateOfBirth, Department dep, int studyingYear)
        {
           
          
            Fname = fname;
            Lname = lname;
            Mail = mail;
            DateOfBirth = dateOfBirth;
            Dep = dep;
            StudyingYear = studyingYear;
        }

       
       
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string Mail { get => mail; set => mail = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public Department Dep { get => dep; set => dep = value; }
        public int StudyingYear { get => studyingYear; set => studyingYear = value; }
        public AddStudent() { }

        public void InsertSToList()
        {
            DBServices dbs = new DBServices();
            dbs.InsertSToList(this);

        }
    }
   
}