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
    public partial class CompaniesListControl : UserControl
    {
        MainMenu mainMenu;

        public CompaniesListControl()
        {
            InitializeComponent();
        }

        public CompaniesListControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.flowLayoutPanel1.AutoScroll = true;
            LoadCompaniesList(token);
        }

        private void LoadCompaniesList(byte[] token)
        {
            DataTable companies = SqlConn.GetTableProcedure("PROC_GET_COMPANIES_LIST", token);
            if (companies == null)
                mainMenu.ExitProgram();
            else
            {
                for (int i = 0; i < companies.Rows.Count; i++)
                {
                    Panel panel = new Panel()
                    {
                        Width = flowLayoutPanel1.Width - 25,
                        Height = 40,
                        BackColor = SystemColors.ControlLight,                     
                        Tag = companies.Rows[i]["name"].ToString()                       
                    };
                    panel.Click += new EventHandler(Panel_Click);

                    Label name = new Label()
                    {
                        Width = panel.Width - 6,
                        Height = 34,
                        Location = new Point(3, 3),
                        BackColor = SystemColors.GradientActiveCaption,
                        Font = new Font("Microsoft Sans Serif", 16),
                        Text = companies.Rows[i]["name"].ToString(),
                        Tag = companies.Rows[i]["name"].ToString()
                    };
                    name.Click += new EventHandler(Panel_Click);


                    panel.Controls.Add(name);
                    flowLayoutPanel1.Controls.Add(panel);
                }                
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            string name = "";
            if (sender is Panel)
                name = ((Panel)sender).Tag.ToString();
            else if (sender is Label)
                name = ((Label)sender).Tag.ToString();

            mainMenu.ShowCompanyProfile(name);
        }
    }
}
