using BetterTogetherProj.Models.DAL;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class Events
    {
        int eventCode;
        DateTime eventDate;
        string eventText;
        string eventtype;
        string eventImage;
        string eventname;
        List<EventsFeedback> fbevents;
        List<Student> studentsinevent;
        bool status;

        public int EventCode { get => eventCode; set => eventCode = value; }
        public DateTime EventDate { get => eventDate; set => eventDate = value; }
        public string EventText { get => eventText; set => eventText = value; }
        public string Eventtype { get => eventtype; set => eventtype = value; }
        public string EventImage { get => eventImage; set => eventImage = value; }
        public string Eventname { get => eventname; set => eventname = value; }
        public List<EventsFeedback> Fbevents { get => fbevents; set => fbevents = value; }
        public List<Student> Studentsinevent { get => studentsinevent; set => studentsinevent = value; }
        public bool Status { get => status; set => status = value; }

        public Events() { }

        public Events(int eventCode, DateTime eventDate, string eventText, string eventtype, string eventImage, string eventname, int participant, List<EventsFeedback> fbevents, List<Student> studentsinevent, bool status)
        {
            EventCode = eventCode;
            EventDate = eventDate;
            EventText = eventText;
            Eventtype = eventtype;
            EventImage = eventImage;
            Eventname = eventname;
            Fbevents = fbevents;
            Studentsinevent = studentsinevent;
            Status = status;
        }

        public void InsertEvent()
        {
            DBServices dbs = new DBServices();
            dbs.InsertEvent(this);
        }

        public List<Events> GetEventByevtype(string evtypeFB)
        {
            DBServices dbs = new DBServices();
            List<Events> EvFBList = dbs.GetEventByevtype(evtypeFB);
            return EvFBList;

        }
        public List<Events> Geteventdetail()
        {
            DBServices dbs = new DBServices();
            List<Events> evdetailList = dbs.Geteventdetail();
            return evdetailList;

        }

        public void UpdateEvent()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateEvent(this);

        }
    }
}