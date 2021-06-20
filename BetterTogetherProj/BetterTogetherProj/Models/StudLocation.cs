using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class StudLocation
    {
        string mail;
        float x;
        float y;
        DateTime timeStamp;

        public StudLocation(string mail, float x, float y, DateTime timeStamp)
        {
            Mail = mail;
            X = x;
            Y = y;
            TimeStamp = timeStamp;
        }

        public string Mail { get => mail; set => mail = value; }
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
        public StudLocation()
        {

        }
        public void Insert()
        {
            DBServicesReact dbs = new DBServicesReact();
            dbs.InsertStudLocation(this);
        }
    }
}