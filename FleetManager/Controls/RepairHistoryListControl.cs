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
    public partial class RepairHistoryListControl : UserControl
    {
        MainMenu mainMenu;
        byte[] token;
            
        public RepairHistoryListControl()
        {
            InitializeComponent();
        }

        public RepairHistoryListControl(byte[] token, MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.token = token;
            this.flowLayoutPanel1.AutoScroll = true;
            LoadRepairHistoryList();
        }

        private void LoadRepairHistoryList()
        {
            DataTable repairs = SqlConn.GetTableProcedure("PROC_GET_REPAIR_HISTORY_LIST", token);
            if (repairs == null)
                mainMenu.ExitProgram();
            else
            {
                for (int i = 0; i < repairs.Rows.Count; i++)
                {
                    Panel panel = new Panel()
                    {
                        Width = flowLayoutPanel1.Width - 25,
                        Height = 90,
                        BackColor = SystemColors.ControlLight
                    };

                    Label description = new Label()
                    {
                        Width = panel.Width - 6,
                        Height = 27,
                        Location = new Point(3, 3),
                        BackColor = SystemColors.GradientActiveCaption,
                        Text = repairs.Rows[i]["description"].ToString()
                    };

                    Label carService = new Label()
                    {
                        Width = panel.Width - 6,
                        Height = 30,
                        Location = new Point(3, 30),
                        BackColor = SystemColors.GradientActiveCaption,
                        Text = repairs.Rows[i]["name"].ToString()
                    };

                    Label dateCostTime = new Label()
                    {
                        Width = panel.Width - 6,
                        Height = 27,
                        Location = new Point(3, 60),
                        BackColor = SystemColors.GradientActiveCaption,
                        Text = repairs.Rows[i]["date"].ToString() + ", " + repairs.Rows[i]["cost"].ToString() + " PLN, " + repairs.Rows[i]["time"].ToString()
                    };

                    panel.Controls.Add(description);
                    panel.Controls.Add(carService);
                    panel.Controls.Add(dateCostTime);
                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
        }
    }
}
