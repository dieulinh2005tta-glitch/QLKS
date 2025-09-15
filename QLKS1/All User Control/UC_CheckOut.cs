using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS1.All_User_Control
{
    public partial class UC_CheckOut : UserControl
    {
        function fn = new function();
        string query;

        public UC_CheckOut()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void UC_CheckOut_Load(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price, rooms.price_per_hour, customer.is_hourly, customer.checkin_time " +
                        "from customer inner join rooms on customer.roomid= rooms.roomid " +
                        "where chekout= 'NO'"; DataSet ds = fn.GetData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price, rooms.price_per_hour, customer.is_hourly, customer.checkin_time " +
                       "from customer inner join rooms on customer.roomid= rooms.roomid " +
                       "where cname like '" + txtName.Text + "%' and chekout = 'NO'"; DataSet ds = fn.GetData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];


        }
        int id;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCName.Text= guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRoom.Text= guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();


            }
           
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if(txtCName.Text!=" ")
            {
                if(MessageBox.Show("Bạn có chắc chắn không?","Xác nhận",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)== DialogResult.OK)
                {
                    String cdate= txtCheckOutDate.Text;

                    int rowIndex = guna2DataGridView1.SelectedCells[0].OwningRow.Index;
                    DataGridViewRow row = guna2DataGridView1.Rows[rowIndex];

                   
                    bool isHourly = Convert.ToBoolean(row.Cells["is_hourly"].Value);
                    double totalAmount = 0;
                    string checkoutMessage = "";

                    if (isHourly)
                    {
                        DateTime checkinTime = Convert.ToDateTime(row.Cells["checkin_time"].Value);
                        DateTime checkoutTime = DateTime.Now;
                        TimeSpan duration = checkoutTime - checkinTime;
                        double hours = Math.Ceiling(duration.TotalHours);
                        double pricePerHour = Convert.ToDouble(row.Cells["price_per_hour"].Value);
                        totalAmount = hours * pricePerHour;
                        checkoutMessage = "Tổng tiền theo giờ là: " + totalAmount.ToString("N0") + " VNĐ";
                    }
                    else
                    {
                        totalAmount = Convert.ToDouble(row.Cells["price"].Value);
                        checkoutMessage = "Tổng tiền theo ngày là: " + totalAmount.ToString("N0") + " VNĐ";
                    }

                    MessageBox.Show(checkoutMessage, "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    query = "update customer set chekout= 'YES', checkout='" + cdate + "' where cid = " + id + "update rooms set booked= 'NO' where roomNo='" + txtRoom.Text + "'";
                    fn.setData(query, "Thanh Toán Thành Công");
                    UC_CheckOut_Load(this, null);
                    clearAll();

                }
            }
            else 
            {
                MessageBox.Show("Không có khách hàng để lựa chọn", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public void clearAll()
        {
            txtCName.Clear();
            txtName.Clear();
            txtRoom.Clear();
            txtCheckOutDate.ResetText();

        }

        private void UC_CheckOut_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
