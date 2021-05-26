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

        public StudLocation(string mail, float x, float y)
        {
            Mail = mail;
            X = x;
            Y = y;
        }

        public string Mail { get => mail; set => mail = value; }
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }

        public StudLocation()
        {

        }
    }
}