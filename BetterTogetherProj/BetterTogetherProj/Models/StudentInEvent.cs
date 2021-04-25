using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class StudentInEvent
    {
        int eventCode;
        string mail;

        public StudentInEvent() { }
        public StudentInEvent(int eventCode, string mail)
        {
            EventCode = eventCode;
            Mail = mail;
        }

        public int EventCode { get => eventCode; set => eventCode = value; }
        public string Mail { get => mail; set => mail = value; }

        public void InsertEventArrival()
        {
            DBServicesUnit dbs = new DBServicesUnit();
            dbs.InsertEventArrival(this);
        }
        public void Delete()
        {
            DBServicesUnit dbs = new DBServicesUnit();
            dbs.DeleteArrival(this);
        }
    }
}