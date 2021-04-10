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
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServices()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SqlConnection connect1(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommand1(String CommandSTR, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = CommandSTR; // can be Select, Insert, Update, Delete
            cmd.CommandTimeout = 10; // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
            return cmd;
        }
        public List<Questionnaire> GetQuestionnaire()
        {
            SqlConnection con = null;
            List<Questionnaire> qrList = new List<Questionnaire>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from department_P  inner join questionnaire_P3 on questionnaire_P3.departmentCode=department_P.departmentCode";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Questionnaire qr = new Questionnaire();

                    qr.QuestionnaireNum = Convert.ToInt16(dr["qrCode"]);
                    qr.QuestionnairePublish = Convert.ToDateTime(dr["publishDate"]);
                    qr.EndPublishDate = Convert.ToDateTime(dr["endPublishDate"]);
                    qr.SubQr = (string)dr["subQ"];
                    qr.Status = Convert.ToBoolean(dr["status"]);
                    qr.NumResponders = Convert.ToInt32(dr["numResponders"]);
                    qr.Dep = (new Department { DepartmentName = (string)dr["departmentName"] });
                    qr.QuestionnaireYear = Convert.ToInt16(dr["qrYear"]);

                    qrList.Add(qr);
                }

                return qrList;
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

        public int InsertQuestionnaire(Questionnaire qr)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(qr);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(Questionnaire qr)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            if (qr.Status == true)
                    sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}', '{6}')", qr.QuestionnairePublish.ToString("yyyy-MM-dd"), qr.SubQr, 1, qr.NumResponders, qr.EndPublishDate.ToString("yyyy-MM-dd"), qr.Dep.DepartmentCode, qr.QuestionnaireYear);
                else
                    sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}', '{6}')", qr.QuestionnairePublish.ToString("yyyy-MM-dd"), qr.SubQr, 0, qr.NumResponders, qr.EndPublishDate.ToString("yyyy-MM-dd"), qr.Dep.DepartmentCode, qr.QuestionnaireYear);
           
            String prefix = "INSERT INTO questionnaire_P3 " + "(publishDate,subQ,status,numResponders,endPublishDate,departmentCode, qrYear)";
            command = prefix + sb.ToString();
            foreach (var question in qr.Queslist)
            {
              String prefix1 = "INSERT INTO question_P3" + "(qCode, qText, questionType, qrCode, ansText1, ansText2, ansText3, ansText4, ansText5, ansText6) Values('" + question.Questionnum + "','" + question.QuestionText + "','" + question.QuestionType + "','" + qr.QuestionnaireNum + "','" + question.Anslist[0] + "','" + question.Anslist[1] + "','" + question.Anslist[2] + "','" + question.Anslist[3] + "','" + question.Anslist[4] + "','" + question.Anslist[5] + "')";
               command += prefix1;
            }                        
            
            return command;    
        }

        public List<Subject> Getsub()
        {
            SqlConnection con = null;
            List<Subject> subList = new List<Subject>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from  [dbo].[subject_P3]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Subject sub = new Subject();
                    sub.SubName = (string)dr["subName"];
                    subList.Add(sub);
                }

                return subList;
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

        public List<string> Getsamename(string subname)
        {
            SqlConnection con = null;
            List<string> subsubList = new List<string>();
            //Subject sub = new Subject();
            //sub.SubSubject = new List<string>();

            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from subSubject_P3 where subName='" + subname + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {   // Read till the end of the data into a row

                    subsubList.Add((string)dr["subSubName"]);
                }

                return subsubList;
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

        public int InsertToTable(Ads ad)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(ad);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(Ads ad)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}')", ad.AdsDate.ToString("yyyy-MM-dd"), ad.AdsText, ad.AdsImage, ad.Subject, ad.SubSubject);
            String prefix = "INSERT INTO ads_P3" + "(adsdate, adsText, adsimage, subName, subSubName )";
            command = prefix + sb.ToString();

            return command;
        }

        public List<Department> GetDepartment()
        {
            SqlConnection con = null;
            List<Department> depList = new List<Department>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from department_P";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Department dep = new Department();

                    dep.DepartmentCode = Convert.ToInt16(dr["departmentCode"]);
                    dep.DepartmentName = (string)dr["departmentName"];

                    depList.Add(dep);
                }

                return depList;
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

        public List<EventType> Getevtype()
        {
            SqlConnection con = null;
            List<EventType> evtypeList = new List<EventType>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from  [dbo].[eventType_P3]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    EventType evtype = new EventType();
                    evtype.Eventtype = (string)dr["eventTypeName"];
                    evtypeList.Add(evtype);
                }

                return evtypeList;
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

        public List<string> Getsametype(string evtypename)
        {
            SqlConnection con = null;
            List<string> evnameList = new List<string>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from eventName_P3 where eventTypeName='" + evtypename + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row

                    evnameList.Add((string)dr["eventname"]);

                }

                return evnameList;
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

        public int InsertEvent(Events ev)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(ev);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(Events ev)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}')", ev.EventDate.ToString("yyyy-MM-dd"), ev.EventText, ev.EventImage, ev.Eventname, ev.Eventtype);
            String prefix = "INSERT INTO events_P3" + "(eventDate, eventText, eventImage, eventname, eventTypeName)";
            command = prefix + sb.ToString();

            return command;
        }

        public int InsertEventtype(EventType evtype)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommandevtype(evtype);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommandevtype(EventType evtype)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}')", evtype.Eventtype);
            String prefix = "INSERT INTO eventType_P3" + "(eventTypeName)";
            command = prefix + sb.ToString();

            return command;
        }

        public int InsertEventname(EventType evname)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommandevname(evname);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommandevname(EventType evname)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", evname.Eventtype, evname.EventName[evname.EventName.Count - 1]);
            String prefix = "INSERT INTO eventName_P3" + "(eventTypeName,eventname )";
            command = prefix + sb.ToString();
            return command;
        }


        public int InsertSubject(Subject sub)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommandsub(sub);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommandsub(Subject sub)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}')", sub.SubName);
            String prefix = "INSERT INTO subject_P3" + "(subName)";
            command = prefix + sb.ToString();

            return command;
        }

        public int InsertSubsub(Subject subsub)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommandsubsub(subsub);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommandsubsub(Subject subsub)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", subsub.SubName, subsub.SubSubject[subsub.SubSubject.Count - 1]);
            String prefix = "INSERT INTO subSubject_P3" + "(subName, subSubName )";
            command = prefix + sb.ToString();

            return command;
        }

        public List<Ads> GetAdBySub(string subnameFB)
        {
            SqlConnection con = null;
            List<Ads> AdList = new List<Ads>();
            try
            {
                con = connect1("DBConnectionString");
                //String selectSTR = "select * from feedbackstudenttoads_P3 inner join ads_P3 on ads_P3.adCode=feedbackstudenttoads_P3.adCode inner join student_P on student_P.mail=feedbackstudenttoads_P3.studentmail where subName='" + subnameFB + "'";
                String selectSTR = "select * from ads_P3 where subName='" + subnameFB + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Ads ad = new Ads();
                    ad.AdsCode = Convert.ToInt16(dr["adCode"]);
                    ad.Fbads = GetFBad(ad.AdsCode);
                    ad.SubSubject = (string)dr["subSubName"];
                    AdList.Add(ad);
                }

                return AdList;
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

        public List<AdsFeedback> GetFBad(int adCode)
        {
            SqlConnection con = null;
            List<AdsFeedback> FBList = new List<AdsFeedback>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from feedbackstudenttoads_P3 inner join student_P on student_P.mail=feedbackstudenttoads_P3.studentmail  where feedbackstudenttoads_P3.adCode=" + adCode;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {   // Read till the end of the data into a row
                    AdsFeedback FB = new AdsFeedback();
                    FB.FbAdsNum = Convert.ToInt16(dr["fbAdsNum"]);
                    FB.Student = (new Student { Fname = (string)dr["firstName"] });
                    FB.Student.Mail = (string)dr["mail"];
                    FB.CommentText = (string)dr["commenttext"];
                    FB.CommentDate = Convert.ToDateTime(dr["commentdate"]);
                    FB.Managercomment = Convert.ToString(dr["managercomment"]);


                    FBList.Add(FB);

                }
                return FBList;

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
        public List<Events> GetEventByevtype(string evtypeFB)
        {
            SqlConnection con = null;
            List<Events> EventList = new List<Events>();
            try
            {
                con = connect1("DBConnectionString");
                //String selectSTR = "select * from feedbackstudenttoads_P3 inner join ads_P3 on ads_P3.adCode=feedbackstudenttoads_P3.adCode inner join student_P on student_P.mail=feedbackstudenttoads_P3.studentmail where subName='" + subnameFB + "'";
                String selectSTR = "select * from events_P3 where eventTypeName='" + evtypeFB + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Events ev = new Events();

                    ev.EventCode = Convert.ToInt16(dr["eventCode"]);
                    ev.Fbevents = GetFBev(ev.EventCode);
                    ev.Eventname = (string)dr["eventname"];
                    EventList.Add(ev);
                }

                return EventList;
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

        public List<EventsFeedback> GetFBev(int eventCode)
        {
            SqlConnection con = null;
            List<EventsFeedback> FBList = new List<EventsFeedback>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from feedbackstudenttoevents_P3 inner join student_P on student_P.mail=feedbackstudenttoevents_P3.studentmail  where feedbackstudenttoevents_P3.eventCode=" + eventCode;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {   // Read till the end of the data into a row
                    EventsFeedback FB = new EventsFeedback();
                    FB.FbEventNum = Convert.ToInt16(dr["fbEventNum"]);
                    FB.Student = (new Student { Fname = (string)dr["firstName"] });
                    FB.Student.Mail = (string)dr["mail"];
                    FB.CommentText = (string)dr["commenttext"];
                    FB.CommentDate = Convert.ToDateTime(dr["commentdate"]);
                    FB.Managercomment = Convert.ToString(dr["managercomment"]);


                    FBList.Add(FB);

                }
                return FBList;

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



        public int Insertcomment(AdsFeedback mngcom)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(mngcom);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(AdsFeedback mngcom)
        {
            String command;

            command = "update feedbackstudenttoads_P3 set managercomment ='" + mngcom.Managercomment + "' where feedbackstudenttoads_P3.fbAdsNum=" + mngcom.FbAdsNum;

            return command;
        }

        public int Insertcomment(EventsFeedback mngcom)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(mngcom);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(EventsFeedback mngcom)
        {
            String command;

            command = "update feedbackstudenttoevents_P3 set managercomment ='" + mngcom.Managercomment + "' where feedbackstudenttoevents_P3.fbEventNum=" + mngcom.FbEventNum;

            return command;
        }

        public List<Events> Geteventdetail()
        {
            SqlConnection con = null;
            List<Events> evdetailList = new List<Events>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from events_P3";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Events evdetail = new Events();
                    evdetail.EventCode = Convert.ToInt16(dr["eventCode"]);
                    evdetail.Eventtype = (string)dr["eventTypeName"];
                    evdetail.Eventname = (string)dr["eventname"];
                    evdetail.EventDate = Convert.ToDateTime(dr["eventDate"]);
                    evdetail.Studentsinevent = Getstudentinevent(evdetail.EventCode);
                    evdetail.EventText= (string)dr["eventText"];
                    evdetail.Status= Convert.ToBoolean(dr["status"]);
                    evdetailList.Add(evdetail);
                }

                return evdetailList;
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

        public List<Student> Getstudentinevent(int eventcode)
        {
            SqlConnection con = null;
            List<Student> studList = new List<Student>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from studentinevent_P3 where eventCode="+ eventcode;
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


        public int UpdateEvent(Events eventt)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect1("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommand(eventt);      // helper method to build the insert string

            cmd = CreateCommand1(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Update command String
        //--------------------------------------------------------------------

        private String BuildUpdateCommand(Events eventt)
        {
            String command;

            if (eventt.Status == true)// השמת הסטטוס TRUE=1 והפוך  
            {

                command = "update events_P3 set eventDate='" + eventt.EventDate.ToString("yyyy-MM-dd") + "',eventText='"+eventt.EventText+"' ,status=1 where eventCode=" + eventt.EventCode;
            }
            else
            {

                command = "update events_P3 set eventDate='" + eventt.EventDate.ToString("yyyy-MM-dd") + "',eventText='" + eventt.EventText +"' ,status =0 where eventCode=" + eventt.EventCode;

            }

            return command;

        }
    }
}