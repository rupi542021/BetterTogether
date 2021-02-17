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
                String selectSTR = "select * from questionnaire_P3";
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
                sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}')", qr.QuestionnairePublish.ToString("yyyy-MM-dd"), qr.SubQr, 1, qr.NumResponders, qr.EndPublishDate.ToString("yyyy-MM-dd"));
            else
                sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}')", qr.QuestionnairePublish.ToString("yyyy-MM-dd"), qr.SubQr, 0, qr.NumResponders, qr.EndPublishDate.ToString("yyyy-MM-dd"));
            String prefix = "INSERT INTO questionnaire_P3 " + "(publishDate,subQ,status,numResponders,endPublishDate)";
            command = prefix + sb.ToString();

            return command;
        }



    }
    }
