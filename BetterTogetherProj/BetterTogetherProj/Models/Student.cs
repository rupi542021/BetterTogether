﻿using BetterTogetherProj.Models;
using BetterTogetherProj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Student
    {
        string mail;
        string password;
        string fname;
        string lname;
        DateTime dateOfBirth;
        Department dep;
        int studyingYear;
        Residence homeTown;
        Residence addressStudying;
        string personalStatus;
        bool isAvailableCar;
        bool intrestedInCarPool;
        string photo;
        string gender;
        DateTime registrationDate;
        bool activeStatus;
        List<Hobby> hlist;
        List<Pleasure> plist;
        List<string> friendslist;
        List<StudentInCourse> studInCourse;
        double match;
        List<Preferences> preflist;
        int homeDist;
        int studyingDist;
        int agesRange;
        string token;
        DateTime beginingDate;
        DateTime finalDate;
        double distance;

        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public Department Dep { get => dep; set => dep = value; }
        public int StudyingYear { get => studyingYear; set => studyingYear = value; }
        public Residence HomeTown { get => homeTown; set => homeTown = value; }
        public Residence AddressStudying { get => addressStudying; set => addressStudying = value; }
        public string PersonalStatus { get => personalStatus; set => personalStatus = value; }
        public bool IsAvailableCar { get => isAvailableCar; set => isAvailableCar = value; }
        public bool IntrestedInCarPool { get => intrestedInCarPool; set => intrestedInCarPool = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Gender { get => gender; set => gender = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public bool ActiveStatus { get => activeStatus; set => activeStatus = value; }
        public List<Hobby> Hlist { get => hlist; set => hlist = value; }
        public List<Pleasure> Plist { get => plist; set => plist = value; }
        public List<string> Friendslist { get => friendslist; set => friendslist = value; }
        public List<StudentInCourse> StudInCourse { get => studInCourse; set => studInCourse = value; }
        public double Match { get => match; set => match = value; }
        public List<Preferences> Preflist { get => preflist; set => preflist = value; }
        public int HomeDist { get => homeDist; set => homeDist = value; }
        public int StudyingDist { get => studyingDist; set => studyingDist = value; }
        public int AgesRange { get => agesRange; set => agesRange = value; }
        public string Token { get => token; set => token = value; }
        public DateTime FinalDate { get => finalDate; set => finalDate = value; }
        public DateTime BeginingDate { get => beginingDate; set => beginingDate = value; }
        public double Distance { get => distance; set => distance = value; }

        public Student() { }

        public Student(string mail, string password, string fname, string lname, DateTime dateOfBirth, Department dep, int studyingYear, Residence homeTown, Residence addressStudying, string personalStatus, bool isAvailableCar, bool intrestedInCarPool, string photo, string gender, DateTime registrationDate, bool activeStatus, List<Hobby> hlist, List<Pleasure> plist, List<string> friendslist, List<StudentInCourse> studInCourse, double match, List<Preferences> preflist, int homeDist, int studyingDist, int agesRange, string token, int entryCounter, DateTime timeStep, DateTime beginingDate,DateTime finalDate, double distance)
        {
            this.mail = mail;
            this.password = password;
            this.fname = fname;
            this.lname = lname;
            this.dateOfBirth = dateOfBirth;
            this.dep = dep;
            this.studyingYear = studyingYear;
            this.homeTown = homeTown;
            this.addressStudying = addressStudying;
            this.personalStatus = personalStatus;
            this.isAvailableCar = isAvailableCar;
            this.intrestedInCarPool = intrestedInCarPool;
            this.photo = photo;
            this.gender = gender;
            this.registrationDate = registrationDate;
            this.activeStatus = activeStatus;
            this.hlist = hlist;
            this.plist = plist;
            this.friendslist = friendslist;
            this.studInCourse = studInCourse;
            this.match = match;
            this.preflist = preflist;
            this.homeDist = homeDist;
            this.studyingDist = studyingDist;
            this.agesRange = agesRange;
            this.token = token;
            this.beginingDate = beginingDate;
            this.finalDate = finalDate;
            this.distance = distance;
        }

        public Student getCurrentStudent(string mail)
        {
            DBServicesReact dbs = new DBServicesReact();
            Student stud = dbs.getCurrentStudent(mail);
            return stud;
        }

        public Student checkStudentRuppin(string email)
        {
            DBServicesReact dbs = new DBServicesReact();
            Student stud = dbs.checkStudentRuppin(email);
            return stud;
        }
        public Student checkStudentLogin(string email, string password)
        {
            DBServicesReact dbs = new DBServicesReact();
            Student stud = dbs.checkStudentLogin(email, password);
            return stud;
        }
        public void Insert()
        {
            DBServicesReact dbs = new DBServicesReact();
            dbs.InsertStudent(this);
            if (this.Plist.Count > 0)
            {
                dbs.InsertStudentPleasures(this);
            }
            if (this.Hlist.Count > 0)
            {
                dbs.InsertStudentHobbies(this);
            }

        }
        public List<Student> ReadAllStudent(string mail)
        {
            DBServicesReact db = new DBServicesReact();
            List<Student> studentsList = db.GetStudentsWithRecommend(mail);
            return studentsList;
        }
        public Student ReadStudentByMail(string mail)
        {
            DBServicesReact db = new DBServicesReact();
            Student s = db.GetStudentDetails(mail);
            return s;
        }
        public List<Student> GetStudentsWithRecommend(string mail)
        {
            DBServicesReact db = new DBServicesReact();
            List<Student> studentsList = db.GetStudentsWithRecommend(mail);
            return studentsList;
        }
        public List<Student> GetAllFavorites(string mail)
        {
            DBServicesReact db = new DBServicesReact();
            List<Student> studentsList = db.GetAllFavorites(mail);
            return studentsList;
        }
        public void UpdateStudentPtofile()
        {
            DBServicesReact dbs = new DBServicesReact();
            dbs.UpdateStudentPtofile(this);
            dbs.UpdateStudentPleasures(this);
            dbs.UpdateStudentHobbies(this);
        }

        public void updateUserPreferences(string mail, List<Preferences> prefList)
        {
            DBServicesReact dbs = new DBServicesReact();
            dbs.updateUserPreferences(mail, prefList);
        }
        public void updateUserPassword(string pass, Student stud)
        {
            DBServicesReact dbs = new DBServicesReact();
            dbs.updateUserPassword(pass, stud);
        }
        public void updateUserPrefRanges()
        {
            DBServicesReact dbs = new DBServicesReact();
            dbs.updateUserPrefRanges(this);
        }
        public void deleteUserProfile(string mail)
        {
            DBServicesReact dbs = new DBServicesReact();
            dbs.deleteUserProfile(mail);
        }
        public void InsertSToList()
        {
            DBServices dbs = new DBServices();
            dbs.InsertSToList(this);

        }
        public void updateToken(string mail, string token)
        {
            DBServicesReact dbs = new DBServicesReact();
            dbs.updateToken(mail, token);

        }

        //public List<Student> getStudentbyDepAndYear()
        //{
        //    DBServices dbs = new DBServices();
        //    List<Student> StuList = dbs.getStudentbyDepAndYear();
        //    return StuList;
        //}

        public List<double> getpercentregistered()
        {
            DBServices dbs = new DBServices();
            List<double> StuRatio = dbs.getpercentregistered();
            return StuRatio;
        }

        public IEnumerable<List<int>> getpercentregiByDep()
        {
            DBServices dbs = new DBServices();
            IEnumerable<List<int>> StuRatioDep = dbs.getpercentregiByDep();
            return StuRatioDep;
        }

        public IEnumerable<List<int>> getpercentregiByYear()
        {
            DBServices dbs = new DBServices();
            IEnumerable<List<int>> StuRatioDep = dbs.getpercentregiByYear();
            return StuRatioDep;
        }

        public List<int> getNumOfNewUsers(int numDays)
        {
            DBServices dbs = new DBServices();
            List<int> daysandusers = dbs.getNumOfNewUsers(numDays);
            return daysandusers;
        }

        public List<Student> GetCloseStudents(string mail)
        {
            DBServicesReact db = new DBServicesReact();
            List<Student> studentsList = db.GetCloseStudents(mail);
            return studentsList;
        }
    }
}