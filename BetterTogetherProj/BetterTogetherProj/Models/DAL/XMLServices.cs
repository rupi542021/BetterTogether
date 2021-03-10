using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace BetterTogetherProj.Models.DAL
{
    public class XMLServices
    {

        public List<Residence> GetAllResidences()
        {
            List<Residence> ResidenceList = new List<Residence>();
            List<string> NamesList = new List<string>();
            List<double> YList = new List<double>();
            List<double> XList = new List<double>();
            String xmlFile = HttpContext.Current.Server.MapPath("~/App_Data/XMLResidence.xml");
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name.ToString() == "Value")
                        {
                            NamesList.Add(reader.ReadString());
                        }
                        if (reader.Name.ToString() == "Y")
                        {
                            YList.Add(Convert.ToDouble(reader.ReadString()));
                        }
                        if (reader.Name.ToString() == "X")
                        {
                            XList.Add(Convert.ToDouble(reader.ReadString()));
                        }
                    }
                }

                int s = 1;
                for (int t = 4; t < NamesList.Count; t = t + 9)
                {
                    Residence r = new Residence();
                    r.Id = s;
                    r.Name = NamesList[t];
                    r.Y = YList[s - 1];
                    r.X = XList[s - 1];
                    ResidenceList.Add(r);
                    s++;
                }
                var newList = ResidenceList.OrderBy(x => x.Name).ToList();
                return newList;
            }
        }
        public Residence GetResidence(string RName)
        {
            Residence r = new Residence();
            List<string> NamesList = new List<string>();
            List<double> YList = new List<double>();
            List<double> XList = new List<double>();
            String xmlFile = HttpContext.Current.Server.MapPath("~/App_Data/XMLResidence.xml");
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name.ToString() == "Value")
                        {
                            NamesList.Add(reader.ReadString());
                        }
                        if (reader.Name.ToString() == "Y")
                        {
                            YList.Add(Convert.ToDouble(reader.ReadString()));
                        }
                        if (reader.Name.ToString() == "X")
                        {
                            XList.Add(Convert.ToDouble(reader.ReadString()));
                        }
                    }
                }

                int s = 1;
                for (int t = 4; t < NamesList.Count; t = t + 9)
                {
                    if (NamesList[t] == RName)
                    {
                        r.Id = s;
                        r.Name = NamesList[t];
                        r.Y = YList[s - 1];
                        r.X = XList[s - 1];
                        return r;
                    }
                    s++;
                }
                return null;
            }
        }
    }
}