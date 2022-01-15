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
    public partial class CoworkersListControl : UserControl
    {
        MainMenu mainMenu;
        // przechowywanie listy usernamów wyświetlanych użytkowników
        string[] username;

        public CoworkersListControl()
        {
            InitializeComponent();
        }

        public CoworkersListControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.flowLayoutPanel1.AutoScroll = true;

            username = LoadCoworkersList(token);
        }

        private string[] LoadCoworkersList(byte[] token)
        {
            DataTable coworks = SqlConn.GetTableProcedure("PROC_GET_COWORKERS_LIST", token);
            if (coworks == null)
                mainMenu.ExitProgram();
            else
            {
                string[] username = new string[coworks.Rows.Count];

                for (int i = 0; i < coworks.Rows.Count; i++)
                {
                    username[i] = coworks.Rows[i]["username"].ToString();

                    // Label wyświetlający imię i nazwisko
                    Label name = new Label();
                    name.Name = i.ToString();
                    name.Text = coworks.Rows[i]["first_name"].ToString() + ' ' + coworks.Rows[i]["last_name"].ToString();
                    name.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Underline);
                    name.Width = flowLayoutPanel1.Width - 30;
                    name.Height += 5;
                    name.Click += new EventHandler(LabelClicked);
                    this.flowLayoutPanel1.Controls.Add(name);

                    // Label wyświetlający pozycję w firmie
                    Label position = new Label();
                    position.Text = coworks.Rows[i]["position"].ToString();
                    position.Font = new Font("Microsoft Sans Serif", 12);
                    position.Width = flowLayoutPanel1.Width - 30;
                    position.Height += 10;
                    position.Padding = new Padding(0, 0, 0, 10);
                    this.flowLayoutPanel1.Controls.Add(position);
                }

                // Zwrócenie listy nazw wyświetlanych użytkowników
                return username;
            }
            return null;
        }

        // Otworzenie profilu użytkownika wybranego z wyświetlanej listy
        private void LabelClicked(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            int i = Int32.Parse(lbl.Name);
            mainMenu.ShowUserProfile(username[i]);
        }
    }
}
