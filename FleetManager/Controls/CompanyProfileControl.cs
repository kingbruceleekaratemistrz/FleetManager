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
    public partial class CompanyProfileControl : UserControl
    {
        public CompanyProfileControl()
        {
            InitializeComponent();
        }

        public CompanyProfileControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();

            DataTable companyProfileTable = SqlConn.GetTableProcedure("PROC_GET_COMP_PROFILE", token);
            if (companyProfileTable == null)
                mainMenu.ExitProgram();
            else
            {
                string name = companyProfileTable.Rows[0]["name"].ToString();
                string description = companyProfileTable.Rows[0]["description"].ToString();
                string address = companyProfileTable.Rows[0]["address"].ToString();
                string phone = companyProfileTable.Rows[0]["phone"].ToString();
                string mail = companyProfileTable.Rows[0]["mail"].ToString();

                this.NameLabel.Text = name;
                this.DescriptionLabel.Text = description;
                this.AddressLabel.Text = address;
                this.PhoneLabel.Text = phone;
                this.MailLabel.Text = mail;
            }
        }

        public CompanyProfileControl(byte[] token, string company, MainMenu mainMenu)
        {
            InitializeComponent();

            DataTable companyProfileTable = SqlConn.GetTableProcedure("PROC_GET_COMP_PROFILE", "input_name", company, token);
            if (companyProfileTable == null)
                mainMenu.ExitProgram();
            else
            {
                string name = companyProfileTable.Rows[0]["name"].ToString();
                string description = companyProfileTable.Rows[0]["description"].ToString();
                string address = companyProfileTable.Rows[0]["address"].ToString();
                string phone = companyProfileTable.Rows[0]["phone"].ToString();
                string mail = companyProfileTable.Rows[0]["mail"].ToString();

                this.NameLabel.Text = name;
                this.DescriptionLabel.Text = description;
                this.AddressLabel.Text = address;
                this.PhoneLabel.Text = phone;
                this.MailLabel.Text = mail;
            }
        }
    }
}
