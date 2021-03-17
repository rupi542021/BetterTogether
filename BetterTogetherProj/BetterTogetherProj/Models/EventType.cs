using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class EventType

    {
        string eventTypeName;

        public EventType(string eventTypeName)
        {
            EventTypeName = eventTypeName;
        }

        public string EventTypeName { get => eventTypeName; set => eventTypeName = value; }

        public EventType() { }

        public List<EventType> Getevtype()
        {
            DBServices dbs = new DBServices();
            List<EventType> evtypeList = dbs.Getevtype();
            return evtypeList;

        }

        public void InsertEventtype()
        {
            DBServices dbs = new DBServices();
            dbs.InsertEventtype(this);
        }
        
    }
}