using BetterTogetherProj.Models.DAL;
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
                XMLServices xml = new XMLServices();
                List<Residence> ResidenceList = xml.GetAllResidences();
                return ResidenceList;  
        }
        public Residence Read(string RName)
        {
            XMLServices xml = new XMLServices();
            Residence Res = xml.GetResidence(RName);
            return Res;
        }
    }
}
    