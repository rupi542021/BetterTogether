using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace BetterTogetherProj.Models.DAL
{
    public class DBServicesReact
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServicesReact()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SqlConnection connect(String conString)
        {

            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = CommandSTR;
            cmd.CommandTimeout = 10;
            cmd.CommandType = System.Data.CommandType.Text;
            return cmd;
        }
       

        public Student checkStudentRuppin(string email)
        {
            SqlConnection con = null;
            Student stud = new Student();

            try
            {
                con = connect("DBConnectionString"); 

                String selectSTR = "SELECT * FROM Ruppin_StudentsData_P";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    if (email == (string)dr["email"])
                    {
                        bool alreadyExist = checkIfExist(email);
                        if (alreadyExist == false)
                        {
                            stud.Mail = (string)dr["email"];
                            stud.Fname = (string)(dr["firstName"]);
                            stud.Lname = (string)(dr["lastName"]);
                            stud.DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]);
                            stud.Dep = getStudDep(Convert.ToInt32(dr["department"]));
                            stud.StudyingYear = Convert.ToInt32(dr["studyingYear"]);
                            return stud;
                        }

                        else {
                            stud.Mail = null;
                            return stud;
                        }
                    }
                }
                stud.Mail = null;
                return stud;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        public bool checkIfExist(string email)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT mail FROM student_P where [mail] ='" + email + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
    }
    public Student checkStudentLogin(string email,string password)
        {
            SqlConnection con = null;
            Student stud = new Student();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM student_P";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    if (email == (string)dr["mail"]&&password==(string)dr["password"])
                    {
                        stud.Mail = (string)dr["mail"];
                        stud.Password = (string)dr["password"];
                        stud.Fname = (string)(dr["firstName"]);
                        stud.Lname = (string)(dr["lastName"]);
                        stud.DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]);
                        stud.Dep = getStudDep(Convert.ToInt32(dr["departmentCode"]));
                        stud.StudyingYear = Convert.ToInt32(dr["studyingYear"]);
                        stud.HomeTown= (string)(dr["homeTown"]);
                        stud.AddressStudying= (string)(dr["adrressStudying"]);
                        stud.PersonalStatus= (string)(dr["personalStatus"]);
                        stud.IsAvailableCar = Convert.ToBoolean(dr["isAvailableCar"]);
                        stud.IntrestedInCarPool = Convert.ToBoolean(dr["intrestedInCarPool"]);
                        stud.Photo= (string)(dr["photo"]);
                        stud.Gender = (string)(dr["gender"]);
                        stud.RegistrationDate = Convert.ToDateTime(dr["registrationDate"]);
                        stud.ActiveStatus = Convert.ToBoolean(dr["active"]);
                        return stud;
                    }
                }
                stud.Mail = null;
                return stud;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        public Department getStudDep(int DepID)
        {
            SqlConnection con = null;
            Department Dep = new Department();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM department_P";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    if (DepID == Convert.ToInt32(dr["departmentCode"]))
                    {
                        Dep.DepartmentCode = Convert.ToInt32(dr["departmentCode"]);
                        Dep.DepartmentName = (string)(dr["departmnetName"]);
                    }
                }
                return Dep;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        public List<Pleasure> GetAllPleasures()
        {
            SqlConnection con = null;
            List<Pleasure> PList = new List<Pleasure>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM pleasure_P";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Pleasure p = new Pleasure();
                    p.Pcode = Convert.ToInt32(dr["pCode"]);
                    p.Pname = (string)(dr["pName"]);
                    p.Picon = (string)(dr["pIcon"]);
                    PList.Add(p);
                }
                return PList;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<Hobby> GetAllHoddies()
        {
            SqlConnection con = null;
            List<Hobby> HList = new List<Hobby>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM hobby_P";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Hobby h = new Hobby();
                    h.Hcode = Convert.ToInt32(dr["hCode"]);
                    h.Hname = (string)(dr["hName"]);
                    h.Hicon = (string)(dr["hIcon"]);
                    HList.Add(h);
                }
                return HList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int InsertStudent(Student student)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertStudentCommand(student);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildInsertStudentCommand(Student student)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            String prefix = "INSERT INTO Student_P" + "(mail, password, firstName, lastName, dateOfBirth, departmentCode, studyingYear, homeTown, adrressStudying, personalStatus, isAvailableCar, intrestedInCarpool, photo, gender, registrationDate, active) ";
            sb.AppendFormat("Values('{0}', '{1}','{2}', '{3}','{4}', '{5}','{6}', '{7}','{8}', '{9}','{10}', '{11}','{12}', '{13}','{14}', '{15}')", student.Mail, student.Password, student.Fname, student.Lname, student.DateOfBirth.ToString("yyyy-MM-dd H:mm:ss"), student.Dep.DepartmentCode, student.StudyingYear, student.HomeTown, student.AddressStudying, student.PersonalStatus, student.IsAvailableCar, student.IntrestedInCarPool, student.Photo, student.Gender, student.RegistrationDate.ToString("yyyy-MM-dd H:mm:ss"), true);
            command = prefix + sb.ToString();
            return command;
        }

        public int InsertStudentPleasures(Student student)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertStudentPleasuresCommand(student);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildInsertStudentPleasuresCommand(Student student)
        {
            String prefix = "";
            for (int i = 0; i < student.Plist.Count; i++)
            {
                prefix += "INSERT INTO student_pleasure_P (studentmail, pleasurepCode) Values('" + student.Mail + "'," + student.Plist[i].Pcode + ") ";
            }
            return prefix;
        }

        public int InsertStudentHobbies(Student student)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertStudentHobbiesCommand(student);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildInsertStudentHobbiesCommand(Student student)
        {
            String prefix = "";
            foreach (var hobby in student.Hlist)
            {
                prefix += "INSERT INTO student_hobby_P (studentmail, hobbyhCode) Values('" + student.Mail + "'," + hobby.Hcode + ") ";

            }
            return prefix;
        }


    }
}
