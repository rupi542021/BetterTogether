using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class Preferences
    {
        int prefcode;
        string prefname;

        public Preferences(int prefcode, string prefname)
        {
            Prefcode = prefcode;
            Prefname = prefname;
        }

        public int Prefcode { get => prefcode; set => prefcode = value; }
        public string Prefname { get => prefname; set => prefname = value; }

        public Preferences()
        {

        }
    }
}