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
        
        public UserProfileControl()
        {
            InitializeComponent();
        }

        public UserProfileControl(byte[] token)
        {
            InitializeComponent();

            DataTable profileTable = SqlConn.GetTableProcedure("PROC_GET_USER_PROFILE", token);

            string firstName = profileTable.Rows[0]["first_name"].ToString();
            string lastName = profileTable.Rows[0]["last_name"].ToString();
            string company = profileTable.Rows[0]["company"].ToString();
            string position = profileTable.Rows[0]["position"].ToString();
            string phone = profileTable.Rows[0]["phone"].ToString();
            string mail = profileTable.Rows[0]["mail"].ToString();
            string car = profileTable.Rows[0]["car"].ToString();
            string picUrl = profileTable.Rows[0]["photo_url"].ToString();

            this.FirstnameLabel.Text = firstName;
            this.LastnameLabel.Text = lastName;
            this.CompanyLabel.Text = company;
            this.PositionLabel.Text = position;
            this.PhoneLabel.Text = phone;
            this.MailLabel.Text = mail;
            this.CarLabel.Text = car;
            this.ProfilePictureBox.Image = Image.FromFile(picUrl);
        }
    }
}
