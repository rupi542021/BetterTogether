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

        public int Insert(Questionnaire qr)
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
            String prefix = "INSERT INTO questionnaire_P3 " + "(publishDate,subQ,status,numResponders,endPublishDate,departmentCode, qrYear )";
            command = prefix + sb.ToString();

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
            String prefix = "INSERT INTO events_P3" + "(eventDate, eventText, eventImage, eventname, eventTypeName )";
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
            sb.AppendFormat("Values('{0}', '{1}')", evname.Eventtype, evname.EventName[evname.EventName.Count-1]);
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

        //public List<StudentFeedToAds> GetFBAds(string subnameFB)
        //{
        //    SqlConnection con = null;
        //    List<StudentFeedToAds> FBList = new List<StudentFeedToAds>();
        //    try
        //    {
        //        con = connect1("DBConnectionString");
        //        String selectSTR = "select * from feedbackstudenttoads_P3 inner join ads_P3 on ads_P3.adCode=feedbackstudenttoads_P3.adCode inner join student_P on student_P.mail=feedbackstudenttoads_P3.studentmail where subName='" + subnameFB + "'";
        //        SqlCommand cmd = new SqlCommand(selectSTR, con);
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (dr.Read())
        //        {   // Read till the end of the data into a row
        //            StudentFeedToAds FB = new StudentFeedToAds();
        //            FB.Student = (new Student { Fname = (string)dr["firstName"] });
        //            FB.Student.Mail = (string)dr["mail"];
        //            FB.Ads = new Ads();
        //            FB.Ads.SubSubject = (string)dr["subSubName"];
        //            FB.Ads.AdsCode = Convert.ToInt16(dr["adCode"]);
        //            FB.CommentText = (string)dr["commenttext"];
        //            FB.CommentDate = Convert.ToDateTime(dr["commentdate"]);
        //            FB.Managercomment = (string)dr["managercomment"];
        //            FBList.Add(FB);
        //        }

        //        return FBList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }

        //    }

        //}

        //public List<StudentFeedToEvents> GetFBEvents(string evtypeFB)
        //{
        //    SqlConnection con = null;
        //    List<StudentFeedToEvents> FBList = new List<StudentFeedToEvents>();
        //    try
        //    {
        //        con = connect1("DBConnectionString");
        //        String selectSTR = "select * from feedbackstudenttoevents_P3 inner join events_P3 on events_P3.eventCode=feedbackstudenttoevents_P3.eventCode inner join student_P on student_P.mail=feedbackstudenttoevents_P3.studentmail where eventTypeName='" + evtypeFB + "'";
        //        SqlCommand cmd = new SqlCommand(selectSTR, con);
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (dr.Read())
        //        {   // Read till the end of the data into a row
        //            StudentFeedToEvents FB = new StudentFeedToEvents();
        //            FB.Student = (new Student { Fname = (string)dr["firstName"] });
        //            FB.Student.Mail = (string)dr["mail"];
        //            FB.Events = new Events();
        //            FB.Events.Eventname = (new EventName { Eventname = (string)dr["eventname"] });
        //            FB.Events.EventCode = Convert.ToInt16(dr["eventCode"]);
        //            FB.CommentText = (string)dr["commenttext"];
        //            FB.CommentDate = Convert.ToDateTime(dr["commentdate"]);
        //            FB.Managercomment = (string)dr["managercomment"];

        //            FBList.Add(FB);
        //        }

        //        return FBList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }

        //    }

        //}


        //public int Insertquestion(Question q)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect1("DBConnectionString"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    String cStr = BuildInsertCommand(q);      // helper method to build the insert string

        //    cmd = CreateCommand1(cStr, con);             // create the command

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
        //        return numEffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }

        //}

        ////--------------------------------------------------------------------
        //// Build the Insert command String
        ////--------------------------------------------------------------------
        //private String BuildInsertCommand(Question q)
        //{
        //    String command;
        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendFormat("Values('{0}','{1}','{2}')", q.Questionnum, q.QuestionText, q.QuestionType, q.Qr.QuestionnaireNum);
        //    String prefix = "INSERT INTO question_P3 " + "(qCode, qText, questionType, qrCode )";
        //    command = prefix + sb.ToString();

        //    return command;
        //}


        //public int Insertcomment(StudentFeedToAds mngcom)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect1("DBConnectionString"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    String cStr = BuildInsertCommand(mngcom);      // helper method to build the insert string

        //    cmd = CreateCommand1(cStr, con);             // create the command

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
        //        return numEffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }

        //}

        ////--------------------------------------------------------------------
        //// Build the Insert command String
        ////--------------------------------------------------------------------
        //private String BuildInsertCommand(StudentFeedToAds mngcom)
        //{
        //    String command;

        //    command = "update feedbackstudenttoads_P3 set managercomment ='" + mngcom.Managercomment + "' where feedbackstudenttoads_P3.studentmail ='" + mngcom.Student.Mail + "' and feedbackstudenttoads_P3.adCode =" + mngcom.Ads.AdsCode;

        //    return command;
        //}

        //public int Insertcomment(StudentFeedToEvents mngcom)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect1("DBConnectionString"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    String cStr = BuildInsertCommand(mngcom);      // helper method to build the insert string

        //    cmd = CreateCommand1(cStr, con);             // create the command

        //    try
        //    {
        //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
        //        return numEffected;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }

        //}

        ////--------------------------------------------------------------------
        //// Build the Insert command String
        ////--------------------------------------------------------------------
        //private String BuildInsertCommand(StudentFeedToEvents mngcom)
        //{
        //    String command;

        //    command = "update feedbackstudenttoevents_P3 set managercomment ='" + mngcom.Managercomment + "' where feedbackstudenttoevents_P3.studentmail ='" + mngcom.Student.Mail + "' and feedbackstudenttoevents_P3.eventCode =" + mngcom.Events.EventCode;

        //    return command;
        //}

        //public List<Events> Geteventdetail()
        //{
        //    SqlConnection con = null;
        //    List<Events> evdetailList = new List<Events>();
        //    try
        //    {
        //        con = connect1("DBConnectionString");
        //        String selectSTR = "select * from events_P3";
        //        SqlCommand cmd = new SqlCommand(selectSTR, con);
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (dr.Read())
        //        {   // Read till the end of the data into a row
        //            Events evdetail = new Events();
        //            evdetail.EventCode = Convert.ToInt16(dr["eventCode"]);
        //            evdetail.Eventtype = (new EventType { EventTypeName = (string)dr["eventTypeName"] });
        //            evdetail.Eventname = (new EventName { Eventname = (string)dr["eventname"] });
        //            evdetail.EventDate = Convert.ToDateTime(dr["eventDate"]);
        //            evdetail.ParticipantQu = Convert.ToInt32(dr["participantQu"]);
        //            evdetail.NotparticipantQu = Convert.ToInt32(dr["notparticipantQu"]);

        //            evdetailList.Add(evdetail);
        //        }

        //        return evdetailList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }

        //    }

        //}
    }
}
