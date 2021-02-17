using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class Ads
    {
        int adsCode;
        DateTime adsDate;
        string adsText;
        Subject subject;
        string adsImage;

        public Ads(int adsCode, DateTime adsDate, string adsText, Subject subject, string adsImage)
        {
            AdsCode = adsCode;
            AdsDate = adsDate;
            AdsText = adsText;
            Subject = subject;
            AdsImage = adsImage;
        }

        public int AdsCode { get => adsCode; set => adsCode = value; }
        public DateTime AdsDate { get => adsDate; set => adsDate = value; }
        public string AdsText { get => adsText; set => adsText = value; }
        public Subject Subject { get => subject; set => subject = value; }
        public string AdsImage { get => adsImage; set => adsImage = value; }

        public Ads() { }
    }
}