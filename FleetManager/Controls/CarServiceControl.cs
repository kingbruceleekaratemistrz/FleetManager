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
    public partial class CarServiceControl : UserControl
    {        
        MainMenu mainMenu;
        byte[] token;
        string carServiceName;
        int id;

        public CarServiceControl()
        {
            InitializeComponent();
        }
    
        public CarServiceControl(byte[] token, MainMenu mainMenu, int id)
        {
            InitializeComponent();
            this.token = token;
            this.id = id;
            this.mainMenu = mainMenu;

            DataTable carServiceTable = SqlConn.GetTableProcedure("PROC_GET_CAR_SERVICE", "input_id", id, token);
            if (carServiceTable == null)
                mainMenu.ExitProgram();
            else
            {
                this.NameLabel.Text = carServiceName = carServiceTable.Rows[0]["name"].ToString();
                this.AddressLabel.Text = carServiceTable.Rows[0]["address"].ToString();
                this.PhoneLabel.Text = carServiceTable.Rows[0]["phone"].ToString();
                this.MailLabel.Text = carServiceTable.Rows[0]["mail"].ToString();
            }

            DataTable acc = SqlConn.GetTableProcedure("PROC_GET_ACC", token);
            if ((int)acc.Rows[0][0] == -1)
                mainMenu.ExitProgram();
            else if ((int)acc.Rows[0][0] == 2)            
                EditionButton.Enabled = EditionButton.Visible = true;             
            else            
                EditionButton.Enabled = EditionButton.Visible = false;
                        
            SaveButton.Enabled = SaveButton.Visible = false;
        }

        private void Reload()
        {
            DataTable carServiceTable = SqlConn.GetTableProcedure("PROC_GET_CAR_SERVICE", "input_id", id, token);
            if (carServiceTable == null)
                mainMenu.ExitProgram();
            else
            {
                this.NameLabel.Text = carServiceName = carServiceTable.Rows[0]["name"].ToString();
                this.AddressLabel.Text = carServiceTable.Rows[0]["address"].ToString();
                this.PhoneLabel.Text = carServiceTable.Rows[0]["phone"].ToString();
                this.MailLabel.Text = carServiceTable.Rows[0]["mail"].ToString();
            }
        }

        #region Edycja/Zapis danych

        private void RegisterVisitButton_Click(object sender, EventArgs e)
        {
            RegisterVisitMenu registerVisitMenu = new RegisterVisitMenu(token, carServiceName, id, mainMenu);
            registerVisitMenu.ShowDialog();
        }

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
            Label[] labelsToHide = { AddressLabel, PhoneLabel, MailLabel };
            string[] labelTexts = { "Adres warsztatu:", "Nr telefonu:", "Adres email:" };

            // Schowanie labeli z labelsToHide i wstawienie na ich miejsce textBoxów
            int k = 0;
            foreach (Label lb in labelsToHide)
            {                                
                Label label = new Label()
                {
                    Name = lb.Name + "EditLab",
                    Location = new Point(lb.Location.X - 100, lb.Location.Y),
                    Width = 100,
                    Text = labelTexts[k++],
                    Enabled = true,
                    Visible = true
                };

                TextBox textBox = new TextBox
                {
                    Name = lb.Name + "Edit",
                    Location = lb.Location,
                    Width = 100,
                    Enabled = true,
                    Visible = true
                };

                lb.Enabled = lb.Visible = false;
                this.Controls.Add(label);
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

                parName.Add("input_car_service");
                parValue.Add(carServiceName);

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
                    bool updRes = SqlConn.InsertIntoTableProcedure("PROC_UPDATE_CAR_SERVICES", parName.ToArray(), parValue.ToArray(), token);
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
            Label[] labelsToShow = { AddressLabel, PhoneLabel, MailLabel };

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
