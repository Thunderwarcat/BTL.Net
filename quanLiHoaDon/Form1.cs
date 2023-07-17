using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using quanLiHoaDon;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace quanLiHoaDon
{
    public partial class HoaDon : Form
    {
        public HoaDon()
        {
            InitializeComponent();
        }
        DataTable Data;

        private void Form1_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnInHoaDon.Enabled = false;
            txtMaHD.ReadOnly = true;
            txtTenNV.ReadOnly = true;
         
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtGiamGia.Text = "";
            txtTongTien.Text = "";

            string a;
            function.FillCombo("SELECT * from NhanVien", cbMaNV, "MaNV", "TenNV");
            cbMaNV.SelectedIndex = -1;
            function.FillCombo("SELECT MaHang, TenHang FROM MatHang", cbMaMH, "MaHang", "TenHang");
            cbMaMH.SelectedIndex = -1;
          

            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHD.Text != "")
            {
                load_inforHoadon();
                btnHuy.Enabled = true;
                btnInHoaDon.Enabled = true;
            }
            LoadDataGridView();
        }
        private void load_inforHoadon()
        {
            string str;

            str = "SELECT MaNhanVien FROM tblHDBan WHERE MaHDBan = N'" + txtMaHD.Text + "'";
            cbMaNV.Text = function.GetFieldValues(str);

            str = "SELECT TongTien FROM tblHDBan WHERE MaHDBan = N'" + txtMaHD.Text + "'";
            txtTongTien.Text = function.GetFieldValues(str);
            lbBangChu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(txtTongTien.Text);
        }
        public void Resetdata()
        {
            txtMaHD.Text = "";
            dtNgayBan.Text = DateTime.Now.ToShortDateString();
            cbMaNV.Text = "";
            txtTongTien.Text = "0";
            lbBangChu.Text = "Bằng chữ: ";
            txtSoLuong.Text = "";
            cbMaMH.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }


        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT a.MaHang, b.TenHang, a.SoLuongMua, b.DonGia, " +
                "a.ThanhTien FROM ChiTietHD a, MatHang AS b WHERE a.MaHD = N'"
                + txtMaHD.Text + "' AND a.MaHang=b.MaHang";
            Data = function.GetDataToTable(sql);
            dataGridView1.DataSource = Data;

        }
        private void ResetValuesHang()
        {
            cbMaMH.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
            
        }

     
        private void txtTenHang_TextChanged(object sender, EventArgs e)
        {
         
        }


        private void cbMaNV_TextChanged(object sender, EventArgs e)
        {
           
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra

            if (cbMaNV.Text == "")
            {
                txtTenNV.Text = "";
            }
            else
            {
                txtTenNV.Text = cbMaNV.SelectedValue.ToString();

            }
           
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Do you want to leave ??", "notice", MessageBoxButtons.YesNo);
            if(re == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnLuu.Enabled = true;
            btnInHoaDon.Enabled = false;
            btnThem.Enabled = false;
            txtMaHD.Enabled = true;
            Resetdata();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        
        {
            string sql;
            double sl, SLcon;
           
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
               
                if (cbMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbMaNV.Focus();
                    return;
                }
              
               string sqll = "INSERT INTO HoaDon(MaHD, NgayBan, MaNV,  TongTien) VALUES (N'" + txtMaHD.Text.Trim() + "','"+dtNgayBan.Value+"','" + cbMaNV.Text + "', '" + txtTongTien.Text + ")";
                function.RunSQL(sqll);

            // Lưu thông tin của các mặt hàng
            if (cbMaMH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaMH.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
           
          
            if (cbMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã Nhân Viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaNV.Focus();
                return;
            }
          
            if (txtMaHD.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã HD ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHD.Focus();
                return;
            }
         
            if (txtDonGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Gia  ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGia.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
             sl = Convert.ToDouble(function.GetFieldValues("SELECT SoLuong FROM MatHang"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = "INSERT INTO ChiTietHD(MaHD,MaHang,SoLuongMua,DonGia,ThanhTien) " + 
                "VALUES(N'" + txtMaHD.Text.Trim() + "',N'" + cbMaMH + "'," + txtSoLuong.Text + "," + txtDonGia.Text + "," + txtThanhTien.Text + ")";
            function.RunSQL(sql);
            LoadDataGridView();

            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE MatHang SET SoLuong =" + SLcon + " WHERE MaHang= N'" + cbMaMH.SelectedValue + "'";
            function.RunSQL(sql);
            // Cập nhật lại tổng tiền cho hóa đơn bán
            double tong = double.Parse(function.GetFieldValues("SELECT TongTien FROM HoaDon WHERE MaHD = N'" + txtMaHD.Text + "'"));
            double Tongmoi = tong + double.Parse(txtThanhTien.Text);
            sql = "UPDATE HoaDon SET TongTien =" + Tongmoi + " WHERE MaHD = N'" + txtMaHD.Text + "'";
            function.RunSQL(sql);
            txtTongTien.Text = Tongmoi.ToString();
            lbBangChu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(Tongmoi.ToString());
            ResetValuesHang();
           
            btnThem.Enabled = true;
            btnInHoaDon.Enabled = true;
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void cbMaMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMaMH.Text == "")
            {
                txtTenHang.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            string MaMH = cbMaMH.Text;
            string sql = $"select * from MatHang where MaHang = '{MaMH}'";
            Data = function.GetDataToTable(sql);
            foreach (DataRow item in Data.Rows)
            {
                txtTenHang.Text = item["TenHang"].ToString();
                txtDonGia.Text = item["DonGia"].ToString();
            }
        }
    }
}
