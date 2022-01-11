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
    public partial class CarsListControl : UserControl
    {
        public CarsListControl()
        {
            InitializeComponent();
        }

        public CarsListControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.flowLayoutPanel1.AutoScroll = true;
            LoadCarsList(token, mainMenu);
        }

        private void LoadCarsList(byte[] token, MainMenu mainMenu)
        {
            DataTable cars = SqlConn.GetTableProcedure("PROC_GET_CARS_LIST", token);
            if (cars == null)
                mainMenu.ExitProgram();
            else
            {
                for (int i = 0; i < cars.Rows.Count; i++)
                {
                    Panel panel = new Panel();
                    panel.Width = flowLayoutPanel1.Width;
                    panel.Height = 100;
                    panel.Tag = (int)cars.Rows[i]["car_id"];
                    panel.Click += new EventHandler(panel_Click);

                    Label carName = new Label();
                    carName.Text = cars.Rows[i]["brand"].ToString() + ' ' + cars.Rows[i]["model"].ToString();
                    carName.Font = new Font("Microsoft Sans Serif", 18);
                    carName.Width = panel.Width;

                    Label carInfo = new Label();
                    carInfo.Text = cars.Rows[i]["prod_year"].ToString() + " rok, "
                        + cars.Rows[i]["hp"].ToString() + " KM, "
                        + cars.Rows[i]["cc"].ToString() + " cm\xB3";
                    carInfo.Font = new Font("Microsoft Sans Serif", 12);
                    carInfo.AutoSize = true;
                    carInfo.Location = new Point(carName.Location.X, carName.Location.Y + 30);

                    panel.Controls.Add(carName);
                    panel.Controls.Add(carInfo);
                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
        }

        private void panel_Click(object sender, EventArgs e)
        {
            Panel tmpPanel = (Panel) sender;
            MessageBox.Show("car id: " + tmpPanel.Tag);
        }
    }
}
