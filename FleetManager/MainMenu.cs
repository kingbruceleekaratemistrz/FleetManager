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
            this.token = token;
            this.loginMenu = loginMenu;

            InitializeComponent();

            UserProfileControl userProfileControl = new UserProfileControl(token);
            this.mainPanel.Controls.Add(userProfileControl);

            /*UserProfileControl userProfileControl = new UserProfileControl(token);
            userProfileControl.Show();*/
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Czy chcesz się wylogować?", "Fleet Manager", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
            else
            {
                SqlConn.CallLogoutProcedure(token);
                if (MessageBox.Show("Czy chcesz wyjść z aplikacji?", "FleetManager", MessageBoxButtons.YesNo) == DialogResult.No)
                    loginMenu.Show();
                else
                    loginMenu.Close();
            }
        }
    }
}
