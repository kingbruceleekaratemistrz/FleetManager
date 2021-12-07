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

        public CarsListControl(byte[] token)
        {
            InitializeComponent();
            this.flowLayoutPanel1.AutoScroll = true;
            LoadCarsList(token);
        }

        private void LoadCarsList(byte[] token)
        {
            DataTable cars = SqlConn.GetTableProcedure("PROC_GET_CARS_LIST", token);

            for (int i = 0; i < cars.Rows.Count; i++)
            {
                Panel panel = new Panel();
                panel.Width = flowLayoutPanel1.Width;
                panel.Height = 100;

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
}
