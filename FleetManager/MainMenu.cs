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
        private bool sessionOK;
        private byte[] token;
        private LoginMenu loginMenu;

        public MainMenu(byte[] token, LoginMenu loginMenu)
        {
            // token używany jest przy tworzeniu nowych kontrolek w celu autoryzacji sesji
            // loginMenu umożliwia powrót do menu logowania, lub zakończenie działanie programu
            this.sessionOK = true;
            this.token = token;
            this.loginMenu = loginMenu;

            InitializeComponent();

            // inicjalizacja
            DataTable dataTable = SqlConn.GetTableProcedure("PROC_GET_SHORT_USER_PROFILE", token);
            if (dataTable == null)
            {
                sessionOK = false;
                Application.Exit();            
            }
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
            if (sessionOK == true)
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
            else 
            {
                loginMenu.Close();
            }
        }

        #region Wyświetlanie kotrolek użytkownika

        // Wyświetla profil zalogowaego użytkownika 
        private void ShowUserProfile_Click(object sender, EventArgs e)
        {
            ShowUserProfile();
        }

        // Wyświetla profil firmy zalogowaego użytkownika
        private void ShowCompanyProfile_Click(object sender, EventArgs e)
        {
            ShowCompanyProfile();
        }

        // Wyświetla profil pojazdu zalogowaego użytkownika
        private void ShowCarProfile_Click(object sender, EventArgs e)
        {
            ShowCarProfile();
        }

        // Wyświetla listę pracowników z firmy zalogowaego użytkownika
        private void ShowCoworkersList_Click(object sender, EventArgs e)
        {
            CoworkersListControl ctrl = new CoworkersListControl(token, this);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }

        // Wyświetla listę pojazdów

        private void ShowCarServicesList(object sender, EventArgs e)
        {
            CarServicesListControl ctrl = new CarServicesListControl(token, this);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }

        private void ShowCarList_Click(object sender, EventArgs e)
        {
            CarsListControl ctrl = new CarsListControl(token, this);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }
        
        public void ShowUserProfile()
        {
            UserProfileControl ctrl = new UserProfileControl(token, this);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }

        public void ShowUserProfile(string username)
        {
            UserProfileControl ctrl = new UserProfileControl(token, this, username);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }

        public void ShowCompanyProfile()
        {
            CompanyProfileControl ctrl = new CompanyProfileControl(token, this);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }

        public void ShowCarProfile()
        {
            CarProfileControl ctrl = new CarProfileControl(token, this);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }

        public void ShowCarProfile(string username)
        {
            CarProfileControl ctrl = new CarProfileControl(token, this, username);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }

        public void ShowCarServiceProfile(int id)
        {
            CarServiceControl ctrl = new CarServiceControl(token, this, id);
            this.MainPanel.Controls.Clear();
            this.MainPanel.Controls.Add(ctrl);
        }

        #endregion


        public void ExitProgram()
        {
            sessionOK = false;
            this.Close();
        }

    }
}
