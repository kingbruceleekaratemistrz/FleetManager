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
    public partial class UsersListControl : UserControl
    {
        MainMenu mainMenu;

        public UsersListControl()
        {
            InitializeComponent();
        }

        public UsersListControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.flowLayoutPanel1.AutoScroll = true;
            LoadUsersList(token);
        }

        private void LoadUsersList(byte[] token)
        {
            DataTable users = SqlConn.GetTableProcedure("PROC_GET_USERS_LIST", token);
            if (users == null)
                mainMenu.ExitProgram();
            else
            {
                for (int i = 0; i < users.Rows.Count; i++)
                {
                    string username = users.Rows[i]["username"].ToString();

                    Label name = new Label
                    {
                        Text = users.Rows[i]["first_name"].ToString() + ' ' + users.Rows[i]["last_name"].ToString(),
                        Font = new Font("Microsoft Sans Serif", 18, FontStyle.Underline),
                        Width = flowLayoutPanel1.Width - 30,
                        Tag = username
                    };
                    name.Click += new EventHandler(Panel_Click);
                    name.Height += 5;

                    Label company = new Label()
                    {
                        Text = users.Rows[i]["company"].ToString(),
                        Font = new Font("Microsoft Sans Serif", 12),
                        Width = flowLayoutPanel1.Width - 30,
                        Tag = username,
                        Padding = new Padding(0,0,0,10)
                    };
                    company.Click += new EventHandler(Panel_Click);
                    company.Height += 10;
                    

                    flowLayoutPanel1.Controls.Add(name);
                    flowLayoutPanel1.Controls.Add(company);
                }

                /*Color[] color = { Color.Red, Color.Blue, Color.Green, Color.Pink, Color.Purple, Color.Red, Color.Blue, Color.Green, Color.Pink, Color.Purple };
                int k = 0;
                for (int i = 0; i < users.Rows.Count; i++)
                {
                    string username = users.Rows[i]["username"].ToString();

                    Panel panel = new Panel();
                    panel.Width = flowLayoutPanel1.Width - 25;
                    panel.Height = 100;
                    panel.BackColor = color[k];
                    panel.Tag = username;
                    panel.Click += new EventHandler(Panel_Click);

                    Label fullName = new Label();
                    fullName.Width = panel.Width - 6;
                    fullName.Height = 50 - 6;
                    fullName.Location = new Point(3, 3);
                    fullName.BackColor = Color.Goldenrod;
                    fullName.Text = users.Rows[i]["first_name"].ToString() + users.Rows[i]["last_name"].ToString();
                    fullName.Tag = username;
                    fullName.Click += new EventHandler(Panel_Click);

                    Label company = new Label();
                    company.Width = panel.Width - 6;
                    company.Height = 50 - 6;
                    company.Location = new Point(3, 53);
                    company.BackColor = Color.Goldenrod;
                    company.Text = users.Rows[i]["company"].ToString();
                    company.Tag = username;
                    company.Click += new EventHandler(Panel_Click);

                    panel.Controls.Add(fullName);
                    panel.Controls.Add(company);
                    flowLayoutPanel1.Controls.Add(panel);

                    k += (k == 9) ? -9 : 1;
                }*/
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            string username = "";
            if (sender is Panel)
                username = ((Panel)sender).Tag.ToString();
            else if (sender is Label)
                username = ((Label)sender).Tag.ToString();

            mainMenu.ShowUserProfile(username);
        }
    }
}
