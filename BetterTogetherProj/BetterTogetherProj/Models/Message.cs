using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Message
    {
        int mcode;
        string mtext;
        DateTime mpublishdate;
        List<StudentCommentOnMessage> commentsList;

        public Message()
        {

        }

        public Message(int mcode, string mtext, DateTime mpublishdate, List<StudentCommentOnMessage> commentsList)
        {
            Mcode = mcode;
            Mtext = mtext;
            Mpublishdate = mpublishdate;
            CommentsList = commentsList;
        }

        public int Mcode { get => mcode; set => mcode = value; }
        public string Mtext { get => mtext; set => mtext = value; }
        public DateTime Mpublishdate { get => mpublishdate; set => mpublishdate = value; }
        public List<StudentCommentOnMessage> CommentsList { get => commentsList; set => commentsList = value; }
    }
}