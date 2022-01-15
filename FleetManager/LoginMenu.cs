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
            this.UsernameBox.Text = "dulnikip";
            this.PasswordBox.Text = "123";
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = this.UsernameBox.Text;
            string password = this.PasswordBox.Text;
            
            // Sprawdzenie czy podane łańcuchy nie są za długie
            if (AreCredentialsToLong())
                return;
           
            // Token autoryzujący weryfikowany przy wywoływainu procedur na bazie
            byte[] token = SqlConn.CallLoginProcedure(username, password);

            // Jeśli token == null to znaczy że dane werfikacyjne były niepoprawne
            if (token == null)
                MessageBox.Show("Podano błędny login lub hasło!");
            else
            {
 
                // string tokenString = BitConverter.ToString(token).Replace("-", string.Empty);

                MessageBox.Show("Logowanie zakończone sukcesem!");
                
                // Usuwa wartości textboxów, aby można było wrócić do LoginMenu po wylogowaniu.
                this.UsernameBox.Text = string.Empty;
                this.PasswordBox.Text = string.Empty;
                this.Hide();

                // Tworzy MainMenu w którym wyświetlane będą kontrolki użytkownika (profile, listy itp.)
                /*UserProfileMenu profileMenu = new UserProfileMenu(username, token, this);                
                profileMenu.ShowDialog();*/
                MainMenu mainMenu = new MainMenu(token, this);
                mainMenu.Show();
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
