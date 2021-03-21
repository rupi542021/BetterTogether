using BetterTogetherProj.Models.DAL;
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
        int participantQu;
        int notparticipantQu;
        List<EventsFeedback> fbevents;

        public Events(int eventCode, DateTime eventDate, string eventText, string eventtype, string eventImage, string eventname, int participantQu, int notparticipantQu, List<EventsFeedback> fbevents)
        {
            EventCode = eventCode;
            EventDate = eventDate;
            EventText = eventText;
            Eventtype = eventtype;
            EventImage = eventImage;
            Eventname = eventname;
            ParticipantQu = participantQu;
            NotparticipantQu = notparticipantQu;
            Fbevents = fbevents;
        }

        public int EventCode { get => eventCode; set => eventCode = value; }
        public DateTime EventDate { get => eventDate; set => eventDate = value; }
        public string EventText { get => eventText; set => eventText = value; }
        public string Eventtype { get => eventtype; set => eventtype = value; }
        public string EventImage { get => eventImage; set => eventImage = value; }
        public string Eventname { get => eventname; set => eventname = value; }
        public int ParticipantQu { get => participantQu; set => participantQu = value; }
        public int NotparticipantQu { get => notparticipantQu; set => notparticipantQu = value; }
        public List<EventsFeedback> Fbevents { get => fbevents; set => fbevents = value; }

        public Events() { }

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
        //public List<Events> Geteventdetail()
        //{
        //    DBServices dbs = new DBServices();
        //    List<Events> evdetailList = dbs.Geteventdetail();
        //    return evdetailList;

        //}

    }
}