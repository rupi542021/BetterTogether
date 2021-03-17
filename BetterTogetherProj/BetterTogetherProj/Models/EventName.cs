//using BetterTogetherProj.Models.DAL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace BetterTogetherProj.Models
//{
//    public class EventName
//    {
//        string eventname;
//        EventType eventtype;

//        public EventName(string eventname, EventType eventtype)
//        {
//            Eventname = eventname;
//            Eventtype = eventtype;
//        }

//        public string Eventname { get => eventname; set => eventname = value; }
//        public EventType Eventtype { get => eventtype; set => eventtype = value; }

//        public EventName() { }


//        public List<EventName> Getsametype(string evtypename)
//        {
//            DBServices dbs = new DBServices();
//            List<EventName> evnameList = dbs.Getsametype(evtypename);
//            return evnameList;

//        }

//        public void InsertEventname()
//        {
//            DBServices dbs = new DBServices();
//            dbs.InsertEventname(this);
//        }
//    }
//}