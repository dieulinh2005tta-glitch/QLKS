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
        // Thêm delegate và event thắng
        public delegate void CheckoutCompletedEventHandler(object sender, double totalAmount);
        public event CheckoutCompletedEventHandler CheckoutCompleted;

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
                        "where chekout= 'NO'";
            DataSet ds = fn.GetData(query);
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

        /*private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (txtCName.Text != " ")
            {
                if (MessageBox.Show("Bạn có chắc chắn không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String cdate = txtCheckOutDate.Text;

                    // Lấy chỉ số hàng được chọn
                    int rowIndex = guna2DataGridView1.SelectedCells[0].OwningRow.Index;
                    DataGridViewRow row = guna2DataGridView1.Rows[rowIndex];

                    bool isHourly = Convert.ToBoolean(row.Cells["is_hourly"].Value);
                    double totalAmount = 0;

                    if (isHourly)
                    {
                        // Thay đổi ở đây: Kiểm tra giá trị trước khi chuyển đổi
                        if (row.Cells["checkin_time"].Value != DBNull.Value)
                        {
                            DateTime checkinTime = Convert.ToDateTime(row.Cells["checkin_time"].Value);
                            DateTime checkoutTime = DateTime.Now;
                            TimeSpan duration = checkoutTime - checkinTime;
                            double hours = Math.Ceiling(duration.TotalHours);
                            double pricePerHour = Convert.ToDouble(row.Cells["price_per_hour"].Value);
                            totalAmount = hours * pricePerHour;

                            MessageBox.Show("Tổng số giờ thuê: " + hours + " tiếng.\n" +
                                            "Tổng tiền theo giờ là: " + totalAmount.ToString("N0") + " VNĐ.", "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không có thời gian check-in hợp lệ để tính tiền theo giờ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Ngừng thực thi nếu không thể tính toán
                        }
                    }
                    else // Logic tính tiền theo ngày
                    {
                        // Kiểm tra giá trị của cột "checkin"
                        if (row.Cells["checkin"].Value != DBNull.Value)
                        {
                            DateTime checkinDate = Convert.ToDateTime(row.Cells["checkin"].Value);
                            DateTime checkoutDate = DateTime.Parse(cdate);

                            int totalDays = (int)(checkoutDate - checkinDate).TotalDays + 1;

                            double dailyPrice = Convert.ToDouble(row.Cells["price"].Value);
                            totalAmount = totalDays * dailyPrice;

                            MessageBox.Show("Số ngày thuê: " + totalDays + " ngày.\n" +
                                            "Tổng tiền theo ngày là: " + totalAmount.ToString("N0") + " VNĐ.", "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không có ngày check-in hợp lệ để tính tiền theo ngày.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Tiếp tục cập nhật cơ sở dữ liệu sau khi tính toán thành công
                    query = "UPDATE customer SET chekout = 'YES', checkout = '" + cdate + "', total_amount = " + totalAmount + " WHERE cid = " + id + "; " +
                            "UPDATE rooms SET booked = 'NO' WHERE roomNo='" + txtRoom.Text + "'";

                    fn.setData(query, "Thanh Toán Thành Công");
                    UC_CheckOut_Load(this, null);
                    clearAll();
                }
            }
            else
            {
                MessageBox.Show("Không có khách hàng để lựa chọn", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }*/

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (txtCName.Text != " ")
            {
                if (MessageBox.Show("Bạn có chắc chắn không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String cdate = txtCheckOutDate.Text;

                 
                    int rowIndex = guna2DataGridView1.SelectedCells[0].OwningRow.Index;
                    DataGridViewRow row = guna2DataGridView1.Rows[rowIndex];

                    bool isHourly = Convert.ToBoolean(row.Cells["is_hourly"].Value);
                    double totalAmount = 0;

                    if (isHourly)
                    {
                        if (row.Cells["checkin_time"].Value != DBNull.Value)
                        {
                            DateTime checkinTime = Convert.ToDateTime(row.Cells["checkin_time"].Value);
                            DateTime checkoutTime = DateTime.Now;
                            TimeSpan duration = checkoutTime - checkinTime;
                            double hours = Math.Ceiling(duration.TotalHours);

                            
                            if (row.Cells["price_per_hour"].Value != DBNull.Value)
                            {
                                double pricePerHour = Convert.ToDouble(row.Cells["price_per_hour"].Value);
                                totalAmount = hours * pricePerHour;

                                MessageBox.Show("Tổng số giờ thuê: " + hours + " tiếng.\n" +
                                                "Tổng tiền theo giờ là: " + totalAmount.ToString("N0") + " VNĐ.", "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }//day la cai bien tong tien totalAmount
                            else
                            {
                                MessageBox.Show("Giá thuê theo giờ không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có thời gian check-in hợp lệ để tính tiền theo giờ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else // Logic tính tiền theo ngày
                    {
                        if (row.Cells["checkin"].Value != DBNull.Value)
                        {
                            DateTime checkinDate = Convert.ToDateTime(row.Cells["checkin"].Value);
                            DateTime checkoutDate = DateTime.Parse(cdate);

                            int totalDays = (int)(checkoutDate - checkinDate).TotalDays + 1;

                            // Lỗi `InvalidCastException` có thể xảy ra ở đây, đảm bảo cột `price` không rỗng
                            if (row.Cells["price"].Value != DBNull.Value)
                            {
                                double dailyPrice = Convert.ToDouble(row.Cells["price"].Value);
                                totalAmount = totalDays * dailyPrice;

                                MessageBox.Show("Số ngày thuê: " + totalDays + " ngày.\n" +
                                                "Tổng tiền theo ngày là: " + totalAmount.ToString("N0") + " VNĐ.", "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Giá thuê theo ngày không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có ngày check-in hợp lệ để tính tiền theo ngày.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Bước quan trọng: Gọi event để truyền giá trị tổng tiền
                    // Dấu ? đảm bảo event chỉ được gọi nếu có form khác đang lắng nghe
                    CheckoutCompleted?.Invoke(this, totalAmount);

                    // Tiếp tục cập nhật cơ sở dữ liệu sau khi tính toán thành công
                    string id = row.Cells["cid"].Value.ToString();
                    string roomNo = row.Cells["roomNo"].Value.ToString();

                    string query = "UPDATE customer SET chekout = 'YES', checkout = '" + cdate + "', total_amount = " + totalAmount + " WHERE cid = " + id + "; " +
                            "UPDATE rooms SET booked = 'NO' WHERE roomNo='" + roomNo + "'";

                    function fn = new function();
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
