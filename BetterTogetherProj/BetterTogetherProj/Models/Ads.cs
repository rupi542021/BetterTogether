using BetterTogetherProj.Models.DAL;
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
        string subject;
        string adsImage;
        string subSubject;
        List<AdsFeedback> fbads;


        public Ads() { }

        public Ads(int adsCode, DateTime adsDate, string adsText, string subject, string adsImage, string subSubject, List<AdsFeedback> fbads)
        {
            AdsCode = adsCode;
            AdsDate = adsDate;
            AdsText = adsText;
            Subject = subject;
            AdsImage = adsImage;
            SubSubject = subSubject;
            Fbads = fbads;
        }

        public int AdsCode { get => adsCode; set => adsCode = value; }
        public DateTime AdsDate { get => adsDate; set => adsDate = value; }
        public string AdsText { get => adsText; set => adsText = value; }
        public string Subject { get => subject; set => subject = value; }
        public string AdsImage { get => adsImage; set => adsImage = value; }
        public string SubSubject { get => subSubject; set => subSubject = value; }
        public List<AdsFeedback> Fbads { get => fbads; set => fbads = value; }

        public void InsertToTable()
        {
            DBServices dbs = new DBServices();
            dbs.InsertToTable(this);
        }

        public List<Ads> GetAdBySub(string subnameFB)
        {
            DBServices dbs = new DBServices();
            List<Ads> AdFBList = dbs.GetAdBySub(subnameFB);
            return AdFBList;

        }
    }

}