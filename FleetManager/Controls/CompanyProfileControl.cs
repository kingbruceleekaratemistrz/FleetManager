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
    public partial class CompanyProfileControl : UserControl
    {
        MainMenu mainMenu;
        byte[] token;
        string company = null;

        public CompanyProfileControl()
        {
            InitializeComponent();
        }

        public CompanyProfileControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.token = token;
            this.mainMenu = mainMenu;

            DataTable companyProfileTable = SqlConn.GetTableProcedure("PROC_GET_COMP_PROFILE", token);
            if (companyProfileTable == null)
                mainMenu.ExitProgram();
            else
            {
                string name = companyProfileTable.Rows[0]["name"].ToString();
                string description = companyProfileTable.Rows[0]["description"].ToString();
                string address = companyProfileTable.Rows[0]["address"].ToString();
                string phone = companyProfileTable.Rows[0]["phone"].ToString();
                string mail = companyProfileTable.Rows[0]["mail"].ToString();

                this.NameLabel.Text = name;
                this.DescriptionLabel.Text = description;
                this.AddressLabel.Text = address;
                this.PhoneLabel.Text = phone;
                this.MailLabel.Text = mail;
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
        }

        public CompanyProfileControl(byte[] token, string company, MainMenu mainMenu)
        {
            InitializeComponent();
            this.token = token;
            this.mainMenu = mainMenu;
            this.company = company;

            DataTable companyProfileTable = SqlConn.GetTableProcedure("PROC_GET_COMP_PROFILE", "input_name", company, token);
            if (companyProfileTable == null)
                mainMenu.ExitProgram();
            else
            {
                string name = companyProfileTable.Rows[0]["name"].ToString();
                string description = companyProfileTable.Rows[0]["description"].ToString();
                string address = companyProfileTable.Rows[0]["address"].ToString();
                string phone = companyProfileTable.Rows[0]["phone"].ToString();
                string mail = companyProfileTable.Rows[0]["mail"].ToString();

                this.NameLabel.Text = name;
                this.DescriptionLabel.Text = description;
                this.AddressLabel.Text = address;
                this.PhoneLabel.Text = phone;
                this.MailLabel.Text = mail;
            }


        }

        private void Reload()
        {
            DataTable companyProfileTable = SqlConn.GetTableProcedure("PROC_GET_COMP_PROFILE", token);
            if (companyProfileTable == null)
                mainMenu.ExitProgram();
            else
            {
                string name = companyProfileTable.Rows[0]["name"].ToString();
                string description = companyProfileTable.Rows[0]["description"].ToString();
                string address = companyProfileTable.Rows[0]["address"].ToString();
                string phone = companyProfileTable.Rows[0]["phone"].ToString();
                string mail = companyProfileTable.Rows[0]["mail"].ToString();

                this.NameLabel.Text = name;
                this.DescriptionLabel.Text = description;
                this.AddressLabel.Text = address;
                this.PhoneLabel.Text = phone;
                this.MailLabel.Text = mail;
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
            HideControlsShowTextBoxes();
        }

        /// <summary>
        /// Chowa kontrolki, których wartości mogą być edytowane i wstawia na ich miejsce textboxy.
        /// Nieedytowalne kontorlki i EditionButton wyszarza oraz wyświetla SaveButton.
        /// </summary>
        private void HideControlsShowTextBoxes()
        {
            Label[] labelsToHide = { DescriptionLabel, AddressLabel, PhoneLabel, MailLabel };
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

            // Wyszarzenie NameLabel
            NameLabel.ForeColor = Color.DimGray;

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

                // sprawdzanie textboxa Description                
                string newDescription = this.Controls[DescriptionLabel.Name + "Edit"].Text;
                if (newDescription != "")
                {
                    foreach (char c in newDescription.ToCharArray())
                    {
                        if (!Program.LegalChars.Contains(c))
                        {
                            MessageBox.Show("W opisie zawarty jest niedozwolony znak: " + c + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }

                    parName.Add("new_description");
                    parValue.Add(newDescription);
                }

                // sprawdzanie textboxa Address
                string newAddress = this.Controls[AddressLabel.Name + "Edit"].Text;
                if (newAddress != "")
                {
                    foreach (char c in newAddress.ToCharArray())
                    {
                        if (!Program.LegalChars.Contains(c))
                        {
                            MessageBox.Show("W adresie zawarty jest niedozwolony znak: " + c + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }

                    parName.Add("new_address");
                    parValue.Add(newAddress);
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

                //zapis na baze
                if (parName.Count != 0)
                {
                    bool updRes = SqlConn.InsertIntoTableProcedure("PROC_UPDATE_COMP_PROFILES", parName.ToArray(), parValue.ToArray(), token);                   
                    if (updRes == false)
                    {
                        MessageBox.Show("Nie udało się zaktualizować profilu firmy.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                        mainMenu.ExitProgram();
                    }
                    else
                        MessageBox.Show("Zmiana danych firmy zakończona pomyślnie.");
                }

            }
            //odtworzenie userprof
            HideTextboxesShowControls();
            Reload();
        }

        /// <summary>
        /// Chowa textboxy i wyświetla odpowiednie kontrolki.
        /// Przywraca wyszarzone kontrolki i EditionButton oraz chowa SaveButton.
        /// </summary>
        private void HideTextboxesShowControls()
        {
            Label[] labelsToShow = { DescriptionLabel, AddressLabel, PhoneLabel, MailLabel };

            // Wyświetla labele z labelsToShow
            foreach (Label lb in labelsToShow)
            {
                this.Controls.Remove((TextBox)this.Controls[lb.Name + "Edit"]);
                lb.Enabled = lb.Visible = true;
            }

            NameLabel.ForeColor = SystemColors.ControlText;

            SaveButton.Enabled = SaveButton.Visible = false;
            EditionButton.Enabled = true;
        }

        #endregion
    }
}
