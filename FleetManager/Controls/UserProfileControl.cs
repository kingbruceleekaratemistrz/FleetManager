using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetManager.Controls
{
    public partial class UserProfileControl : UserControl
    {
        MainMenu mainMenu;
        
        public UserProfileControl()
        {
            InitializeComponent();
        }

        public UserProfileControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;

            DataTable profileTable = SqlConn.GetTableProcedure("PROC_GET_USER_PROFILE", token);
            if (profileTable == null)
                mainMenu.ExitProgram();
            else
            {
                this.FirstnameLabel.Text = profileTable.Rows[0]["first_name"].ToString();
                this.LastnameLabel.Text = profileTable.Rows[0]["last_name"].ToString();
                this.CompanyLabel.Text = profileTable.Rows[0]["company"].ToString();
                this.PositionLabel.Text = profileTable.Rows[0]["position"].ToString();
                this.PhoneLabel.Text = profileTable.Rows[0]["phone"].ToString();
                this.MailLabel.Text = profileTable.Rows[0]["mail"].ToString();
                this.CarLabel.Text = profileTable.Rows[0]["brand"].ToString() + ' ' + profileTable.Rows[0]["model"].ToString();
                this.ProfilePictureBox.Image = Image.FromFile(profileTable.Rows[0]["photo_url"].ToString());
            }
        }

        public UserProfileControl(byte[] token, MainMenu mainMenu, string username)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;

            DataTable profileTable = SqlConn.GetTableProcedure("PROC_GET_USER_PROFILE", "input_username", username, token);
            if (profileTable == null)
                mainMenu.ExitProgram();
            else
            {
                this.FirstnameLabel.Text = profileTable.Rows[0]["first_name"].ToString();
                this.LastnameLabel.Text = profileTable.Rows[0]["last_name"].ToString();
                this.CompanyLabel.Text = profileTable.Rows[0]["company"].ToString();
                this.PositionLabel.Text = profileTable.Rows[0]["position"].ToString();
                this.PhoneLabel.Text = profileTable.Rows[0]["phone"].ToString();
                this.MailLabel.Text = profileTable.Rows[0]["mail"].ToString();
                this.CarLabel.Text = profileTable.Rows[0]["brand"].ToString() + ' ' + profileTable.Rows[0]["model"].ToString();
                this.ProfilePictureBox.Image = Image.FromFile(profileTable.Rows[0]["photo_url"].ToString());
            }
        }

        private void CarLabel_Click(object sender, EventArgs e)
        {
            this.mainMenu.ShowCarProfile();
        }
    }
}
