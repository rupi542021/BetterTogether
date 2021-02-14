﻿using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BetterTogetherProj.Models.DAL
{
    public class DBServices
    {
<<<<<<< HEAD
        public SqlConnection connect(String conString)
        {

=======
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServices()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SqlConnection connect(String conString)
        {
>>>>>>> 6604b7c39b791e5a388c14375836282b341355da
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {
<<<<<<< HEAD

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }
        public List<Questionnaire> GetQuestionnaire()
        {
            SqlConnection con = null;
            List<Questionnaire> qrList = new List<Questionnaire>();
=======
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = CommandSTR; // can be Select, Insert, Update, Delete
            cmd.CommandTimeout = 10; // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
            return cmd;
        }

        public Student checkStudentRuppin(string email)
        {
            SqlConnection con = null;
            Student stud = new Student();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Ruppin_StudentsData_P";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    if (email == (string)dr["email"])
                    {
                        stud.Mail = (string)dr["email"];
                        stud.Fname = (string)(dr["firstName"]);
                        stud.Lname = (string)(dr["lastName"]);
                        stud.DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]);
                        stud.Dep = getStudDep(Convert.ToInt32(dr["department"])); 
                        stud.StudyingYear = Convert.ToInt32(dr["studyingYear"]);
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
>>>>>>> 6604b7c39b791e5a388c14375836282b341355da

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

<<<<<<< HEAD
                String selectSTR = "select * from questionnaire_P2";

=======
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
>>>>>>> 6604b7c39b791e5a388c14375836282b341355da
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
<<<<<<< HEAD
                    Questionnaire qr = new Questionnaire();

                    qr.QuestionnaireNum = Convert.ToInt16(dr["qrCode"]);
                    qr.QuestionnairePublish= Convert.ToDateTime(dr["publishDate"]);
                    qr.EndPublishDate= Convert.ToDateTime(dr["endPublishDate"]);
                    qr.SubQr= (string)dr["subQ"];
                    qr.Status = Convert.ToBoolean(dr["status"]);
                    qr.NumResponders = Convert.ToInt32(dr["numResponders"]);
                  
                    qrList.Add(qr);
                }

                return qrList;
=======
                    Pleasure p = new Pleasure();
                    p.Pcode = Convert.ToInt32(dr["pCode"]);
                    p.Pname = (string)(dr["pName"]);
                    p.Picon = (string)(dr["pIcon"]);
                    PList.Add(p);
                }
                return PList;
>>>>>>> 6604b7c39b791e5a388c14375836282b341355da
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
<<<<<<< HEAD

        }
=======
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



>>>>>>> 6604b7c39b791e5a388c14375836282b341355da
    }
}