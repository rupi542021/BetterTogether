using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace BetterTogetherProj.Models
{
    public class Residence
    {
        int id;
        string name;
        double x;
        double y;


        public Residence()
        {

        }

        public Residence(int id, string name, double x, double y)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

        public List<Residence> Read()
        { 
            List<Residence> ResidenceList = new List<Residence>();
            List<string> NamesList = new List<string>();
            List<double> YList = new List<double>();
            List<double> XList = new List<double>();
            using (XmlReader reader = XmlReader.Create("C:\\Users\\User\\Documents\\GitHub\\ProjectFinal\\BetterTogetherProj\\BetterTogetherProj\\App_Data\\XMLResidence.xml"))
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
                for (int t = 4; t < NamesList.Count; t=t+9)
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
    }
}
    