using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Course
    {
        int courseCode;
        string courseName;
        List<StudentInCourse> sInCList;

  
            public Course() { }

        public Course(int courseCode, string courseName, List<StudentInCourse> sInCList)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            SInCList = sInCList;
        }

        public int CourseCode { get => courseCode; set => courseCode = value; }
        public string CourseName { get => courseName; set => courseName = value; }
        public List<StudentInCourse> SInCList { get => sInCList; set => sInCList = value; }
    }
}