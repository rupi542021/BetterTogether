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
        string preficon;
        

        public Preferences()
        {

        }

        public Preferences(int prefcode, string prefname, string preficon)
        {
            Prefcode = prefcode;
            Prefname = prefname;
            Preficon = preficon;
        }

        public int Prefcode { get => prefcode; set => prefcode = value; }
        public string Prefname { get => prefname; set => prefname = value; }
        public string Preficon { get => preficon; set => preficon = value; }
    }
}