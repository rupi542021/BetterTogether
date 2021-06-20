using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTogetherProj.Models
{
    public class Links
    {
        string whatsappLink;
        string driveLink;

        public Links(string whatsappLink, string driveLink)
        {
            this.whatsappLink = whatsappLink;
            this.driveLink = driveLink;
        }

        public string WhatsappLink { get => whatsappLink; set => whatsappLink = value; }
        public string DriveLink { get => driveLink; set => driveLink = value; }

        public Links() { }
        public Links ReadLinks(int depCode,int yearCode)
        {
            DBServicesReact dbs = new DBServicesReact();
            Links links = dbs.ReadLinks(depCode, yearCode);
            return links;
        }
    }
}