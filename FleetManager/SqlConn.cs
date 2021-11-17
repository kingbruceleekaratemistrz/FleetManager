using System;
using System.Data;
using System.Data.SqlClient;

namespace FleetManager
{
    internal class SqlConn
    {
        private static string connectionString = "Server=(LocalDb)\\AdmBD;Database=fleet_db;Trusted_Connection=True;";

        public static byte[] CallLoginProcedure(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PROC_AUTHORIZE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@input_username", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@input_passwd", SqlDbType.NVarChar).Value = password;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if (table.Rows[0][0].ToString().CompareTo("0") == 0)
                        return null;

                    return (byte[])table.Rows[0][0];
                }
            }
        }

        public static DataTable GetTableProcedure(string procName, string parName, string parValue)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@" + parName, SqlDbType.NVarChar).Value = parValue;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    return table;
                }
            }
        }
    }
}