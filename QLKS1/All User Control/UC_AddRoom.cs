using ClosedXML.Excel;
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
                Int64 priceperhour = Int64.Parse(txtPricePerHour.Text);



                query = "insert into rooms (roomNo, roomType, bed, price, price_per_hour) values ('" + roomno + "','" + type + "','" + bed + "'," + price + "','" + priceperhour + ",'NO')";
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
                txtPricePerHour.Text = row.Cells[5].Value.ToString();
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

        private void btnExel_Click(object sender, EventArgs e)
        {
            if (DataGridView1.Rows.Count > 0)
            {
               
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                sfd.FileName = "DanhSachPhong_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            
                            var ws = wb.Worksheets.Add("Danh sách phòng");

                           
                            for (int i = 0; i < DataGridView1.Columns.Count; i++)
                            {
                                ws.Cell(1, i + 1).Value = DataGridView1.Columns[i].HeaderText;
                            }

                            for (int i = 0; i < DataGridView1.Rows.Count; i++)
                            {
                             
                                if (DataGridView1.Rows[i].IsNewRow) continue;

                                for (int j = 0; j < DataGridView1.Columns.Count; j++)
                                {
                                    ws.Cell(i + 2, j + 1).Value = DataGridView1.Rows[i].Cells[j].Value?.ToString() ?? "";
                                }
                            }
                            
                            var titleRange = ws.Range(1, 1, 1, DataGridView1.Columns.Count);
                            titleRange.Merge();

                            ws.Cell(1, 1).Value = "Danh Sách Phòng";
                            ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(1, 1).Style.Font.Bold = true;
                            ws.Cell(1, 1).Style.Font.FontSize = 16;
                            for (int i = 0; i < DataGridView1.Columns.Count; i++)
                            {
                                ws.Cell(2, i + 1).Value = DataGridView1.Columns[i].HeaderText;
                            }

                            
                            for (int i = 0; i < DataGridView1.Rows.Count; i++)
                            {
                                if (DataGridView1.Rows[i].IsNewRow) continue;

                                for (int j = 0; j < DataGridView1.Columns.Count; j++)
                                {
                                
                                    ws.Cell(i + 3, j + 1).Value = DataGridView1.Rows[i].Cells[j].Value?.ToString() ?? "";
                                }
                            }


                            var rangeWithData = ws.Range(1, 1, DataGridView1.Rows.Count + 1, DataGridView1.Columns.Count);
                            rangeWithData.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            rangeWithData.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            rangeWithData.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            rangeWithData.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            rangeWithData.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                            ws.Columns().AdjustToContents();

                            wb.SaveAs(sfd.FileName);

                            MessageBox.Show("Dữ liệu đã được xuất ra Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xuất file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    }
    


