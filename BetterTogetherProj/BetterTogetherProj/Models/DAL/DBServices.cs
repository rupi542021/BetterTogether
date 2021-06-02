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
        public List<Questionnaire> GetQuestionnaire(int statusQR, int deleteMode)
        {
            SqlConnection con = null;
            List<Questionnaire> qrList = new List<Questionnaire>();
            String selectSTR = "";
            try
            {
                con = connect1("DBConnectionString");
                if (deleteMode == 1)
                    selectSTR += "update questionnaire_P3 set status = 0 where (questionnaire_P3.endPublishDate < GETDATE() or questionnaire_P3.publishDate>GETDATE()) and questionnaire_P3.deleteMode=1";
                else if(deleteMode==0)
                {
                    selectSTR += "update questionnaire_P3 set status = 0 where (questionnaire_P3.endPublishDate < GETDATE() or questionnaire_P3.publishDate>GETDATE()) and (questionnaire_P3.deleteMode=0 OR questionnaire_P3.deleteMode IS NULL) ";
                    selectSTR += "update questionnaire_P3 set status = 1 where questionnaire_P3.endPublishDate >= GETDATE() and questionnaire_P3.publishDate <= GETDATE() and (questionnaire_P3.deleteMode=0 OR questionnaire_P3.deleteMode IS NULL)";
                }
                selectSTR += "select * from department_P  inner join questionnaire_P3 on questionnaire_P3.departmentCode=department_P.departmentCode where questionnaire_P3.status=" + statusQR+ " order by questionnaire_P3.qrCode";
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
                    qr.Dep = (new Department { DepartmentName = (string)dr["departmentName"] });
                    qr.Dep.DepartmentCode = Convert.ToInt16(dr["departmentCode"]);
                    qr.QuestionnaireYear = Convert.ToInt16(dr["qrYear"]);
                    qr.NumResponders = GetNumResponders(qr.QuestionnaireNum, qr.Dep.DepartmentCode, qr.QuestionnaireYear);
                    qr.Queslist = getQuestionsbyNumqr(qr.QuestionnaireNum);
                    if (dr["deleteMode"] is null)
                        qr.DeleteMode = Convert.ToBoolean(dr["deleteMode"]);
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

       

        public int GetNumResponders(int QrId, int depcode, int Qryear)
        {
            SqlConnection con = null;
            int Numres = 0;
            String selectSTR = "";
            try
            {
                con = connect1("DBConnectionString");
                if (depcode == 15 && Qryear == 0)
                {
                    selectSTR += "SELECT COUNT(distinct studentmail) as 'numResponders' from studentanswers_P3 INNER JOIN questionnaire_P3 on studentanswers_P3.qrCode = questionnaire_P3.qrCode where studentanswers_P3.qrCode =" + QrId;

                }
                else if (depcode != 15 && Qryear != 0)
                {
                    selectSTR += "SELECT COUNT(distinct studentmail) as 'numResponders' from studentanswers_P3 INNER JOIN questionnaire_P3 on studentanswers_P3.qrCode = questionnaire_P3.qrCode where studentanswers_P3.qrCode =" + QrId +
                                 "and studentanswers_P3.studentmail in(select student_P.mail from student_P inner join department_P on student_P.departmentCode = department_P.departmentCode where department_P.departmentCode =" + depcode +
                                 ")and studentanswers_P3.studentmail in(select  student_P.mail from student_P inner join questionnaire_P3 on student_P.studyingYear = questionnaire_P3.qrYear where student_P.studyingYear = " + Qryear + ")";
                }
                else if (depcode == 15 && Qryear != 0)
                {
                    selectSTR += "SELECT COUNT(distinct studentmail) as 'numResponders' from studentanswers_P3 INNER JOIN questionnaire_P3 on studentanswers_P3.qrCode = questionnaire_P3.qrCode where studentanswers_P3.qrCode =" + QrId +
                                 "and studentanswers_P3.studentmail in(select  student_P.mail from student_P inner join questionnaire_P3 on student_P.studyingYear = questionnaire_P3.qrYear where student_P.studyingYear = " + Qryear + ")";
                }
                else if (depcode != 15 && Qryear == 0)
                {
                    selectSTR += "SELECT COUNT(distinct studentmail) as 'numResponders' from studentanswers_P3 INNER JOIN questionnaire_P3 on studentanswers_P3.qrCode = questionnaire_P3.qrCode where studentanswers_P3.qrCode =" + QrId +
                                 "and studentanswers_P3.studentmail in(select student_P.mail from student_P inner join department_P on student_P.departmentCode = department_P.departmentCode where department_P.departmentCode =" + depcode+ ")";
                }
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Numres = Convert.ToInt16(dr["numResponders"]);

                }
                return Numres;
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


        public List<Question> getQuestionsbyNumqr(int questionnaireNum)
        {
            SqlConnection con = null;
            List<Question> qList = new List<Question>();

            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "select * from question_P3 where qrCode=" + questionnaireNum;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                while (dr.Read())

                {   // Read till the end of the data into a row
                    Question q = new Question();
                    q.Questionnum = Convert.ToInt16(dr["qCode"]);
                    q.QuestionText = (string)dr["qText"];
                    q.QuestionType = (string)dr["questionType"];

                    q.Anslist = new List<string>();
                    for (int i = 1; i < 7; i++)
                    {
                        q.Anslist.Add(Convert.ToString(dr["ansText"+i]));
                    }
                
                    qList.Add(q);
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
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}')", ad.AdsDate.ToString("yyyy-MM-dd"), ad.AdsText, ad.AdsImage, ad.Subject, ad.SubSubject, ad.Status,ad.AdsDuring);
            String prefix = "INSERT INTO ads_P3" + "(adsdate, adsText, adsimage, subName, subSubName, status,duringAd )";
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
            if(ev.Status==true)
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}','{5}')", ev.EventDate.ToString("yyyy-MM-dd"), ev.EventText, ev.EventImage, ev.Eventname, ev.Eventtype, 1);
            else
                sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}','{5}')", ev.EventDate.ToString("yyyy-MM-dd"), ev.EventText, ev.EventImage, ev.Eventname, ev.Eventtype, 0);

            String prefix = "INSERT INTO events_P3" + "(eventDate, eventText, eventImage, eventname, eventTypeName, status)";
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

        public List<Ads> GetAdBySub(int statusAd,string subnameFB)
        {
            SqlConnection con = null;
            List<Ads> AdList = new List<Ads>();
            String selectSTR = "";
            try
            {
                con = connect1("DBConnectionString");
                //String selectSTR = "select * from feedbackstudenttoads_P3 inner join ads_P3 on ads_P3.adCode=feedbackstudenttoads_P3.adCode inner join student_P on student_P.mail=feedbackstudenttoads_P3.studentmail where subName='" + subnameFB + "'";
                if (subnameFB == "בחרי נושא לתצוגה")
                {
                    selectSTR += "select * from ads_P3";
                }
                else if (subnameFB == "''")
                {
                    selectSTR += "update ads_P3 set status=0 where DATEDIFF(day,ads_P3.adsdate,getdate()) >ads_P3.duringAd select * from ads_P3 where status="+ statusAd;

                }
                else
                {
                    selectSTR += "select * from ads_P3 where subName='" + subnameFB + "'";
                }
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Ads ad = new Ads();
                    ad.AdsCode = Convert.ToInt16(dr["adCode"]);
                    ad.Fbads = GetFBad(ad.AdsCode);
                    ad.SubSubject = (string)dr["subSubName"];
                    ad.Subject= (string)dr["subName"];
                    ad.AdsDate= Convert.ToDateTime(dr["adsdate"]);
                    ad.AdsText= (string)dr["adsText"];
                    ad.AdsDuring= Convert.ToInt16(dr["duringAd"]);
                    ad.Status= Convert.ToBoolean(dr["status"]);
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
            String selectSTR = "";
            try
            {
                con = connect1("DBConnectionString");
                //String selectSTR = "select * from feedbackstudenttoads_P3 inner join ads_P3 on ads_P3.adCode=feedbackstudenttoads_P3.adCode inner join student_P on student_P.mail=feedbackstudenttoads_P3.studentmail where subName='" + subnameFB + "'";
                if (evtypeFB == "בחרי סוג אירוע לתצוגה")
                {
                     selectSTR+= "select * from events_P3";
                }
                else
                {
                     selectSTR += "select * from events_P3 where eventTypeName='" + evtypeFB + "'";
                }
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Events ev = new Events();

                    ev.EventCode = Convert.ToInt16(dr["eventCode"]);
                    ev.Fbevents = GetFBev(ev.EventCode);
                    ev.Eventname = (string)dr["eventname"];
                    ev.EventDate= Convert.ToDateTime(dr["eventDate"]);
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

        public List<Events> Geteventdetail(int statusEvent)
        {
            SqlConnection con = null;
            List<Events> evdetailList = new List<Events>();
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "update events_P3 set status=0 where events_P3.eventDate<GETDATE() select * from events_P3 where status="+ statusEvent;
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
                    evdetail.EventImage = (string)dr["eventImage"];
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
                //command += "update events_P3 set status=0 where events_P3.eventDate<GETDATE()";
            }
            else
            {

                command = "update events_P3 set eventDate='" + eventt.EventDate.ToString("yyyy-MM-dd") + "',eventText='" + eventt.EventText +"' ,status =0 where eventCode=" + eventt.EventCode;

            }
            return command;

        }



        public int UpdateQr(Questionnaire Qr)
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

            String cStr = BuildUpdateCommand(Qr);      // helper method to build the insert string

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

        private String BuildUpdateCommand(Questionnaire Qr)
        {
            String command;

            if (Qr.Status == true)// השמת הסטטוס TRUE=1 והפוך  
            {

                command = "update questionnaire_P3 set publishDate='" + Qr.QuestionnairePublish.ToString("yyyy-MM-dd") + "',endPublishDate='" + Qr.EndPublishDate.ToString("yyyy-MM-dd") + "',subQ='"+Qr.SubQr+"' ,status=1 where qrCode=" + Qr.QuestionnaireNum;
            }
            else
            {

                command = "update questionnaire_P3 set publishDate='" + Qr.QuestionnairePublish.ToString("yyyy-MM-dd") + "',endPublishDate='" + Qr.EndPublishDate.ToString("yyyy-MM-dd") + "',subQ='" + Qr.SubQr + "',status=0 where qrCode=" + Qr.QuestionnaireNum;

            }
            return command;

        }


        public int UpdateAd(Ads adss)
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

            String cStr = BuildUpdateCommand(adss);      // helper method to build the insert string

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

        private String BuildUpdateCommand(Ads adss)
        {
            String command;

            if (adss.Status == true)// השמת הסטטוס TRUE=1 והפוך  
            {

                command = "update ads_P3 set adsdate='" + adss.AdsDate.ToString("yyyy-MM-dd") + "',adsText='" + adss.AdsText + "' ,status=1 ,duringAd='"+adss.AdsDuring+"' where adCode=" + adss.AdsCode ;
            }
            else
            {

                command = "update ads_P3 set adsdate='" + adss.AdsDate.ToString("yyyy-MM-dd") + "',adsText='" + adss.AdsText + "' ,status=0 ,duringAd='" + adss.AdsDuring + "' where adCode=" + adss.AdsCode;
            }
            return command;

        }
        public List<StudentsAnswers> GetStudentAns(int numQr, int depcode, int depyear)
        {
            SqlConnection con = null;
            List<StudentsAnswers> SAnsList = new List<StudentsAnswers>();
            String selectSTR = "";
            try
            {
                con = connect1("DBConnectionString");
                if (depcode == 15 && depyear!=0)
                {
                    selectSTR += "select * from studentanswers_P3 INNER JOIN questionnaire_P3 on studentanswers_P3.qrCode = questionnaire_P3.qrCode where studentanswers_P3.qrCode =" + numQr +
                                 "and studentanswers_P3.studentmail in(select  student_P.mail from student_P inner join questionnaire_P3 on student_P.studyingYear = questionnaire_P3.qrYear where student_P.studyingYear =" + depyear + ")";

                }
                else if(depcode == 15 && depyear == 0)
                {
                    selectSTR += "select * from studentanswers_P3 INNER JOIN question_P3 on studentanswers_P3.qCode = question_P3.qCode and studentanswers_P3.qrCode = question_P3.qrCode where studentanswers_P3.qrCode =" + numQr;
                      
                }
                else if(depcode != 15 && depyear != 0)
                {
                    selectSTR += "select * from studentanswers_P3 INNER JOIN questionnaire_P3 on studentanswers_P3.qrCode = questionnaire_P3.qrCode where studentanswers_P3.qrCode ="+ numQr +
                                 "and studentanswers_P3.studentmail in(select student_P.mail from student_P inner join department_P on student_P.departmentCode = department_P.departmentCode where department_P.departmentCode ="+ depcode+
                                 ")and studentanswers_P3.studentmail in(select  student_P.mail from student_P inner join questionnaire_P3 on student_P.studyingYear = questionnaire_P3.qrYear where student_P.studyingYear = "+ depyear+")";
                }
                else if(depcode != 15 && depyear == 0)
                {
                    selectSTR += "select * from studentanswers_P3 INNER JOIN questionnaire_P3 on studentanswers_P3.qrCode = questionnaire_P3.qrCode where studentanswers_P3.qrCode =" + numQr +
                                 "and studentanswers_P3.studentmail in(select student_P.mail from student_P inner join department_P on student_P.departmentCode = department_P.departmentCode where department_P.departmentCode =" + depcode + ")";
                }
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row
                    StudentsAnswers SAns = new StudentsAnswers();
                    SAns.Mail = (string)dr["studentmail"];
                    SAns.QuestionnaireNum = Convert.ToInt16(dr["qrCode"]);
                    SAns.Questionnum = Convert.ToInt16(dr["qCode"]);
                    SAns.OpenAnswer = Convert.ToString(dr["openAnswer"]);
                    SAns.StudAns = new List<bool>();
                    for (int i = 1; i < 7; i++)
                    {
                        SAns.StudAns.Add(Convert.ToBoolean(dr["ansText" + i]));
                    }

                    SAnsList.Add(SAns);
                }

                return SAnsList;
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

        public int GetQuestionnaireNum()
        {
            int Qnum=0;
            SqlConnection con = null;
            try
            {
                con = connect1("DBConnectionString");
                String selectSTR = "SELECT max(qrCode+1) as 'qrCode' from questionnaire_P3";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {   // Read till the end of the data into a row

                    Qnum = Convert.ToInt16(dr["qrCode"]);

                }
                return Qnum;
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

        public int UpdateStatusQr(int QrId)
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

            String cStr = BuildUpdatestqrCommand(QrId);      // helper method to build the insert string

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

        private String BuildUpdatestqrCommand(int QrId)
        {
            String command;

            command = "update questionnaire_P3 set status=0, deleteMode=1 where qrCode=" + QrId;

            return command;

        }



        public int UpdateStatusAd(int AdId)
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

            String cStr = BuildUpdatestadCommand(AdId);      // helper method to build the insert string

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

        private String BuildUpdatestadCommand(int AdId)
        {
            String command;

            command = "update ads_P3 set status=0 where adCode=" + AdId;

            return command;

        }

        public int UpdateStatusEvent(int EventId)
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

            String cStr = BuildUpdatestevCommand(EventId);      // helper method to build the insert string

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

        private String BuildUpdatestevCommand(int EventId)
        {
            String command;

            command = "update events_P3 set status=0 where eventCode=" + EventId;

            return command;

        }
        public int InsertSToList(Student student)
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

            String cStr = BuildInsertCommand(student);      // helper method to build the insert string

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
        private String BuildInsertCommand(Student student)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}','{5}')", student.Mail, student.Fname, student.Lname, student.DateOfBirth.ToString("yyyy-MM-dd H:mm:ss"), student.Dep.DepartmentCode, student.StudyingYear );
            String prefix = "INSERT INTO Ruppin_StudentsData_P" + "(email,firstName,lastName,dateOfBirth,department,studyingYear)";
            command = prefix + sb.ToString();

            return command;
        }


        //public List<Questionnaire> GetRespondersbyDepAndYear()
        //{
        //    SqlConnection con = null;
        //    List<Questionnaire> qrList = new List<Questionnaire>();
        //    String selectSTR = "";
        //    try
        //    {
        //        con = connect1("DBConnectionString");

        //        selectSTR += "SELECT COUNT(distinct studentmail) as 'numResponders', questionnaire_P3.departmentCode, questionnaire_P3.qrYear from studentanswers_P3 INNER JOIN questionnaire_P3 on studentanswers_P3.qrCode = questionnaire_P3.qrCode group by questionnaire_P3.departmentCode, questionnaire_P3.qrYear";
        //        SqlCommand cmd = new SqlCommand(selectSTR, con);
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (dr.Read())
        //        {   // Read till the end of the data into a row
        //            Questionnaire qr = new Questionnaire();
        //            qr.NumResponders = Convert.ToInt16(dr["numResponders"]);
        //            qr.Dep = (new Department { DepartmentCode = Convert.ToInt16(dr["departmentCode"]) });             
        //            qr.QuestionnaireYear= Convert.ToInt16(dr["qrYear"]);
        //            qrList.Add(qr);
        //        }

        //        return qrList;
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
        //public List<Student> getStudentbyDepAndYear()
        //{
        //    SqlConnection con = null;
        //    List<Student> stuList = new List<Student>();
        //    String selectSTR = "";
        //    try
        //    {
        //        con = connect1("DBConnectionString");

        //        selectSTR += "select count (mail) as 'numstudents', student_P.departmentCode, student_P.studyingYear from student_P inner join department_P on student_P.departmentCode=department_P.departmentCode group by student_P.departmentCode, student_P.studyingYear";
        //        SqlCommand cmd = new SqlCommand(selectSTR, con);
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (dr.Read())
        //        {   // Read till the end of the data into a row
        //            Student stu = new Student();

        //            stu.Dep = (new Department { DepartmentCode = Convert.ToInt16(dr["departmentCode"]) });
        //            stu.Dep.Numstudents= Convert.ToInt16(dr["numstudents"]);
        //            stu.StudyingYear = Convert.ToInt16(dr["studyingYear"]);
        //            stuList.Add(stu);
        //        }

        //        return stuList;
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

        public List<double> getpercentregistered()
        {

            List<double> stuList = new List<double>();

            SqlConnection con1 = null;
            SqlConnection con2 = null;
            String selectSTR1 = "";
            String selectSTR2 = "";
            double stuRegistered =0.0;
            double stu = 0.0;
            try
            {
                con1 = connect1("DBConnectionString");
                selectSTR1 += "select count (mail) as 'numstudentsreg' from student_P ";
                

                SqlCommand cmd1 = new SqlCommand(selectSTR1, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr1.Read())
                {   // Read till the end of the data into a row

                    stuRegistered= Convert.ToDouble(dr1["numstudentsreg"]);
                    stuList.Add(stuRegistered);


                }
                con2 = connect1("DBConnectionString");
                selectSTR2 += "select count(email) as 'numstudents' from Ruppin_StudentsData_P ";


                SqlCommand cmd2 = new SqlCommand(selectSTR2, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr2.Read())
                {   // Read till the end of the data into a row

                  
                    stu = Convert.ToDouble(dr2["numstudents"]);
                    stuList.Add(stu);

                }

                return stuList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con1 != null)
                {
                    con1.Close();
                }
                if (con2 != null)
                {
                    con2.Close();
                }

            }

        }


        public List<int> GetpercentActiveQr()
        {

            List<int> QrList = new List<int>();

            SqlConnection con1 = null;
            SqlConnection con2 = null;
            String selectSTR1 = "";
            String selectSTR2 = "";
            int ActiveNum = 0;
            int TotalNum = 0;
            try
            {
                con1 = connect1("DBConnectionString");
                selectSTR1 += "select count (qrCode) as 'ActiveNum' from questionnaire_P3 where status=1 and DATEDIFF(day,questionnaire_P3.publishDate , GETDATE())<=30 ";

                SqlCommand cmd1 = new SqlCommand(selectSTR1, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr1.Read())
                {   // Read till the end of the data into a row

                    ActiveNum = Convert.ToInt16(dr1["ActiveNum"]);
                    QrList.Add(ActiveNum);


                }
                con2 = connect1("DBConnectionString");
                selectSTR2 += "select count (qrCode) as 'TotalNum' from questionnaire_P3";


                SqlCommand cmd2 = new SqlCommand(selectSTR2, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr2.Read())
                {   // Read till the end of the data into a row


                    TotalNum = Convert.ToInt16(dr2["TotalNum"]);
                    QrList.Add(TotalNum);

                }

                return QrList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con1 != null)
                {
                    con1.Close();
                }
                if (con2 != null)
                {
                    con2.Close();
                }

            }

        }
    }
}