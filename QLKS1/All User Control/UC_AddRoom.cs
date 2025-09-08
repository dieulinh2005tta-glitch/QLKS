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
    public partial class UC_AddRoom : UserControl
    {
        function fn = new function();
        String query;
        public UC_AddRoom()
        {
            InitializeComponent();
        }


        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            query = "select * from rooms";
            DataSet ds = fn.GetData(query);
            DataGridView1.DataSource = ds.Tables[0];

        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            if (txtRoomNo.Text != "" && txtRoomType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
            {
                String roomno = txtRoomNo.Text;
                String type = txtRoomType.Text;
                String bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);



                query = "insert into rooms (roomNo, roomType, bed, price) values ('" + roomno + "','" + type + "','" + bed + "'," + price + ")";
                fn.setData(query, "Đã Thêm Phòng");

                UC_AddRoom_Load(this, null);
                clearAll();

            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void clearAll()
        {
            txtRoomNo.Clear();
            txtRoomType.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();
        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            UC_AddRoom_Load(this, null);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtRoomNo.Text != "" && txtRoomType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
            {
                String roomno = txtRoomNo.Text;
                String type = txtRoomType.Text;
                String bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

               
                query = "UPDATE rooms SET roomType = '" + type + "', bed = '" + bed + "', price = " + price + " WHERE roomNo = '" + roomno + "'";

               
                fn.setData(query, "Thông tin phòng đã được cập nhật thành công.");

                UC_AddRoom_Load(this, null);
                clearAll();
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin để sửa phòng.", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
  

            if (e.RowIndex >= 0)
            {
              
                DataGridViewRow row = this.DataGridView1.Rows[e.RowIndex];

     
                txtRoomNo.Text = row.Cells[1].Value.ToString();
                txtRoomType.Text = row.Cells[2].Value.ToString();
                txtBed.Text = row.Cells[3].Value.ToString();
                txtPrice.Text = row.Cells[4].Value.ToString();
            }


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtRoomNo.Text != "")
            {
                DialogResult result = MessageBox.Show("Phòng này có thể đang được sử dụng. Bạn có chắc chắn muốn xóa phòng và tất cả khách hàng liên quan không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    String roomno = txtRoomNo.Text;


                    query = "SELECT roomid FROM rooms WHERE roomNo = '" + roomno + "'";
                    DataSet ds = fn.GetData(query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int roomidToDelete = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                     
                        string deleteCustomerQuery = "DELETE FROM customer WHERE roomid = " + roomidToDelete;
                        fn.setData(deleteCustomerQuery, ""); 

                    
                        string deleteRoomQuery = "DELETE FROM rooms WHERE roomNo = '" + roomno + "'";
                        fn.setData(deleteRoomQuery, "Phòng " + roomno + " đã được xóa thành công.");

                        UC_AddRoom_Load(this, null);
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    }


