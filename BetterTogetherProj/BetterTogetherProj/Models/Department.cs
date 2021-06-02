using BetterTogetherProj.Models.DAL;
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
        int numstudents;


        public Department() { }

        public Department(int departmentCode, string departmentName, List<Student> slist, List<ExternalGroups> exgList, int numstudents)
        {
            DepartmentCode = departmentCode;
            DepartmentName = departmentName;
            Slist = slist;
            ExgList = exgList;
            Numstudents = numstudents;
        }

        public int DepartmentCode { get => departmentCode; set => departmentCode = value; }
        public string DepartmentName { get => departmentName; set => departmentName = value; }
        public List<Student> Slist { get => slist; set => slist = value; }
        public List<ExternalGroups> ExgList { get => exgList; set => exgList = value; }
        public int Numstudents { get => numstudents; set => numstudents = value; }

        public List<Department> GetDepartment()
        {
            DBServices dbs = new DBServices();
            List<Department> depList = dbs.GetDepartment();
            return depList;

        }
    }
}