using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS1.All_User_Control
{
    public partial class UC_CustomerRes : UserControl
    {
        function fn = new function();
        String query;
        public UC_CustomerRes()
        {
            InitializeComponent();
        }
        public void setComboBox(String query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
             sdr.Close();
        }
        private void label7_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }
        int rid;
        private void RoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            query= "Select price, roomid from rooms where roomNo= '" + txtRoomNo.Text+ "'";
            DataSet ds = fn.GetData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());

        }

        private void UC_CustomerRes_Load(object sender, EventArgs e)
        {

        }

        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoom.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();

        }

        private void txtRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomNo.Items.Clear();
            query = "select roomNo from rooms where bed = '" + txtBed.Text + "' and roomType = '" + txtRoom.Text + "' and booked = 'No'";
            setComboBox(query, txtRoomNo);
        }

        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtContact.Text != " " && txtNationality.Text != " " && txtGender.Text != " " && txtDob.Text != " " && txtIDProof.Text != " " && txtAddress.Text != " " && txtCheckin.Text != " " && txtPrice.Text!= " ") 

            {
                string Name =txtName.Text;
                Int64 mobile= Int64.Parse(txtContact.Text);
                string national =txtNationality.Text;
                string gender= txtGender.Text;
                string dob = txtDob.Text;
                string idproof = txtIDProof.Text;
                string address = txtAddress.Text;
                string checkin = txtCheckin.Text;


                query = "Insert into customer (cname ,mobile,nationality,gender, dob,idproof,address, checkin, roomid) values('" + Name + "','" + mobile + "','" + national + "','" + gender + "','"+dob+"','" + idproof + "','" + address + "','" + checkin + "'," + rid + ") update rooms set booked ='YES' where roomNo='" + txtRoomNo.Text + "'";
                fn.setData(query,"Số Phòng"+txtRoomNo.Text+"Đăng ký khách hàng thành công.");
                clearAll();



            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        public void clearAll()
        {
            txtName.Clear();
            txtContact.Clear();
            txtNationality.Clear();
            txtGender.SelectedIndex = -1;
            txtDob.ResetText();
            txtIDProof.Clear();
            txtAddress.Clear();
            txtCheckin.ResetText();
            txtBed.SelectedIndex = -1;
            txtRoom.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();



        }

        private void UC_CustomerRes_Leave(object sender, EventArgs e)
        {
            clearAll();

        }

        private void btnSuaKhachHang_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtContact.Text != "" && txtIDProof.Text != "")
            {
                string Name = txtName.Text;
                Int64 mobile = Int64.Parse(txtContact.Text);
                string national = txtNationality.Text;
                string gender = txtGender.Text;
                string dob = txtDob.Text;
                string idproof = txtIDProof.Text;
                string address = txtAddress.Text;
                string checkin = txtCheckin.Text;

              
                query = "UPDATE customer SET cname = '" + Name + "', mobile = '" + mobile + "', nationality = '" + national + "', gender = '" + gender + "', dob = '" + dob + "', address = '" + address + "', checkin = '" + checkin + "' WHERE idproof = '" + idproof + "'";

              
                fn.setData(query, "Thông tin khách hàng đã được cập nhật thành công.");
                clearAll();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cần thiết (Tên, SĐT, CMND/CCCD).", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaKhachHang_Click(object sender, EventArgs e)
        {
            if (txtIDProof.Text != "")
            {
              
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không? Thao tác này sẽ làm trống phòng của họ.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    String idproof = txtIDProof.Text;

                    
                    string getRoomNoQuery = "SELECT roomid FROM customer WHERE idproof = '" + idproof + "'";
                    DataSet dsRoomId = fn.GetData(getRoomNoQuery);

                    if (dsRoomId.Tables[0].Rows.Count > 0)
                    {
                        int roomid = int.Parse(dsRoomId.Tables[0].Rows[0][0].ToString());

                        
                        string updateRoomQuery = "UPDATE rooms SET booked = 'No' WHERE roomid = " + roomid;
                        fn.setData(updateRoomQuery, ""); 

                       
                        string deleteCustomerQuery = "DELETE FROM customer WHERE idproof = '" + idproof + "'";
                        fn.setData(deleteCustomerQuery, "Khách hàng đã được xóa và phòng đã được làm trống.");

                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để xóa.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền CMND/CCCD của khách hàng cần xóa.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    }
    

