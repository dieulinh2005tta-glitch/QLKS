using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;

namespace QLKS1
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            uC_AddRoom1.Visible = false;
            uC_CustomerRes1.Visible = false;
            uC_ChẹckOut1.Visible = false;
            btnAddRoom.PerformClick();
        }


        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            PanelMoving.Left = btnAddRoom.Left + 50;
            uC_AddRoom1.Visible = true;
            uC_AddRoom1.BringToFront();
        }

      
        private void btnCustomerRes_Click(object sender, EventArgs e)
        {
            PanelMoving.Left= btnCustomerRes.Left + 50;
            uC_CustomerRes1.Visible = true;
            uC_CustomerRes1.BringToFront();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            PanelMoving.Left=btnCheckOut.Left + 50;
            uC_ChẹckOut1.Visible = true;
            uC_ChẹckOut1.BringToFront();




        }
    }
}
