using System;
using System.Data;
using System.Data.SqlClient;

namespace FleetManager
{
    internal class SqlConn
    {
        private static string connectionString = "Server=(LocalDb)\\AdmBD;Database=fleet_db;Trusted_Connection=True;";

        #region Procedury logowania/wylogowania

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

        public static void CallLogoutProcedure(byte[] token)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PROC_CLOSE_SESSION", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@token", SqlDbType.VarBinary).Value = token;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Procedury zwracające tabelę danych

        public static DataTable GetTableProcedure(string procName, string parName, string parValue, byte[] token)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@token", SqlDbType.VarBinary).Value = token;
                    cmd.Parameters.Add("@" + parName, SqlDbType.NVarChar).Value = parValue;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    return table;
                }
            }
        }

        public static DataTable GetTableProcedure(string procName, byte[] token)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@token", SqlDbType.VarBinary).Value = token;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    return table;
                }
            }
        }

        #endregion 
    }
}