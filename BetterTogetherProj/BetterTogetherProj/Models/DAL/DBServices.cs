using project_classes.Models;
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
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

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

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from questionnaire_P2";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
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
    }
}