using BetterTogetherProj.Models;
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

        public Student(){}

        public Student(string mail, string password, string fname, string lname, DateTime dateOfBirth, Department dep, int studyingYear, Residence homeTown, Residence addressStudying, string personalStatus, bool isAvailableCar, bool intrestedInCarPool, string photo, string gender, DateTime registrationDate, bool activeStatus, List<Hobby> hlist, List<Pleasure> plist, List<string> friendslist, List<StudentInCourse> studInCourse, double match)
        {
            Mail = mail;
            Password = password;
            Fname = fname;
            Lname = lname;
            DateOfBirth = dateOfBirth;
            Dep = dep;
            StudyingYear = studyingYear;
            HomeTown = homeTown;
            AddressStudying = addressStudying;
            PersonalStatus = personalStatus;
            IsAvailableCar = isAvailableCar;
            IntrestedInCarPool = intrestedInCarPool;
            Photo = photo;
            Gender = gender;
            RegistrationDate = registrationDate;
            ActiveStatus = activeStatus;
            Hlist = hlist;
            Plist = plist;
            Friendslist = friendslist;
            StudInCourse = studInCourse;
            Match = match;
        }

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
        
        public Student checkStudentRuppin(string email)
        {
            DBServicesReact dbs = new DBServicesReact();
            Student stud = dbs.checkStudentRuppin(email);
            return stud;
        }
        public Student checkStudentLogin(string email,string password)
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
        public void UpdateStudentPtofile() {
            DBServicesReact dbs = new DBServicesReact();
            dbs.UpdateStudentPtofile(this);
            dbs.UpdateStudentPleasures(this);
            dbs.UpdateStudentHobbies(this);
        }

    }
}