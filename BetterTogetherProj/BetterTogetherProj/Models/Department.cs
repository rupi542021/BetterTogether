using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Department
    {
        int departmentCode;
        string departmentName;
        List<Student> slist;
        List<ExternalGroups> exgList;
        string year;

        

      


        public Department() { }

        public Department(int departmentCode, string departmentName, List<Student> slist, List<ExternalGroups> exgList, string year)
        {
            DepartmentCode = departmentCode;
            DepartmentName = departmentName;
            Slist = slist;
            ExgList = exgList;
            Year = year;
        }

        public int DepartmentCode { get => departmentCode; set => departmentCode = value; }
        public string DepartmentName { get => departmentName; set => departmentName = value; }
        public List<Student> Slist { get => slist; set => slist = value; }
        public List<ExternalGroups> ExgList { get => exgList; set => exgList = value; }
        public string Year { get => year; set => year = value; }
    }
}