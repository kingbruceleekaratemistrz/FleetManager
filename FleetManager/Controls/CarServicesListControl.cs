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
    public partial class CarServicesListControl : UserControl
    {
        MainMenu mainMenu;

        public CarServicesListControl()
        {
            InitializeComponent();
        }

        public CarServicesListControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.flowLayoutPanel1.AutoScroll = true;
            LoadCarServicesList(token);
        }

        private void LoadCarServicesList(byte[] token)
        {
            DataTable carServices = SqlConn.GetTableProcedure("PROC_GET_CAR_SERVICES_LIST", token);

            Color[] color = { Color.Red, Color.Blue, Color.Green, Color.Pink, Color.Purple, Color.Red, Color.Blue, Color.Green, Color.Pink, Color.Purple };
            for (int i = 0; i < carServices.Rows.Count; i++)
            {
                int id = (int)carServices.Rows[i]["car_service_id"];
                Console.WriteLine("panel " + i +": " + id);

                Panel panel = new Panel();
                panel.Width = flowLayoutPanel1.Width - 25;
                panel.Height = 100;
                panel.BackColor = color[i];
                panel.Tag = id;
                panel.Click += new EventHandler(Panel_Click);

                Label carServiceName = new Label();
                carServiceName.Width = panel.Width - 6;
                carServiceName.Height = 50 - 6;
                carServiceName.Location = new Point(3, 3);
                carServiceName.BackColor = Color.Goldenrod;
                carServiceName.Text = carServices.Rows[i]["name"].ToString();
                carServiceName.Tag = id;
                carServiceName.Click += new EventHandler(Panel_Click);

                Label carServiceAddress = new Label();
                carServiceAddress.Width = panel.Width - 6;
                carServiceAddress.Height = 50 - 6;
                carServiceAddress.Location = new Point(3, 53);
                carServiceAddress.BackColor = Color.Goldenrod;
                carServiceAddress.Text = carServices.Rows[i]["address"].ToString();
                carServiceAddress.Tag = id;
                carServiceAddress.Click += new EventHandler(Panel_Click);

                panel.Controls.Add(carServiceName);
                panel.Controls.Add(carServiceAddress);
                flowLayoutPanel1.Controls.Add(panel);
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (sender is Panel)
                id = (int)((Panel)sender).Tag;
            else if (sender is Label)
                id = (int)((Label)sender).Tag;

            mainMenu.ShowCarServiceProfile(id);
        }
    }
}
