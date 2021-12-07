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
        public enum MyControls
        {
            UserProfileControl,
            CompanyProfileControl,
            CarProfileControl
        }

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

        #region Wyświetlanie kotrolek użytkownika

        // Wyświetla profil zalogowaego użytkownika 
        private void ShowUserProfile_Click(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            UserProfileControl ctrl = new UserProfileControl(token, this);
            this.MainPanel.Controls.Add(ctrl);
        }

        // Wyświetla profil firmy zalogowaego użytkownika
        private void ShowCompanyProfile_Click(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            CompanyProfileControl ctrl = new CompanyProfileControl(token);
            this.MainPanel.Controls.Add(ctrl);
        }

        // Wyświetla profil pojazdu zalogowaego użytkownika
        private void ShowCarProfile_Click(object sender, EventArgs e)
        {
            this.MainPanel.Controls.Clear();
            CarProfileControl ctrl = new CarProfileControl(token);
            this.MainPanel.Controls.Add(ctrl);
        }

        // Wyświetla listę pracowników z firmy zalogowaego użytkownika
        private void ShowCoworkersList_Click(object sender, EventArgs e)
        {

        }

        // Wyświetla listę pojazdów
        private void ShowCarList_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Zmienia obecnie wyświetlaną kotrolkę użytkownika w oparciu o parametr mc
        /// </summary>
        /// <param name="mc">Typ wyliczeniowy, odpowiadający kontrolką użytkownika w programie.</param>
        public void ChangeControl(MyControls mc)
        {
            switch(mc)
            {
                case (MyControls)0:
                    ShowCarProfile_Click(null, null);
                    break;
                case (MyControls)1:
                    ShowCompanyProfile_Click(null, null);
                    break;
                case (MyControls)2:
                    ShowCarProfile_Click(null, null);
                    break;
            }
        }

        #endregion

    }
}
