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
            if (carServices == null)
                mainMenu.ExitProgram();
            else
            {
                for (int i = 0; i < carServices.Rows.Count; i++)
                {
                    int id = (int)carServices.Rows[i]["car_service_id"];

                    Panel panel = new Panel()
                    {
                        Width = flowLayoutPanel1.Width - 25,
                        Height = 60,
                        BackColor = SystemColors.ControlLight,
                        Tag = id
                    };
                    panel.Click += new EventHandler(Panel_Click);

                    Label carServiceName = new Label()
                    {
                        Width = panel.Width - 6,
                        Height = 32,
                        Location = new Point(3, 3),
                        BackColor = SystemColors.GradientActiveCaption,
                        Font = new Font("Microsoft Sans Serif", 16),
                        Text = carServices.Rows[i]["name"].ToString(),
                        Tag = id
                    };
                    carServiceName.Click += new EventHandler(Panel_Click);

                    Label address = new Label()
                    {
                        Width = panel.Width - 6,
                        Height = 23,
                        Location = new Point(3, 35),
                        BackColor = SystemColors.GradientActiveCaption,
                        Font = new Font("Microsoft Sans Serif", 12),
                        Text = carServices.Rows[i]["address"].ToString(),
                        Tag = id
                    };
                    address.Click += new EventHandler(Panel_Click);


                    panel.Controls.Add(carServiceName);
                    panel.Controls.Add(address);
                    flowLayoutPanel1.Controls.Add(panel);
                }                
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
