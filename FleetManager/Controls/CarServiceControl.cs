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

            DataTable carServiceTable = SqlConn.GetTableProcedure("PROC_GET_CAR_SERVICE", "input_id", id, token);

            this.NameLabel.Text = carServiceName = carServiceTable.Rows[0]["name"].ToString();
            this.AddressLabel.Text = carServiceTable.Rows[0]["address"].ToString();
            this.PhoneLabel.Text = carServiceTable.Rows[0]["phone"].ToString();
            this.MailLabel.Text = carServiceTable.Rows[0]["mail"].ToString();

            this.RegisterVisitButton.Text = "Zarezerwuj termin wizyty.";
            this.RegisterVisitButton.Visible = true;
        }

        private void RegisterVisitButton_Click(object sender, EventArgs e)
        {
            RegisterVisitMenu registerVisitMenu = new RegisterVisitMenu(token, carServiceName, id);
            registerVisitMenu.ShowDialog();
        }
    }
}
