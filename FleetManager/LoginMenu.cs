using System;
using System.Data;
using System.Windows.Forms;

namespace FleetManager
{
    public partial class LoginMenu : Form
    {

        public LoginMenu()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = this.UsernameBox.Text;
            string password = this.PasswordBox.Text;
            
            // Sprawdzenie czy podane łańcuchy nie są za długie
            if (AreCredentialsToLong())
                return;

            // Jeśli token == null to znaczy że dane werfikacyjne były niepoprawne
            byte[] token = SqlConn.CallLoginProcedure(username, password);
            if (token == null)
                MessageBox.Show("Podano błędny login lub hasło!");
            else
            {
                string tokenString = BitConverter.ToString(token).Replace("-", string.Empty);

                MessageBox.Show("Logowanie zakończone sukcesem!");
                this.Hide();

                UserProfileMenu profileMenu = new UserProfileMenu(username, tokenString);
                profileMenu.Show();
            }          
        }

        private bool AreCredentialsToLong()
        {
            if (this.PasswordBox.TextLength > 30 || this.PasswordBox.TextLength > 30)
            {
                MessageBox.Show("Maksymalna długość nazwy użytkownika oraz hasła to 30 znaków.", "Błąd Logowania!");
                return true;
            }
            return false;
        }
    }
}
