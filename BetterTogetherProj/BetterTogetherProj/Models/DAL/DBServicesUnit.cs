using System;
using System.Collections.Generic;
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
    }
}