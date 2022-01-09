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
    public partial class CarProfileControl : UserControl
    {
        public CarProfileControl()
        {
            InitializeComponent();
        }

        public CarProfileControl(byte[] token)
        {
            InitializeComponent();

            DataTable carTable = SqlConn.GetTableProcedure("PROC_GET_CAR_PROFILE", token);

            this.BrandLabel.Text = carTable.Rows[0]["brand"].ToString();
            this.ModelLabel.Text = carTable.Rows[0]["model"].ToString();
            this.PlateNumberLabel.Text = carTable.Rows[0]["car_plate"].ToString();
            this.ProductionYearLabel.Text = carTable.Rows[0]["prod_year"].ToString() + " rok";
            this.HPLabel.Text = "Moc " + carTable.Rows[0]["hp"].ToString() + " KM";
            this.CCLabel.Text = "Pojemność " + carTable.Rows[0]["cc"].ToString() + " cm\xB3";
        }
    }
}
