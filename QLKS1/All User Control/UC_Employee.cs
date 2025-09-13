using DocumentFormat.OpenXml.Bibliography;
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
    public partial class UC_Employee : UserControl
    {
        function fn = new function();
        string query;
        public UC_Employee()
        {
            InitializeComponent();
        }


        private void UC_Employee_Load(object sender, EventArgs e)
        {
            getMaxID();
        }
        public void getMaxID()
        {
            query = "select max(eid) from employee";
            DataSet ds = fn.GetData(query);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                Int64 num = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                labelToSet.Text = (num + 1).ToString();
            }

        }

        private void btnRegistaton_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtMobile.Text != " " && txtGender.Text != "" && txtEmail.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                string name = txtName.Text;
                Int64 mobile = Int64.Parse(txtMobile.Text);
                string email = txtEmail.Text;
                string gender = txtGender.Text;
                string username = txtUsername.Text;
                string pass = txtPassword.Text;


                query = "insert into employee (ename, mobile, gender, emailid, username, pass) values('" + name + "','" + mobile + "','" + gender + "','" + email + "','" + username + "','" + pass + "')";
                fn.setData(query, "Đăng Ký Nhân Viên Thành Công.");

                clearAll();
                getMaxID();
            }
        }

        public void clearAll()
        {
            txtName.Clear();
            txtMobile.Clear();
            txtGender.SelectedIndex = -1;
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();   

        }

        private void tabEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(tabEmployee.SelectedIndex == 1)
            {
                setEmployee(guna2DataGridView1);

            }else if (tabEmployee.SelectedIndex==2)
            {
                setEmployee(guna2DataGridView2);
            }
        }
        public void setEmployee(DataGridView dgv)
        {
            query = "select * from employee";
            DataSet ds = fn.GetData(query);
            dgv.DataSource = ds.Tables[0];
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show(" Bạn có chắc chắn không", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = " delete from employee where eid= " + txtID.Text + "";
                    fn.setData(query, "Thông Tin Nhân Viên Đã Được Xóa");
                    tabEmployee_SelectedIndexChanged(this, null);

                }
            }
        }

        private void UC_Employee_Leave(object sender, EventArgs e)
        {
            clearAll();

        }
    }
}



