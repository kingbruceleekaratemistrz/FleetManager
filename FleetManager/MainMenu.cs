using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManager.Controls;

namespace FleetManager
{
    public partial class MainMenu : Form
    {
        private byte[] token;
        private LoginMenu loginMenu;

        public MainMenu(byte[] token, LoginMenu loginMenu)
        {
            // token używany jest przy tworzeniu nowych kontrolek w celu autoryzacji sesji
            // loginMenu umożliwia powrót do menu logowania, lub zakończenie działanie programu
            this.token = token;
            this.loginMenu = loginMenu;

            InitializeComponent();

            // inicjalizacja
            DataTable dataTable = SqlConn.GetTableProcedure("PROC_GET_SHORT_USER_PROFILE", token);
            FirstNameLabel.Text = dataTable.Rows[0]["first_name"].ToString();
            LastNameLabel.Text = dataTable.Rows[0]["last_name"].ToString();
            CompanyNameLabel.Text = dataTable.Rows[0]["company"].ToString();
            ProfilePictureBox.Image = Image.FromFile(dataTable.Rows[0]["photo_url"].ToString());
            ProfilePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            // pierwsza kontrolka pokazana po zalogowaniu -- docelowo MaiMenuControl
            // UserProfileControl userProfileControl = new UserProfileControl(token);
            // this.mainPanel.Controls.Add(userProfileControl);
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Czy chcesz się wylogować?", "Fleet Manager", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
            else
            {
                SqlConn.CallLogoutProcedure(token);
                if (MessageBox.Show("Czy chcesz wyjść z aplikacji?", "FleetManager", MessageBoxButtons.YesNo) == DialogResult.No)
                    loginMenu.Show(); // MainMenu zakończy działanie i LoginMenu pokaże się na ekranie
                else
                    loginMenu.Close(); // Zakończenie działanie całego programu
            }
        }

        private void ShowUserProfile_Click(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            UserProfileControl ctrl = new UserProfileControl(token);       
            this.MainPanel.Controls.Add(ctrl);
        }

        private void ShowCompanyProfile_Click(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            CompanyProfileControl ctrl = new CompanyProfileControl(token);
            this.MainPanel.Controls.Add(ctrl);
        }

        private void ShowCarProfile_Click(object sender, EventArgs e)
        {

        }

        private void ShowCoworkersList_Click(object sender, EventArgs e)
        {

        }

        private void ShowCarList_Click(object sender, EventArgs e)
        {

        }
    }
}
