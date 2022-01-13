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
    public partial class UserProfileControl : UserControl
    {
        MainMenu mainMenu;
        byte[] token;
        string username = null;
        
        public UserProfileControl()
        {
            InitializeComponent();
        }

        public UserProfileControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.token = token;
            
            DataTable profileTable = SqlConn.GetTableProcedure("PROC_GET_USER_PROFILE", token);
            if (profileTable == null)
                mainMenu.ExitProgram();
            else
            {
                this.FirstnameLabel.Text = profileTable.Rows[0]["first_name"].ToString();
                this.LastnameLabel.Text = profileTable.Rows[0]["last_name"].ToString();
                this.CompanyLabel.Text = profileTable.Rows[0]["company"].ToString();
                this.PositionLabel.Text = profileTable.Rows[0]["position"].ToString();
                this.PhoneLabel.Text = profileTable.Rows[0]["phone"].ToString();
                this.MailLabel.Text = profileTable.Rows[0]["mail"].ToString();
                this.CarLabel.Text = profileTable.Rows[0]["brand"].ToString() + ' ' + profileTable.Rows[0]["model"].ToString();
                this.ProfilePictureBox.Image = Image.FromFile(profileTable.Rows[0]["photo_url"].ToString());
            }

            DataTable acc = SqlConn.GetTableProcedure("PROC_GET_ACC", token);
            if ((int)acc.Rows[0][0] == -1)
                mainMenu.ExitProgram();
            else if ((int)acc.Rows[0][0] == 0)
            {
                EditionButton.Enabled = EditionButton.Visible = false;
                SaveButton.Enabled = SaveButton.Visible = false;
            }
            else
            {
                EditionButton.Enabled = EditionButton.Visible = true;
                SaveButton.Enabled = SaveButton.Visible = false;
            }

            ChangePasswordButton.Enabled = ChangePasswordButton.Visible = true;
        }

        public UserProfileControl(byte[] token, MainMenu mainMenu, string username)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.token = token;
            this.username = username;

            DataTable profileTable = SqlConn.GetTableProcedure("PROC_GET_USER_PROFILE", "input_username", username, token);
            if (profileTable == null)
                mainMenu.ExitProgram();
            else
            {
                this.FirstnameLabel.Text = profileTable.Rows[0]["first_name"].ToString();
                this.LastnameLabel.Text = profileTable.Rows[0]["last_name"].ToString();
                this.CompanyLabel.Text = profileTable.Rows[0]["company"].ToString();
                this.PositionLabel.Text = profileTable.Rows[0]["position"].ToString();
                this.PhoneLabel.Text = profileTable.Rows[0]["phone"].ToString();
                this.MailLabel.Text = profileTable.Rows[0]["mail"].ToString();
                this.CarLabel.Text = profileTable.Rows[0]["brand"].ToString() + ' ' + profileTable.Rows[0]["model"].ToString();
                this.ProfilePictureBox.Image = Image.FromFile(profileTable.Rows[0]["photo_url"].ToString());
            }

            DataTable acc = SqlConn.GetTableProcedure("PROC_GET_ACC", token);
            if ((int)acc.Rows[0][0] == -1)
                mainMenu.ExitProgram();
            else if ((int)acc.Rows[0][0] == 0)
            {
                EditionButton.Enabled = EditionButton.Visible = false;
                SaveButton.Enabled = SaveButton.Visible = false;
                ChangePasswordButton.Enabled = ChangePasswordButton.Visible = false;
            }
            else
            {
                EditionButton.Enabled = EditionButton.Visible = true;
                SaveButton.Enabled = SaveButton.Visible = false;
                ChangePasswordButton.Enabled = ChangePasswordButton.Visible = true;
            }
        }

        private void Reload()
        {
            DataTable profileTable = (username == null) 
                ? SqlConn.GetTableProcedure("PROC_GET_USER_PROFILE", token)
                : SqlConn.GetTableProcedure("PROC_GET_USER_PROFILE", "input_username", username, token);
            
            if (profileTable == null)  
                mainMenu.ExitProgram();
            else
            {
                this.FirstnameLabel.Text = profileTable.Rows[0]["first_name"].ToString();
                this.LastnameLabel.Text = profileTable.Rows[0]["last_name"].ToString();
                this.CompanyLabel.Text = profileTable.Rows[0]["company"].ToString();
                this.PositionLabel.Text = profileTable.Rows[0]["position"].ToString();
                this.PhoneLabel.Text = profileTable.Rows[0]["phone"].ToString();
                this.MailLabel.Text = profileTable.Rows[0]["mail"].ToString();
                this.CarLabel.Text = profileTable.Rows[0]["brand"].ToString() + ' ' + profileTable.Rows[0]["model"].ToString();
                this.ProfilePictureBox.Image = Image.FromFile(profileTable.Rows[0]["photo_url"].ToString());
            }
        }

        private void CarLabel_Click(object sender, EventArgs e)
        {
            if (CarLabel.ForeColor == SystemColors.ControlText)
            {
                if (username == null)
                    mainMenu.ShowCarProfile();
                else
                {
                    DataTable acc = SqlConn.GetTableProcedure("PROC_GET_ACC", token);
                    if ((int)acc.Rows[0][0] == -1)
                        mainMenu.ExitProgram();
                    else if ((int)acc.Rows[0][0] == 0)
                        MessageBox.Show("Nie masz uprawnień aby wyświetlić profil pojazdu innego użytkownika.");
                    else
                        mainMenu.ShowCarProfile(username);
                }
            }
        }

        #region Edycja/Zapis danych

        /// <summary>
        /// Przechodzi w tryb edycji danych.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditionButton_Click(object sender, EventArgs e)
        {
            HideControlsShowTextboxes();
        }

        /// <summary>
        /// Chowa kontrolki, których wartości mogą być edytowane i wstawia na ich miejsce textboxy.
        /// Nieedytowalne kontorlki i EditionButton wyszarza oraz wyświetla SaveButton.
        /// </summary>
        private void HideControlsShowTextboxes()
        {
            Label[] labelsToHide = { FirstnameLabel, LastnameLabel, PositionLabel, PhoneLabel, MailLabel };
            Label[] labelsToGreyOut = { CompanyLabel, CarLabel };
            TextBox textBox;

            // Schowanie labeli z labelsToHide i wstawienie na ich miejsce textBoxów
            foreach (Label lb in labelsToHide)
            {
                textBox = new TextBox
                {
                    Name = lb.Name + "Edit",
                    Location = lb.Location,
                    Width = 100,
                    Enabled = true,
                    Visible = true
                };
                lb.Enabled = lb.Visible = false;
                this.Controls.Add(textBox);
            }

            // Schowanie ProfilePictrueBox i wstawienie textBoxa
            textBox = new TextBox
            {
                Name = ProfilePictureBox.Name + "Edit",
                Location = ProfilePictureBox.Location,
                Width = 100
            };
            textBox.Enabled = textBox.Visible = true;
            ProfilePictureBox.Enabled = ProfilePictureBox.Visible = false;
            this.Controls.Add(textBox);

            // Wyszarzenie labeli z labelToGrayOut
            foreach (Label lb in labelsToGreyOut)            
                lb.ForeColor = Color.DimGray;            
           
            SaveButton.Enabled = SaveButton.Visible = true;
            EditionButton.Enabled = false;
        }

        /// <summary>
        /// Wyświetla MessageBox.YesNoCancel i odpowiednio: zapisuje zmiany na bazie, 
        /// odrzuca zmiany, pozostaje w edycji danych.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Czy chcesz zapisać zmiany?", "FleetManager", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Cancel)
                return;
            else if (res == DialogResult.Yes)
            {
                List<string> parName = new List<string>();
                List<string> parValue = new List<string>();

                // sprawdzanie textboxa First_name                
                string newFirstname = this.Controls[FirstnameLabel.Name + "Edit"].Text;
                if (newFirstname != "")
                {
                    foreach (char c in newFirstname.ToCharArray())
                    {
                        if (!Program.LegalChars.Contains(c))
                        {
                            MessageBox.Show("W imieniu zawarty jest niedozwolony znak: " + c + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }

                    parName.Add("new_first_name");
                    parValue.Add(newFirstname);
                }

                // sprawdzanie textboxa Lastname
                string newLastname = this.Controls[LastnameLabel.Name + "Edit"].Text;
                if (newLastname != "")
                {
                    foreach (char c in newLastname.ToCharArray())
                    {
                        if (!Program.LegalChars.Contains(c))
                        {
                            MessageBox.Show("W nazwisku zawarty jest niedozwolony znak: " + c + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }

                    parName.Add("new_last_name");
                    parValue.Add(newLastname);
                }

                // sprawdzanie textboxa position
                string newPosition = this.Controls[PositionLabel.Name + "Edit"].Text;
                if (newPosition != "")
                {
                    foreach (char c in newPosition.ToCharArray())
                    {
                        if (!Program.LegalChars.Contains(c))
                        {
                            MessageBox.Show("W stanowisku zawarty jest niedozwolony znak: " + c + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }

                    parName.Add("new_position");
                    parValue.Add(newPosition);
                }

                // sprawdzanie textboxa photo_url
                string newPhoto = this.Controls[ProfilePictureBox.Name + "Edit"].Text;
                if (newPhoto != "")
                {
                    foreach (char c in newPhoto.ToCharArray())
                    {
                        if (!Program.LegalChars.Contains(c))
                        {
                            MessageBox.Show("W ścieżce do zdjęcia zawarty jest niedozwolony znak: " + c + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }

                    parName.Add("new_photo_url");
                    parValue.Add(newPhoto);
                }

                // sprawdzanie textboxa Phone
                string newPhone = this.Controls[PhoneLabel.Name + "Edit"].Text;
                if (newPhone != "")
                {
                    if (newPhone.Length != 9)
                    {
                        MessageBox.Show("Numer telefonu powinien składać się z 9 liczb.\nZapis danych niemożliwy.");
                        return;
                    }

                    foreach (char c in newPhone.ToCharArray())
                    {
                        if (!Program.Digits.Contains(c))
                        {
                            MessageBox.Show("W numerze telefonu zawarty jest niedozwolony znak: " + c + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }

                    parName.Add("new_phone");
                    parValue.Add(newPhone);
                }

                // sprawdzanie textboxa Mail
                string newMail = this.Controls[MailLabel.Name + "Edit"].Text;
                if (newMail != "")
                {
                    foreach (char c in newMail.ToCharArray())
                    {
                        if (!Program.LegalChars.Contains(c))
                        {
                            MessageBox.Show("W adresie email zawarty jest niedozwolony znak: " + c + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }

                    parName.Add("new_mail");
                    parValue.Add(newMail);
                }

                // zapis na baze
                if (parName.Count != 0)
                {
                    bool updRes = SqlConn.InsertIntoTableProcedure("PROC_UPDATE_USERS_PROFILES", parName.ToArray(), parValue.ToArray(), token);                    
                    if (updRes == false)
                    {
                        MessageBox.Show("Nie udało się zaktualizować profilu użytkownika.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                        mainMenu.ExitProgram();
                    }
                    else
                        MessageBox.Show("Zmiana danych użytkownika zakończona pomyślnie.");
                }
            }

            HideTextboxesShowControls();
            Reload();
        }

        /// <summary>
        /// Chowa textboxy i wyświetla odpowiednie kontrolki.
        /// Przywraca wyszarzone kontrolki i EditionButton oraz chowa SaveButton.
        /// </summary>
        private void HideTextboxesShowControls()
        {
            Label[] labelsToShow = { FirstnameLabel, LastnameLabel, PositionLabel, PhoneLabel, MailLabel };
            Label[] labelsToEnable = { CompanyLabel, CarLabel };

            foreach (Label lb in labelsToShow)
            {
                this.Controls.Remove((TextBox)this.Controls[lb.Name + "Edit"]);
                lb.Enabled = lb.Visible = true;
            }

            foreach (Label lb in labelsToEnable)
                lb.ForeColor = SystemColors.ControlText;

            this.Controls.Remove((TextBox)this.Controls[ProfilePictureBox.Name + "Edit"]);
            ProfilePictureBox.Enabled = ProfilePictureBox.Visible = true;

            SaveButton.Enabled = SaveButton.Visible = false;
            EditionButton.Enabled = true;
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            ChangePasswordMenu changePasswordMenu = (username == null) ? new ChangePasswordMenu(token, mainMenu) : new ChangePasswordMenu(token, mainMenu, username);
            changePasswordMenu.ShowDialog();
        }

        #endregion
    }
}
