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
        string homeTown;
        string addressStudying;
        string personalStatus;
        bool isAvailableCar;
        bool intrestedInCarPool;
        string photo;
        string gender;
        DateTime registrationDate;
        bool activeStatus;
        List<Hobby> hlist;
        List<Pleasure> plist;
        List<Student> friendslist;
        List<StudentInCourse> studInCourse;


        public Student(){}

        public Student(string mail, string password, string fname, string lname, DateTime dateOfBirth, Department dep, int studyingYear, string homeTown, string addressStudying, string personalStatus, bool isAvailableCar, bool intrestedInCarPool, string photo, string gender, DateTime registrationDate, bool activeStatus, List<Hobby> hlist, List<Pleasure> plist, List<Student> friendslist, List<StudentInCourse> studInCourse)
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
        }

        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public Department Dep { get => dep; set => dep = value; }
        public int StudyingYear { get => studyingYear; set => studyingYear = value; }
        public string HomeTown { get => homeTown; set => homeTown = value; }
        public string AddressStudying { get => addressStudying; set => addressStudying = value; }
        public string PersonalStatus { get => personalStatus; set => personalStatus = value; }
        public bool IsAvailableCar { get => isAvailableCar; set => isAvailableCar = value; }
        public bool IntrestedInCarPool { get => intrestedInCarPool; set => intrestedInCarPool = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Gender { get => gender; set => gender = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public bool ActiveStatus { get => activeStatus; set => activeStatus = value; }
        public List<Hobby> Hlist { get => hlist; set => hlist = value; }
        public List<Pleasure> Plist { get => plist; set => plist = value; }
        public List<Student> Friendslist { get => friendslist; set => friendslist = value; }
        public List<StudentInCourse> StudInCourse { get => studInCourse; set => studInCourse = value; }

        public Student checkStudentRuppin(string email)
        {
            DBServicesReact dbs = new DBServicesReact();
            Student stud = dbs.checkStudentRuppin(email);
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
       
    }
}