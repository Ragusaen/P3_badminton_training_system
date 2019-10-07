using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.DAL
{
    class DBConnection
    {
        private string connString;

        public DBConnection()
        {
            connString = ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;
        }

        /// <summary>
        /// Executes a select query and returns a DataTable with the results.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        public DataTable ExecuteSelectQuery(string query, SqlParameter[] sqlParameters)
        {
            DataTable dt = null;
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddRange(sqlParameters);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }catch(SqlException e)
                {
                    Console.WriteLine("Error ExecuteSelectQuery - " + e.Message);
                }
            }
            return dt;
        }

        /// <summary>
        /// Executes an insert/update/delete query and returns true if successful.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public bool ExecuteInsertUpdateDeleteQuery(string query, SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddRange(sqlParameters);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }catch(SqlException e)
                {
                    Console.WriteLine("Error ExecuteInsertUpdateDeleteQuery - " + e.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
