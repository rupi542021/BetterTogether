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
        string name;

        public Residence(string name)
        {
            Name = name;
        }
        public Residence()
        {

        }

        public string Name { get => name; set => name = value; }

        public List<Residence> Read()
        {
            List<Residence> lr = new List<Residence>();
            // Start with XmlReader object  
            //here, we try to setup Stream between the XML file nad xmlReader  
            using (XmlReader reader = XmlReader.Create("C:\\Users\\User\\Documents\\GitHub\\ProjectFinal\\BetterTogetherProj\\BetterTogetherProj\\App_Data\\XMLResidence.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "Value":
                                //Console.WriteLine("Value of the Element is : " + reader.ReadString());
                                Residence r = new Residence();
                                r.Name = reader.ReadString();
                                lr.Add(r);
                                break;

                        }
                    }
                    //Console.WriteLine("");
                    
                }
            }
            //Console.ReadKey();
            return lr;
        }
    }
}   
    