using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace FleetManager
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void LoginBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Server=(LocalDb)\\AdmBD;Database=fleet_db;Trusted_Connection=True;"))
            {
                using (SqlCommand cmd = new SqlCommand("USERS_LOGIN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@input_username", SqlDbType.NVarChar).Value = this.UsernameBox.Text;
                    cmd.Parameters.Add("@input_passwd", SqlDbType.NVarChar).Value = this.PasswordBox.Text;

                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    con.Close();

                    int count = ds.Tables[0].Rows.Count;

                    if (count == 1)
                    {
                        MessageBox.Show("Login Successful!");
                    }
                    else
                    {
                        MessageBox.Show("Login Failed!");
                    }
                    
                }
            }

        }
    }
}
