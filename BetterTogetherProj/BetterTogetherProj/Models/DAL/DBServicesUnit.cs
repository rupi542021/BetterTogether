using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace BetterTogetherProj.Models.DAL
{
    public class DBServicesUnit
    {
        public DBServicesUnit()
        {

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
        public void writeToLog(Exception ex)
        {
            // Create a string array with the lines of text
            string line = ex.Message;

            // Set a variable to the Documents path.
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "Exceptions.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Exceptions.txt"), true))
            {
                outputFile.WriteLine(line);
            }
        }
        public List<Events> GetAllEvents()
        {
            SqlConnection con = null;
            List<Events> eventsList = new List<Events>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "update events_P3 set status=0 where events_P3.eventDate<GETDATE() select * from events_P3 where status=1";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Events evdetail = new Events();
                    evdetail.EventCode = Convert.ToInt16(dr["eventCode"]);
                    evdetail.Eventtype = (string)dr["eventTypeName"];
                    evdetail.Eventname = (string)dr["eventname"];
                    evdetail.EventDate = Convert.ToDateTime(dr["eventDate"]);
                    evdetail.Studentsinevent = Getstudentinevent(evdetail.EventCode);
                    evdetail.EventText = (string)dr["eventText"];
                    evdetail.Status = Convert.ToBoolean(dr["status"]);
                    evdetail.EventImage = (string)dr["eventImage"];
                    eventsList.Add(evdetail);
                }
                return eventsList;
            }
            catch (Exception ex)
            {
                writeToLog(ex);
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
        public List<Student> Getstudentinevent(int eventcode)
        {
            SqlConnection con = null;
            List<Student> studList = new List<Student>();
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from studentinevent_P3 where eventCode=" + eventcode;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Student s = new Student();
                    s = s.ReadStudentByMail((string)dr["studentmail"]);

                    studList.Add(s);
                }

                return studList;
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
        public int InsertEventArrival(StudentInEvent se)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                writeToLog(ex);
                throw (ex);
            }

            String cStr = BuildInsertArrivalCommand(se);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                writeToLog(ex);
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
        private String BuildInsertArrivalCommand(StudentInEvent se)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            String prefix = "INSERT INTO studentinevent_P3 (eventCode, studentmail) ";
            sb.AppendFormat("Values('{0}', '{1}')", se.EventCode, se.Mail);
            command = prefix + sb.ToString();
            return command;
        }

        public int DeleteArrival(StudentInEvent se)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                writeToLog(ex);
                throw (ex);
            }

            String cStr = BuildDeleteArrivalCommand(se);
            cmd = CreateCommand(cStr, con);



            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                writeToLog(ex);
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
        private String BuildDeleteArrivalCommand(StudentInEvent se)
        {
            String command = "DELETE from [dbo].[studentinevent_P3] where [studentmail] = '" + se.Mail + "' and [eventCode] = '" + se.EventCode + "'";
            return command;
        }

        public List<Ads> GetAllAds()
        {
            SqlConnection con = null;
            List<Ads> AdList = new List<Ads>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select * from ads_P3 where status=1";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Ads ad = new Ads();
                    ad.AdsCode = Convert.ToInt16(dr["adCode"]);
                    //ad.Fbads = GetFBad(ad.AdsCode);
                    ad.SubSubject = (string)dr["subSubName"];
                    ad.Subject = (string)dr["subName"];
                    ad.AdsImage= (string)dr["adsimage"];
                    // ad.AdsDate = Convert.ToDateTime(dr["adsdate"]);
                    ad.AdsText = (string)dr["adsText"];
                   // ad.Status = Convert.ToBoolean(dr["status"]);
                    AdList.Add(ad);
                }
                return AdList;
            }
            catch (Exception ex)
            {
                writeToLog(ex);
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

        public int InsertEventComment(EventsFeedback ec)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                writeToLog(ex);
                throw (ex);
            }

            String cStr = BuildInsertECommentCommand(ec);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                writeToLog(ex);
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
        private String BuildInsertECommentCommand(EventsFeedback ec)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            String prefix = "INSERT INTO feedbackstudenttoevents_P3 (eventCode, studentmail,commenttext,commentdate) ";
            sb.AppendFormat("Values('{0}', '{1}','{2}', '{3}')", ec.FbEventNum, ec.Student.Mail,ec.CommentText,ec.CommentDate.ToString("yyyy-MM-dd H:mm:ss"));
            command = prefix + sb.ToString();
            return command;
        }
        public int InsertAdComment(AdsFeedback ac)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                writeToLog(ex);
                throw (ex);
            }

            String cStr = BuildInsertACommentCommand(ac);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                writeToLog(ex);
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
        private String BuildInsertACommentCommand(AdsFeedback ac)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            String prefix = "INSERT INTO feedbackstudenttoads_P3 (adCode, studentmail,commenttext,commentdate) ";
            sb.AppendFormat("Values('{0}', '{1}','{2}', '{3}')", ac.FbAdsNum, ac.Student.Mail, ac.CommentText, ac.CommentDate.ToString("yyyy-MM-dd H:mm:ss"));
            command = prefix + sb.ToString();
            return command;
        }
        public List<Questionnaire> GetAllQuestionnaire(int DepCode, int Year)
        {
            SqlConnection con = null;
            List<Questionnaire> qList = new List<Questionnaire>();
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from questionnaire_P3 where (departmentCode="+DepCode+ " or departmentCode=15) and (qrYear=" + Year+ " or qrYear=0) and status=1";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Questionnaire qr = new Questionnaire();

                    qr.QuestionnaireNum = Convert.ToInt16(dr["qrCode"]);
                    qr.QuestionnairePublish = Convert.ToDateTime(dr["publishDate"]);
                    qr.EndPublishDate = Convert.ToDateTime(dr["endPublishDate"]);
                    qr.SubQr = (string)dr["subQ"];
                    //qr.Status = Convert.ToBoolean(dr["status"]);
                    //qr.NumResponders = Convert.ToInt32(dr["numResponders"]);
                    qr.Dep = getStudDep(Convert.ToInt32(dr["departmentCode"]));
                    //qr.Dep.DepartmentCode = Convert.ToInt16(dr["departmentCode"]);
                    qr.QuestionnaireYear = Convert.ToInt16(dr["qrYear"]);
                    //qr.Queslist = getQuestionsbyNumqr(qr.QuestionnaireNum);
                    qList.Add(qr);
                }

                return qList;
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
                con = connect("DBConnectionString");

                String selectSTR = "SELECT * FROM department_P where departmentCode=" + DepID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    //if (DepID == Convert.ToInt32(dr["departmentCode"]))
                    {
                        Dep.DepartmentCode = Convert.ToInt32(dr["departmentCode"]);
                        Dep.DepartmentName = (string)(dr["departmentName"]);
                    }
                }
                return Dep;
            }
            catch (Exception ex)
            {
                writeToLog(ex);
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
       
        
    }
}