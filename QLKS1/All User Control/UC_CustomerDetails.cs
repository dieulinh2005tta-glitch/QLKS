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
    public partial class UC_CustomerDetails : UserControl
    {

        function fn = new function();
        string query;
        public UC_CustomerDetails()
        {
            InitializeComponent();
        }

        private void txtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSearchBy.SelectedIndex == 0)
            {
                query = "select customer.cid, customer.cname, customer.mobile,customer.nationality,customer.gender, customer.dob, customer.idproof,customer.address,customer.checkin, rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms  on customer.roomid= rooms.roomid ";
                getRecord(query);
            }
            else if (txtSearchBy.SelectedIndex == 1)
            {
                query = "select customer.cid, customer.cname, customer.mobile,customer.nationality,customer.gender, customer.dob, customer.idproof,customer.address,customer.checkin, rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms  on customer.roomid= rooms.roomid where checkout is null";
                getRecord(query);
            }
            else if (txtSearchBy.SelectedIndex == 2)
            {
                query = "select customer.cid, customer.cname, customer.mobile,customer.nationality,customer.gender, customer.dob, customer.idproof,customer.address,customer.checkin, rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms  on customer.roomid= rooms.roomid where checkout is not null";
                getRecord(query);

            }
        }

        /* private void txtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
         {
             // Cập nhật câu truy vấn cho tất cả các lựa chọn
             string baseQuery = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, customer.total_amount from customer inner join rooms on customer.roomid= rooms.roomid ";

             // Tùy theo lựa chọn, thêm điều kiện WHERE phù hợp
             if (txtSearchBy.SelectedIndex == 0)
             {
                 query = baseQuery;
                 getRecord(query);
             }
             else if (txtSearchBy.SelectedIndex == 1)
             {
                 query = baseQuery + "where chekout is null";
                 getRecord(query);
             }
             else if (txtSearchBy.SelectedIndex == 2)
             {
                 query = baseQuery + "where chekout is not null";
                 getRecord(query);
             }
         }*/


        private void getRecord(string query)
        {
            DataSet ds = fn.GetData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];

        }
    }
}
