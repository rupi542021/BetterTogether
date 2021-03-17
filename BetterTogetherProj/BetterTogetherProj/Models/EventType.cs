using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class EventType

    {
        string eventtype;
        List<string> eventName;

        

        public EventType() { }

        public EventType(string eventtype, List<string> eventName)
        {
            Eventtype = eventtype;
            EventName = eventName;
        }

        public string Eventtype { get => eventtype; set => eventtype = value; }
        public List<string> EventName { get => eventName; set => eventName = value; }

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

        public List<string> Getsametype(string evtypename)
        {
            DBServices dbs = new DBServices();
            List<string> evnameList = dbs.Getsametype(evtypename);
            return evnameList;

        }
     
        public void InsertEventname()
        {
            DBServices dbs = new DBServices();
            dbs.InsertEventname(this);
        }
    }
}