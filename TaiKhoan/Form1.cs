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

namespace TaiKhoan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      

        private void reset_data()
        {
            txtMaNV.Text = txtMatKhau.Text = txtQuyen.Text = txtTenTK.Text = "";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            txtTenTK.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();

            txtMatKhau.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtMaNV.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtQuyen.Text = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();

        }

        private void load_DataGrid()
        {
           
            string sql = "select * from TaiKhoan";
            DataTable dt = new DataTable();
            dt =  DataProvider.Instance.ExecuteQuery(sql);
            dataGridView1.DataSource = dt;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_DataGrid();
            reset_data();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "INSERT INTO TaiKhoan(TenTaiKhoan,MatKhau,MaNV, Quyen) VALUES (N'" + txtTenTK.Text + "','" + txtMatKhau.Text + "','" + txtMaNV.Text + "','" + txtQuyen.Text + "')";
                DataProvider.Instance.ExecuteNonQuery(sql);
                MessageBox.Show("da them thanh cong");
                load_DataGrid();
                reset_data();

            }
            catch
            {
                MessageBox.Show("chua co Nhan Vien de them tai khoan");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNV.Text;
            string sql = $"Delete TaiKhoan where MaNV = N'{maNhanVien}'";

            try
            {
                if (txtMaNV.Text == "")
                {
                    MessageBox.Show("chua chon MaNV de xoa");

                    txtMaNV.Focus();
                }
                else
                {
                    DataProvider.Instance.ExecuteNonQuery(sql);
                    MessageBox.Show("Da xoa thanh cong");

                    load_DataGrid();
                    reset_data();
                }
            }
            catch (Exception)
            {
                if(maNhanVien == null)
                MessageBox.Show("Loi");
            }
              

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
           try
            {
                string sql = "UPDATE TaiKhoan SET TenTaiKhoan = '"+txtTenTK.Text.Trim().ToString()+"' ,MatKhau=N'" + txtMatKhau.Text.Trim().ToString() +
               "', Quyen='" + txtQuyen.Text.ToString() + "' where MaNV='" + txtMaNV.Text.ToString() + "'";
                DataProvider.Instance.ExecuteNonQuery(sql);
                MessageBox.Show("Sua thanh cong", "thong bao",MessageBoxButtons.OK, MessageBoxIcon.Information) ;

                txtMaNV.Enabled = true;
                reset_data();
                load_DataGrid();

            }
            catch (Exception)
            {

                MessageBox.Show("Loi", "error",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnskip_Click(object sender, EventArgs e)
        {
            reset_data();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            txtTenTK.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();

            txtMatKhau.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtMaNV.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtQuyen.Text = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
            txtMaNV.Enabled = false;
        }
    }

}
