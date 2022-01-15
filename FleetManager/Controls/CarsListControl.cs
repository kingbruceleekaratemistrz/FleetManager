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
                    Panel panel = new Panel()
                    {
                        Width = flowLayoutPanel1.Width - 25,
                        Height = 60,
                        BackColor = SystemColors.ControlLight                    
                    };

                    Label carName = new Label()
                    {
                        Width = panel.Width - 6,
                        Height = 32,
                        Location = new Point(3, 3),
                        BackColor = SystemColors.GradientActiveCaption,
                        Font = new Font("Microsoft Sans Serif", 16),
                        Text = cars.Rows[i]["brand"].ToString() + ' ' + cars.Rows[i]["model"].ToString()
                    };

                    Label carInfo = new Label()
                    {
                        Width = panel.Width - 6,
                        Height = 23,
                        Location = new Point(3, 35),
                        BackColor = SystemColors.GradientActiveCaption,
                        Font = new Font("Microsoft Sans Serif", 12),
                        Text = cars.Rows[i]["prod_year"].ToString() + " rok, "
                        + cars.Rows[i]["hp"].ToString() + " KM, "
                        + cars.Rows[i]["cc"].ToString() + " cm\xB3"
                    };

                    panel.Controls.Add(carName);
                    panel.Controls.Add(carInfo);
                    flowLayoutPanel1.Controls.Add(panel);
                }                
            }
        }
    }
}
