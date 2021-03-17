using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace project_classes.Models
{
    public class StudentInCourse
    {
        Course c;
        Student stud;
        int semester;

        public Course C { get => c; set => c = value; }
        public Student Stud { get => stud; set => stud = value; }
        public int Semester { get => semester; set => semester = value; }

      
     
        public StudentInCourse() { }

        public StudentInCourse(Course c, Student stud, int semester)
        {
            C = c;
            Stud = stud;
            Semester = semester;
        }
    }
}