using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class ExternalGroups
    {
        int groupCode;
        string uniqueLink;
        string groupName;
        Department dep;
        Course c;

       
     
        public ExternalGroups() { }

        public ExternalGroups(int groupCode, string uniqueLink, string groupName, Department dep, Course c)
        {
            GroupCode = groupCode;
            UniqueLink = uniqueLink;
            GroupName = groupName;
            Dep = dep;
            C = c;
        }

        public int GroupCode { get => groupCode; set => groupCode = value; }
        public string UniqueLink { get => uniqueLink; set => uniqueLink = value; }
        public string GroupName { get => groupName; set => groupName = value; }
        public Department Dep { get => dep; set => dep = value; }
        public Course C { get => c; set => c = value; }
    }
}