﻿using BetterTogetherProj.Models.DAL;
using project_classes.Models;
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
        bool status;
        int adsDuring;

        public int AdsCode { get => adsCode; set => adsCode = value; }
        public DateTime AdsDate { get => adsDate; set => adsDate = value; }
        public string AdsText { get => adsText; set => adsText = value; }
        public string Subject { get => subject; set => subject = value; }
        public string AdsImage { get => adsImage; set => adsImage = value; }
        public string SubSubject { get => subSubject; set => subSubject = value; }
        public List<AdsFeedback> Fbads { get => fbads; set => fbads = value; }
        public bool Status { get => status; set => status = value; }
        public int AdsDuring { get => adsDuring; set => adsDuring = value; }

        public Ads() { }

        public Ads(int adsCode, DateTime adsDate, string adsText, string subject, string adsImage, string subSubject, List<AdsFeedback> fbads, bool status, int adsDuring)
        {
            AdsCode = adsCode;
            AdsDate = adsDate;
            AdsText = adsText;
            Subject = subject;
            AdsImage = adsImage;
            SubSubject = subSubject;
            Fbads = fbads;
            Status = status;
            AdsDuring = adsDuring;

        }

        public void InsertToTable()
        {
            DBServices dbs = new DBServices();
            dbs.InsertToTable(this);
        }

        public List<Ads> GetAdBySub(int statusAd,string subnameFB)
        {
            DBServices dbs = new DBServices();
            List<Ads> AdFBList = dbs.GetAdBySub(statusAd,subnameFB);
            return AdFBList;

        }

        public void UpdateAd()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateAd(this);

        }
        public List<Ads> GetAllAds()
        {
            DBServicesUnit dbs = new DBServicesUnit();
            List<Ads> AdsList = dbs.GetAllAds();
            return AdsList;

        }

        public void UpdateStatusAd(int AdId)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateStatusAd(AdId);

        }

        public IEnumerable<List<Student>> GetStudentsByActivity()
        {
            DBServices dbs = new DBServices();
            IEnumerable<List<Student>> StudentsList = dbs.GetStudentsByActivity();
            return StudentsList;

        }

        public void UpdateActivity(string StudentMail, bool active)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateActivity(StudentMail, active);

        }
    }

}