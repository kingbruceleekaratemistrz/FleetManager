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
    public partial class AddDataMenu : Form
    {
        MainMenu mainMenu;
        byte[] token;

        Dictionary<int, string> car_name = new Dictionary<int, string>();
        List<string> company_name = new List<string>();

        public AddDataMenu()
        {
            InitializeComponent();
        }

        public AddDataMenu(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.token = token;

            GetPrimaryKeys();

            TableSelectionComboBox.Items.Add("PROC_INSERT_USER");
            TableSelectionComboBox.Items.Add("PROC_INSERT_COMPANY");
            TableSelectionComboBox.Items.Add("PROC_INSERT_CAR");
            TableSelectionComboBox.Items.Add("PROC_INSERT_CAR_SERVICE");
            TableSelectionComboBox.Items.Add("PROC_INSERT_SERVICE");            
        }

        /// <summary>
        /// Pobiera klucze główne z CARS oraz COMP_PROFILES.
        /// </summary>
        private void GetPrimaryKeys()
        {
            DataTable cars = SqlConn.GetTableProcedure("PROC_GET_CARS_LIST", token);
            if (cars == null)
                mainMenu.ExitProgram();
            
            for (int i = 0; i < cars.Rows.Count; i++)
            {
                int id = (int)cars.Rows[i]["car_id"];

                string brand = cars.Rows[i]["brand"].ToString();
                string model = cars.Rows[i]["model"].ToString();
                double cc = ((double) ((int)cars.Rows[i]["cc"])) / 1000;
                string hp = cars.Rows[i]["hp"].ToString();
                string prod_year = cars.Rows[i]["prod_year"].ToString();
                string val = brand + " " + model + " " + string.Format("{0:F1}", cc) + " " + hp + " KM " + prod_year + " r.";

                car_name.Add(id, val);
            }

            DataTable companies = SqlConn.GetTableProcedure("PROC_GET_COMPANIES_LIST", token);
            if (companies == null)
                mainMenu.ExitProgram();

            for (int i = 0; i < companies.Rows.Count; i++)
                company_name.Add(companies.Rows[i]["name"].ToString());
        }

        private void TableSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlPanel.Controls.Clear();
            switch (TableSelectionComboBox.SelectedItem)
            {
                case "PROC_INSERT_USER":
                    LoadUserInsert();
                    break;

                case "PROC_INSERT_COMPANY":
                    LoadCompanyInsert();
                    break;

                case "PROC_INSERT_CAR":
                    LoadCarInsert();
                    break;

                case "PROC_INSERT_CAR_SERVICE":
                    LoadCarServiceInsert();
                    break;

                case "PROC_INSERT_SERVICE":
                    LoadServiceInsert();
                    break;

                default:
                    break;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            switch (TableSelectionComboBox.SelectedItem)
            {
                case "PROC_INSERT_USER":
                    SaveUserInsert();
                    break;

                case "PROC_INSERT_COMPANY":
                    SaveCompanyInsert();
                    break;

                case "PROC_INSERT_CAR":
                    SaveCarInsert();
                    break;

                case "PROC_INSERT_CAR_SERVICE":
                    SaveCarServiceInsert();
                    break;

                case "PROC_INSERT_SERVICE":
                    SaveServiceInsert();
                    break;

                default:
                    break;
            }
        }

        #region Wczytywanie kontrolek do wpisywania danych

        /// <summary>
        /// Wyświetla Labele TextBoxy umożliwiające wpisanie danych nowego użytkownika.
        /// Dla parametrów które są kluczami obcymi, pobiera wartości z bazy i dodaje do odpowiedniego ComboBoxa,
        /// z którego można je wybrać.
        /// </summary>
        private void LoadUserInsert()
        {
                    
            string[] labelTexts = { "nazwa użytkownika:", "hasło:", "imię:", "nazwisko:", "nazwa firmy:", "stanowisko:", "zdjęcie:", "nr telefonu:", "adres email:", "pojazd:", "rejestracja:" };
            for (int i = 0; i < labelTexts.Length; i++)
            {
                Label label = new Label
                {
                    Name = "lab" + i,
                    Text = labelTexts[i],
                    Width = 100,
                    Location = new Point(0, 30 * i)
                };
                ControlPanel.Controls.Add(label);

                if (i == 4)
                {
                    ComboBox comboBox = new ComboBox
                    {
                        Name = "ctrl4",
                        Width = 250,
                        Location = new Point(100, 30 * i),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };

                    for (int j = 0; j < company_name.Count; j++)
                        comboBox.Items.Add(company_name[j]);

                    ControlPanel.Controls.Add(comboBox);
                }
                else if (i == 9)
                {
                    ComboBox comboBox = new ComboBox
                    {
                        Name = "ctrl9",
                        Width = 250,
                        Location = new Point(100, 30 * i),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };

                    for (int j = 0; j < car_name.Count; j++)
                        comboBox.Items.Add(car_name[j + 1]);

                    ControlPanel.Controls.Add(comboBox);
                }
                else
                {
                    TextBox textBox = new TextBox
                    {
                        Name = "ctrl" + i,
                        Width = 250,
                        Location = new Point(100, 30 * i)
                    };
                    ControlPanel.Controls.Add(textBox);
                }                              
            }                    
        }

        /// <summary>
        /// Wyświetla Labele i TextBoxy umożliwiające wpisanie danych nowej firmy.
        /// </summary>
        private void LoadCompanyInsert()
        {
            string[] labelTexts = { "nazwa firmy:", "opis firmy:", "adres:", "nr telefonu:", "adres email:" };
            for (int i = 0; i < labelTexts.Length; i++)
            {
                Label label = new Label
                {
                    Name = "lab" + i,
                    Text = labelTexts[i],
                    Width = 100,
                    Location = new Point(0, 30 * i)
                };
                ControlPanel.Controls.Add(label);

                TextBox textBox = new TextBox
                {
                    Name = "ctrl" + i,
                    Width = 250,
                    Location = new Point(100, 30 * i)
                };
                ControlPanel.Controls.Add(textBox);                               
            }
        }

        /// <summary>
        /// Wyświetla Labele i TextBoxy umożliwiające wpisanie danych nowego pojazdu.
        /// </summary>
        private void LoadCarInsert()
        {
            string[] labelTexts = { "marka pojazdu:", "model:", "rok produkcji:", "moc [KM]:", "pojemność [cm\xB3]:" };
            for (int i = 0; i < labelTexts.Length; i++)
            {
                Label label = new Label
                {
                    Name = "lab" + i,
                    Text = labelTexts[i],
                    Width = 100,
                    Location = new Point(0, 30 * i)
                };
                ControlPanel.Controls.Add(label);

                TextBox textBox = new TextBox
                {
                    Name = "ctrl" + i,
                    Width = 250,
                    Location = new Point(100, 30 * i)
                };
                ControlPanel.Controls.Add(textBox);
            }
        }

        /// <summary>
        /// Wyświetla Labele i TextBoxy umożliwiające wpisanie danych nowego warsztatu.
        /// </summary>
        private void LoadCarServiceInsert()
        {
            string[] labelTexts = { "nazwa warsztatu:", "adres:", "nr telefonu:", "adres email:" };
            for (int i = 0; i < labelTexts.Length; i++)
            {
                Label label = new Label
                {
                    Name = "lab" + i,
                    Text = labelTexts[i],
                    Width = 100,
                    Location = new Point(0, 30 * i)
                };
                ControlPanel.Controls.Add(label);

                TextBox textBox = new TextBox
                {
                    Name = "ctrl" + i,
                    Width = 250,
                    Location = new Point(100, 30 * i)
                };
                ControlPanel.Controls.Add(textBox);
            }
        }

        /// <summary>
        /// Wyświetla Labele i TextBoxy umożliwiające wpisanie danych nowej usługi.
        /// </summary>
        private void LoadServiceInsert()
        {
            string[] labelTexts = { "koszt:", "czas wykonania:", "opis usługi:", };
            for (int i = 0; i < labelTexts.Length; i++)
            {
                Label label = new Label
                {
                    Name = "lab" + i,
                    Text = labelTexts[i],
                    Width = 100,
                    Location = new Point(0, 30 * i)
                };
                ControlPanel.Controls.Add(label);

                TextBox textBox = new TextBox
                {
                    Name = "ctrl" + i,
                    Width = 250,
                    Location = new Point(100, 30 * i)
                };
                ControlPanel.Controls.Add(textBox);
            }
        }

        #endregion

        #region Sprawdzenie danych i zapis na bazie        

        /// <summary>
        /// Sprawdza dane nowego użytkownika, jeśli są poprawne dodaje dane logowania do USERS_CRED 
        /// i dane profilu do USERS_PROFILES.
        /// </summary>
        private void SaveUserInsert()
        {
            string[] parName1 = { "username", "password", "acc" };            

            // wywołanie procedury dodania do USERS_CREDS
            // zapisanie username, password, acc w tablicy parValue
            string[] parValue1 = new string[3];
            for (int i = 0; i < 2; i++)
                parValue1[i] = ControlPanel.Controls["ctrl" + i].Text;
            parValue1[2] = "0";

            // sprawdzenie poprawności wartości parValue
            foreach (string str in parValue1)
            {
                if (str != "")
                {
                    foreach (char ch in str)
                    {
                        if (!Program.LegalChars.Contains(ch))
                        {
                            MessageBox.Show("Użyto niedozwolonego znaku: " + ch + "\nZapis danych niemożliwy.");
                            return;
                        }                        
                    }
                }
                else
                {
                    MessageBox.Show("Należy przypisać wartość do każdego z parametrów.\nZapis danych niemożliwy.");
                    return;
                }
            }

            // wywołanie procedury dodania do USERS_PROFILES
            string[] parName2 = { "username", "first_name", "last_name", "company", "position", "photo_url", "phone", "mail", "car_id", "car_plate" };
            string[] parValue2 = new string[10];
            
            // zapisanie username, sprawdzone wcześniej
            parValue2[0] = parValue1[0];

            // zapisanie i sprawdzenie parametrów od first_name do mail włącznie
            string tmpVal;
            for (int i = 1; i < 8; i++)
            {
                tmpVal = ControlPanel.Controls["ctrl" + (i + 1)].Text;
                if (tmpVal != "")
                {
                    foreach (char ch in tmpVal)
                    {
                        if (!Program.LegalChars.Contains(ch))
                        {
                            MessageBox.Show("Użyto niedozwolonego znaku: " + ch + "\nZapis danych niemożliwy.");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Należy przypisać wartość do każdego z parametrów.\nZapis danych niemożliwy.");
                    return;
                }

                parValue2[i] = tmpVal;
            }

            // zapisanie i sprawdzenie parametru car_id
            int tmpCarId = Int32.Parse((((ComboBox)ControlPanel.Controls["ctrl9"]).SelectedIndex + 1).ToString());
            if (tmpCarId == 0)
            {
                MessageBox.Show("Nie wybrano pojazdu z listy pojazdów.\nZapis danych niemożliwy.");
                return;
            }
            parValue2[8] = tmpCarId.ToString();

            // zapisanie i sprawdzenie parametru car_plate
            // todo: pobrac liste uzytych car_plate i je porównać z nowym, ponieważ car_plate to klucz główny USERS_PROFILES
            tmpVal = ControlPanel.Controls["ctrl10"].Text;
            if (tmpVal != "")
            {
                foreach (char ch in tmpVal)
                {
                    if (!Program.LegalChars.Contains(ch))
                    {
                        MessageBox.Show("Użyto niedozwolonego znaku: " + ch + "\nZapis danych niemożliwy.");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Należy przypisać wartość do każdego z parametrów.\nZapis danych niemożliwy.");
                return;
            }
            parValue2[9] = tmpVal;

            // procedura zapisujące dane logowania do USERS_CRED
            bool res = SqlConn.InsertIntoTableProcedure("PROC_INSERT_USERS_CRED", parName1, parValue1, token);
            if (res == false)
            {
                MessageBox.Show("Nie udało się zapisać nowego użytkownika.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                mainMenu.ExitProgram();
            }
            else
                MessageBox.Show("Pomyślnie zapisano dane logowania użytkownika.");

            res = SqlConn.InsertIntoTableProcedure("PROC_INSERT_USERS_PROFILES", parName2, parValue2, token);
            if (res == false)
            {
                MessageBox.Show("Nie udało się zapisać nowego użytkownika.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                // usuwanie wcześniej dodanych danych logowania
                res = SqlConn.DeleteFromTableProcedure("PROC_DELETE_USERS_CRED", parName1[0], parValue1[0], token);
                if (res == false)
                    MessageBox.Show("Nie udało się usunąć danych logowania użytkownika. Skontaktuj się z administratorem.");

                mainMenu.ExitProgram();
            }
            else
                MessageBox.Show("Pomyślnie zapisano dane profilu użytkownika.");

            // zakończenie okna dialogowego
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sprawdza dane nowej firmy, jeśli są poprawne dodaje dane do COMP_PROFILES.         
        /// </summary>
        private void SaveCompanyInsert()
        {
            string[] parName = { "name", "description", "address", "phone", "mail" };
            string[] parValue = new string[5];

            // zapisanie wartości z textboxów do zmiennej
            for (int i = 0; i < 5; i++)
                parValue[i] = ControlPanel.Controls["ctrl" + i].Text;

            // sprawdzenie zapisanych wartości
            if (!checkString(parValue[0]))
                mainMenu.ExitProgram();
            if (!checkString(parValue[1]))
                mainMenu.ExitProgram();
            if (!checkString(parValue[2]))
                mainMenu.ExitProgram();
            if (!checkPhone(parValue[3]))
                mainMenu.ExitProgram();
            if (!checkString(parValue[4]))
                mainMenu.ExitProgram();

            // zapis na bazie
            bool res = SqlConn.InsertIntoTableProcedure("PROC_INSERT_COMP_PROFILES", parName, parValue, token);
            if (res == false)
            {
                MessageBox.Show("Nie udało się zapisać nowej firmy.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                mainMenu.ExitProgram();
            }
            else
                MessageBox.Show("Pomyślnie zapisano dane profilu firmy.");

            // zakończenie okna dialogowego
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sprawdza dane nowego pojazdu, jeśli są poprawne dodaje dane do CARS.         
        /// </summary>
        private void SaveCarInsert()
        {
            string[] parName = { "brand", "model", "prod_year", "hp", "cc", "photo_url" };
            string[] parValue = new string[6];
            parValue[5] = "url not found";

            // zapisanie wartości z textboxów do zmiennej
            for (int i = 0; i < 5; i++)
                parValue[i] = ControlPanel.Controls["ctrl" + i].Text;

            // sprawdzenie zapisanych wartości
            if (!checkString(parValue[0]))
                mainMenu.ExitProgram();
            if (!checkString(parValue[1]))
                mainMenu.ExitProgram();
            if (!checkInt(parValue[2]))
                mainMenu.ExitProgram();
            if (!checkInt(parValue[3]))
                mainMenu.ExitProgram();
            if (!checkInt(parValue[4]))
                mainMenu.ExitProgram();

            // zapis na bazie
            bool res = SqlConn.InsertIntoTableProcedure("PROC_INSERT_CARS", parName, parValue, token);
            if (res == false)
            {
                MessageBox.Show("Nie udało się zapisać nowego pojazdu.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                mainMenu.ExitProgram();
            }
            else
                MessageBox.Show("Pomyślnie zapisano dane profilu pojazdu.");

            // zakończenie okna dialogowego
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sprawdza dane nowego warsztatu, jeśli są poprawne dodaje dane do CAR_SERVICES.         
        /// </summary>
        private void SaveCarServiceInsert()
        {
            string[] parName = { "name", "address", "phone", "mail" };
            string[] parValue = new string[4];

            // zapisanie wartości z textboxów do zmiennej
            for (int i = 0; i < 4; i++)
                parValue[i] = ControlPanel.Controls["ctrl" + i].Text;

            // sprawdzenie zapisanych wartości
            if (!checkString(parValue[0]))
                mainMenu.ExitProgram();
            if (!checkString(parValue[1]))
                mainMenu.ExitProgram();
            if (!checkPhone(parValue[2]))
                mainMenu.ExitProgram();
            if (!checkString(parValue[3]))
                mainMenu.ExitProgram();

            // zapis na bazie
            bool res = SqlConn.InsertIntoTableProcedure("PROC_INSERT_CAR_SERVICES", parName, parValue, token);
            if (res == false)
            {
                MessageBox.Show("Nie udało się zapisać nowego warsztatu.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                mainMenu.ExitProgram();
            }
            else
                MessageBox.Show("Pomyślnie zapisano dane warsztatu.");

            // zakończenie okna dialogowego
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sprawdza dane nowej usługi, jeśli są poprawne dodaje dane do SERVICES.         
        /// </summary>
        private void SaveServiceInsert()
        {
            string[] parName = { "cost", "time", "description" };
            string[] parValue = new string[3];

            // zapisanie wartości z textboxów do zmiennej
            for (int i = 0; i < 3; i++)
                parValue[i] = ControlPanel.Controls["ctrl" + i].Text;

            // sprawdzenie zapisanych wartości
            if (!checkInt(parValue[0]))
                mainMenu.ExitProgram();
            if (!checkString(parValue[1]))
                mainMenu.ExitProgram();
            if (!checkString(parValue[2]))
                mainMenu.ExitProgram();

            // zapis na bazie
            bool res = SqlConn.InsertIntoTableProcedure("PROC_INSERT_SERVICES", parName, parValue, token);
            if (res == false)
            {
                MessageBox.Show("Nie udało się zapisać nowej usługi.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                mainMenu.ExitProgram();
            }
            else
                MessageBox.Show("Pomyślnie zapisano dane usługi.");

            // zakończenie okna dialogowego
            this.DialogResult = DialogResult.OK;
        }

        #region Sprawdzanie zmiennych

        private bool checkString(string str)
        {
            if (str == "")
            {
                MessageBox.Show("Należy przypisać wartość do każdego z parametrów.\nZapis danych niemożliwy.");
                return false;
            }

            foreach (char ch in str)
            {
                if (!Program.LegalChars.Contains(ch))
                {
                    MessageBox.Show("W wartości: " + str + " użyto niedozwolonego znaku: " + ch + "\nZapis danych niemożliwy.");
                    return false;
                }
            }
            
            return true;
        }

        private bool checkInt(string str)
        {
            if (str == "")
            {
                MessageBox.Show("Należy przypisać wartość do każdego z parametrów.\nZapis danych niemożliwy.");
                return false;
            }

            int tmp;
            bool res = Int32.TryParse(str, out tmp);
            if (res == false)
            {
                MessageBox.Show("Wartość: " + str + " powinna być liczbą całkowitą.\nZapis danych niemożliwy.");
                return false;
            }

            return true;
        }

        private bool checkPhone(string str)
        {
            if (checkInt(str) == false)
                return false;

            if (str.Length != 9)
            {
                MessageBox.Show("Numer telefonu powinien zawierać dokładnie 9 cyfr.\nZapis danych niemożliwy.");
                return false;
            }

            return true;
        }        

        #endregion

        #endregion

    }
}
