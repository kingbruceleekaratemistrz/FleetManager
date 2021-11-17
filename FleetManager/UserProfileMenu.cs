using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FleetManager
{
    public partial class UserProfileMenu : Form
    {
        private string username;
        private string tokenString;

        public UserProfileMenu(string username, string tokenString)
        {
            this.username = username;
            InitializeComponent();
            DataTable table = SqlConn.GetTableProcedure("PROC_GET_USER_PROFILE", "input_username", username);

            string firstName = table.Rows[0]["first_name"].ToString();
            string lastName = table.Rows[0]["last_name"].ToString();
            string company = table.Rows[0]["company"].ToString();
            string picUrl = table.Rows[0]["photo_url"].ToString();

            this.FirstnameLabel.Text = firstName;
            this.LastnameLabel.Text = lastName;
            this.CompanyLabel.Text = company;
            this.ProfilePictureBox.Image = Image.FromFile(picUrl);
        }

        private void UserProfileMenu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Czy chcesz się wylogować?", "Fleet Manager", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
