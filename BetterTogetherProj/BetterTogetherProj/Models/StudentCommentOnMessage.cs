using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class StudentCommentOnMessage
    {
        Student stud;
        Message msg;
        string comment;
        DateTime ctimedate;

      

        public StudentCommentOnMessage()
        {}

        public StudentCommentOnMessage(Student stud, Message msg, string comment, DateTime ctimedate)
        {
            Stud = stud;
            Msg = msg;
            Comment = comment;
            Ctimedate = ctimedate;
        }

        public Student Stud { get => stud; set => stud = value; }
        public Message Msg { get => msg; set => msg = value; }
        public string Comment { get => comment; set => comment = value; }
        public DateTime Ctimedate { get => ctimedate; set => ctimedate = value; }
    }
}