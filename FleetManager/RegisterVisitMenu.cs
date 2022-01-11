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
    public partial class RegisterVisitMenu : Form
    {
        private List<string> description = new List<string>();
        private List<string> time = new List<string>();
        private List<int> cost = new List<int>();
        private byte[] token;
        private int id;
        private MainMenu mainMenu;

        public RegisterVisitMenu()
        {
            InitializeComponent();
        }

        public RegisterVisitMenu(byte[] token, string name, int id, MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;

            // Inicjalizacja i przypisanie wartości
            InitializeComponent();
            this.token = token;
            this.id = id;

            // Ustawienie Labeli i DateTimePickera
            this.CarServiceLabel.Text = name;
            this.ConfirmationButton.Enabled = false;

            // Inicjalizacja DateTimePickera i HourComboBoxa
            // Pobranie godziny i zamiana na int
            int tmpTime = Int32.Parse(DateTime.Now.ToString("HH:mm").Replace(":", "")); 
            // Lista godzin do HourComboBoxa
            List<int> availableHours = new List<int>();
            int tmpHour = 800;
            for (int i = 0; i < 17; i++)
            {              
                if (tmpHour > tmpTime)
                    availableHours.Add(tmpHour);
                // wzrost naprzemian o 30 i 70. Dzieki temu tmphour przyjmuje wartosci: 800, 830, 900, 930...
                tmpHour += i % 2 == 0 ? 30 : 70;                
            }
            // Jeśli lista avaibleHours jest pusta to minimalna data to jutrzejszy dzień i wszystkie godziny sa dostepne.
            // Poniższa instrukcja zmienia date co wywołuje DateTimePicker_ValueChanged()
            if (availableHours.Count == 0)
                this.DateTimePicker.MinDate = DateTime.Today.AddDays(1);
            else
            {
                this.DateTimePicker.MinDate = DateTime.Today;
                foreach (int tm in availableHours)
                    this.HourComboBox.Items.Add(tm);
            }
            this.HourComboBox.SelectedIndex = 0;
            this.DateTimePicker.MaxDate = this.DateTimePicker.MinDate.AddMonths(2);

            // Pobranie danych o usługach
            DataTable serviceTable = SqlConn.GetTableProcedure("PROC_GET_SERVICES", token);
            if (serviceTable == null)
                mainMenu.ExitProgram();
            else
            {
                for (int i = 0; i < serviceTable.Rows.Count; i++)
                {
                    // Zapisanie danych o usługach
                    description.Add(serviceTable.Rows[i]["description"].ToString());
                    cost.Add((int)serviceTable.Rows[i]["cost"]);
                    time.Add(serviceTable.Rows[i]["time"].ToString());

                    // Dodawanie usług do ComboBoxa
                    this.ServiceComboBox.Items.Add(serviceTable.Rows[i]["description"].ToString());
                }
            }
        }

        private void RegisterVisitMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                MessageBox.Show("Zarezerwowano wizytę!");
            else if (MessageBox.Show("Anulować rejestracje wizyty?", "Fleet Manager", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;           
        }

        private void ServiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CostLabel.Text = "Koszt: " + cost[((ComboBox)sender).SelectedIndex] + " PLN";
            this.TimeLabel.Text = "Czas: " + time[((ComboBox)sender).SelectedIndex];
            this.ConfirmationButton.Enabled = true;
        }

        private void ConfirmationButton_Click(object sender, EventArgs e)
        {           
            
            string date = this.DateTimePicker.Value.ToString("yyyy'-'MM'-'dd");
            string hour = this.HourComboBox.SelectedItem.ToString();
            if (hour.Length == 3)
                hour = hour.Insert(1, ":");
            else
                hour = hour.Insert(2, ":");

            string[] parNameStr = { "date" };
            string[] parValueStr = { date + "T" + hour + ":00" };

            string[] parNameInt = { "service_id", "car_service_id" };
            int[] parValueInt = { this.ServiceComboBox.SelectedIndex + 1, this.id };

            bool res = SqlConn.InsertIntoTableProcedure("PROC_INSERT_REPAIR_HISTORY", parNameStr, parValueStr, parNameInt, parValueInt, token);
            if (res == false)
            {
                MessageBox.Show("Nie udało się zarejestrować wizyty w warsztacie.\nBłędny token sesji.\nNastąpi zamknięcie programu.");
                mainMenu.ExitProgram();
            }
            else
                MessageBox.Show("Pomyślnie zarezerwowano termin wizyty w warsztacie.");

            this.DialogResult = DialogResult.OK;
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (string.Compare(((DateTimePicker)sender).Value.ToString("yyyy'-'MM'-'dd"), DateTime.Now.ToString("yyyy'-'MM'-'dd")) == 0)
            {               
                int tmpTime = Int32.Parse(DateTime.Now.ToString("HH:mm").Replace(":", ""));                
                int tmpHour = 800;
                this.HourComboBox.Items.Clear();
                for (int i = 0; i < 17; i++)
                {
                    if (tmpHour > tmpTime)
                        this.HourComboBox.Items.Add(tmpHour);
                    // wzrost naprzemian o 30 i 70. Dzieki temu tmphour przyjmuje wartosci: 800, 830, 900, 930...
                    tmpHour += i % 2 == 0 ? 30 : 70;
                }
            }
            else
            {
                int tmpHour = 800;
                this.HourComboBox.Items.Clear();
                for (int i = 0; i < 17; i++)
                {
                    this.HourComboBox.Items.Add(tmpHour);
                    tmpHour += i % 2 == 0 ? 30 : 70;
                }
            }
        }
    }
}
