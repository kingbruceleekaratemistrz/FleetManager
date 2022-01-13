using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetManager
{
    public partial class ChangePasswordMenu : Form
    {
        MainMenu mainMenu;
        byte[] token;
        string username = null;

        public ChangePasswordMenu()
        {
            InitializeComponent();
        }

        public ChangePasswordMenu(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();

            this.mainMenu = mainMenu;
            this.token = token;
        }

        public ChangePasswordMenu(byte[] token, MainMenu mainMenu, string username)
        {
            InitializeComponent();

            this.mainMenu = mainMenu;
            this.token = token;
            this.username = username;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Czy chcesz zapisać zmianę?", "FleetManager", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Cancel)
                return;
            else if (res == DialogResult.Yes)
            {
                bool updRes = (username == null) 
                    ? SqlConn.InsertIntoTableProcedure("PROC_UPDATE_USERS_CRED", "new_passwd", PasswordTextBox.Text, token) 
                    : SqlConn.InsertIntoTableProcedure("PROC_UPDATE_USERS_CRED", new string[] { "new_passwd", "input_username" }, new string[] { PasswordTextBox.Text, username }, token);
                if (updRes == false)
                {
                    MessageBox.Show("Nie udało się zmienić hasła.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                    mainMenu.ExitProgram();
                }
                else
                    MessageBox.Show("Zmiana hasła zakończona pomyślnie.");            
            }
            this.DialogResult = DialogResult.No;
        }

        private void ChangePasswordMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.No)
                return;
            else if (MessageBox.Show("Anulować zmianę hasła?", "FleetManager", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }
    }
}
