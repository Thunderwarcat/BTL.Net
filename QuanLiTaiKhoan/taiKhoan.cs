using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLiTaiKhoan;


namespace QuanLiTaiKhoan
{
    public partial class taiKhoan : Form
    {
        public taiKhoan()
        {
            InitializeComponent();
        }

      

        private void taiKhoan_Load(object sender, EventArgs e)
        {
            Load_TaiKhoan();
            reset_data();
        }
        //load du lieu len form tai khoan
        private void Load_TaiKhoan()
        {
            string sql = "select * from TaiKhoan";
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery(sql);
            dtTaiKhoan.DataSource = dt;
            txtTenTK.Focus();

        }
        private void reset_data()
        {
            txtMaNV.Text = txtMatKhau.Text = txtTenTK.Text = txtQuyen.Text = " ";
         
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            try
            {
                string sql = "INSERT INTO TaiKhoan(TenTaiKhoan,MatKhau,MaNV, Quyen) VALUES (N'" + txtTenTK.Text.ToString().Trim() + "','" 
                    + txtMatKhau.Text.ToString().Trim() + "','" + txtMaNV.Text.ToString().Trim() + "','" 
                    + txtQuyen.Text.ToString().Trim() + "')";

              
                    DataProvider.Instance.ExecuteNonQuery(sql);
                    MessageBox.Show("da them thanh cong");
                    Load_TaiKhoan();
                    reset_data();
                
            }
            catch
            {
                MessageBox.Show("chua chon Ma Nhan vien :");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNV.Text;
            string sql = $"Delete TaiKhoan where MaNV = N'{maNhanVien}'";

            try
            {
                if (txtMaNV.Text == " ")
                {
                    MessageBox.Show("chua chon MaNV de xoa");

                    txtMaNV.Focus();
                }
                else
                {
                    DataProvider.Instance.ExecuteNonQuery(sql);
                    MessageBox.Show("Da xoa thanh cong");

                    Load_TaiKhoan();
                    reset_data();
                }
            }
            catch (Exception)
            {
                if (maNhanVien == null)
                    MessageBox.Show("Loi");
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "UPDATE TaiKhoan SET TenTaiKhoan = '" + txtTenTK.Text.Trim().ToString() + "' ,MatKhau=N'" + txtMatKhau.Text.Trim().ToString() +
               "', Quyen='" + txtQuyen.Text.ToString().Trim() + "' where MaNV='" 
               + txtMaNV.Text.ToString().Trim() + "'";

                if (txtTenTK.Text == " ")
                {
                    MessageBox.Show("Ban chua dien  Ten Tai Khoan :");
                    txtTenTK.Focus();
                }
                if (txtQuyen.Text == " ")
                {
                    MessageBox.Show("Ban chua phan Quyen :");
                    txtQuyen.Focus();
                }
                DataProvider.Instance.ExecuteNonQuery(sql);
                MessageBox.Show("Sua thanh cong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                reset_data();
                Load_TaiKhoan();

            }
            catch (Exception)
            {

                MessageBox.Show("Loi", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            reset_data();
            txtMaNV.Enabled = true;
        }

        private void dtTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int i = dtTaiKhoan.CurrentRow.Index;
            txtTenTK.Text = dtTaiKhoan.Rows[i].Cells[0].Value.ToString();

            txtMatKhau.Text = dtTaiKhoan.Rows[i].Cells[1].Value.ToString();
            txtMaNV.Text = dtTaiKhoan.Rows[i].Cells[2].Value.ToString();
            txtQuyen.Text = dtTaiKhoan.Rows[i].Cells[3].Value.ToString().Trim();
            txtMaNV.Enabled = false;
        }

        private void dtTaiKhoan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dtTaiKhoan.CurrentRow.Index;
            txtTenTK.Text = dtTaiKhoan.Rows[i].Cells[0].Value.ToString();

            txtMatKhau.Text = dtTaiKhoan.Rows[i].Cells[1].Value.ToString();
            txtMaNV.Text = dtTaiKhoan.Rows[i].Cells[2].Value.ToString();
            txtQuyen.Text = dtTaiKhoan.Rows[i].Cells[3].Value.ToString().Trim();
            txtMaNV.Enabled = false;
        }
    }
}
