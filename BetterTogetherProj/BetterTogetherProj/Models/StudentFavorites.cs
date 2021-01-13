using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class StudentFavorites
    {
        string student1mail;
        string student2mail;

        public StudentFavorites(string student1mail, string student2mail)
        {
            Student1mail = student1mail;
            Student2mail = student2mail;
        }

        public string Student1mail { get => student1mail; set => student1mail = value; }
        public string Student2mail { get => student2mail; set => student2mail = value; }
    }
}