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
        EventType eventtype;
        string eventImage;
        EventName eventname;
        int participantQu;
        int notparticipantQu;

        public int EventCode { get => eventCode; set => eventCode = value; }
        public DateTime EventDate { get => eventDate; set => eventDate = value; }
        public string EventText { get => eventText; set => eventText = value; }
        public EventType Eventtype { get => eventtype; set => eventtype = value; }
        public string EventImage { get => eventImage; set => eventImage = value; }
        public EventName Eventname { get => eventname; set => eventname = value; }
        public int ParticipantQu { get => participantQu; set => participantQu = value; }
        public int NotparticipantQu { get => notparticipantQu; set => notparticipantQu = value; }

        public Events() { }

        public Events(int eventCode, DateTime eventDate, string eventText, EventType eventtype, string eventImage, EventName eventname, int participantQu, int notparticipantQu)
        {
            EventCode = eventCode;
            EventDate = eventDate;
            EventText = eventText;
            Eventtype = eventtype;
            EventImage = eventImage;
            Eventname = eventname;
            ParticipantQu = participantQu;
            NotparticipantQu = notparticipantQu;
        }

        public void InsertEvent()
        {
            DBServices dbs = new DBServices();
            dbs.InsertEvent(this);
        }

        public List<Events> Geteventdetail()
        {
            DBServices dbs = new DBServices();
            List<Events> evdetailList = dbs.Geteventdetail();
            return evdetailList;

        }

    }
}