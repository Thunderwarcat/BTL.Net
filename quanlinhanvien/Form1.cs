﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace quanlinhanvien1
{
  public partial class NhanVien : Form
  {
    public NhanVien()
    {
      InitializeComponent();
    }
    //chuoi ket noi
    string strCon = @"Data Source=DESKTOP-F41N0H5;Initial Catalog=quanlishopQuanAo;Integrated Security=True";
    // doi tuong ket noi
    SqlConnection sqlCon = null;
    SqlDataAdapter ada = null;
    DataSet ds = null;

   
    //ham hien thi danh sach nhan vien
    private void dtgNhanVien_load()
    {
            
            string sql = "select * from NhanVien";
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery(sql);
            dataGridView1.DataSource = dt;
           

    }
    /*private void label1_Click(object sender, EventArgs e)
    {

    }
*/

    private void Delete_Data()
    {
      txtDiaChi.Text = txtMaNV.Text = txtTenNV.Text = "";
      mstSDT.Text = "";
            txtTimKiem.Text = " ";
            dtNgaySinh.Value = DateTime.Now;
    }


    private void Form1_Load(object sender, EventArgs e)
    {
      txtMaNV.Enabled = false;
      btnSave.Enabled = false;
      btnDelete.Enabled = false;
      btnUpdate.Enabled = false;
      dtgNhanVien_load();
      Delete_Data();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {

      btnSave.Enabled = true;
      btnUpdate.Enabled = false;
      btnDelete.Enabled = false;
      btnAdd.Enabled = false ;

      Delete_Data();
      txtMaNV.Enabled = true;
      txtMaNV.Focus();
      btnSkip.Enabled = true;
      btnSkip.Enabled = true;

    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      string gt;
      string sql;
            string ma = txtMaNV.Text;
      if (ma.Trim().Length == 0)
      {
        MessageBox.Show("Bạn phải nhập mã Nhân Viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtMaNV.Focus();
        return;
      }
      if(ma.Trim().Length >= 10 )
            {
                MessageBox.Show("Ma Nhan Vien Khong hop le ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            string TenNV = txtTenNV.Text;
      if (txtTenNV.Text.Trim().Length == 0)
      {
        MessageBox.Show("Bạn phải nhập tên Nhân Viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtTenNV.Focus();
        return;
      }
      if(TenNV.Contains('@'))
            {
                MessageBox.Show("Ten Nhan Vien khong hop le", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNV.Focus();
                return;
            }
      if (txtDiaChi.Text.Trim().Length == 0)
      {
        MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtDiaChi.Focus();
        return;
      }
      if (mstSDT.Text == "(  )    -")
      {
        MessageBox.Show("Bạn phải nhập Số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        mstSDT.Focus();
        return;

      }
      if(mstSDT.Text.Length >= 13)
            {
                MessageBox.Show("So Dien thoai khong hop le", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mstSDT.Focus();
                return;
            }


      if (ckbGioi.Checked == true)
        gt = "Nam";
      else
        gt = "Nữ";


      sql = "INSERT INTO NhanVien(MaNV,TenNV,GioiTinh, DiaChiNV,LienLacNV, NgaySinhNV) VALUES (N'" + txtMaNV.Text.Trim() + "',N'" + txtTenNV.Text.Trim() + "',N'" + gt + "',N'" + txtDiaChi.Text.Trim() + "','" + mstSDT.Text + "','" + dtNgaySinh.Value + "')";

      DataProvider.Instance.ExecuteNonQuery(sql);
      MessageBox.Show("Them Nhan Vien Thanh cong");
    
      LamMoi();

    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

   private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      int i = dataGridView1.CurrentRow.Index;
      txtMaNV.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();

      string gt = dataGridView1.Rows[i].Cells[2].Value.ToString();
      if (gt.ToString() == "Nam")
      {
                ckbGioi.Checked = true;
      }
      else
            {
                ckbGioi.Checked = false;
            }


      txtTenNV.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
      mstSDT.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
      txtDiaChi.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
      dataGridView1.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";
      dtNgaySinh.Value = (DateTime)dataGridView1.Rows[i].Cells[5].Value;
      


      btnDelete.Enabled = true;
      btnSkip.Enabled = true;
      btnUpdate.Enabled = true;
      txtMaNV.Enabled = true;
      
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      string maNhanVien = txtMaNV.Text;
      string sql = $"Delete NhanVien where MaNV = N'{maNhanVien}'";
      try
      {
        DataProvider.Instance.ExecuteNonQuery(sql);
        MessageBox.Show("Da xoa thanh cong");
      }
      catch (Exception)
      {
        MessageBox.Show("Chua chon nhan vien can xoa");
      }
      
      LamMoi();

    }

    private void btnTimKiem_Click(object sender, EventArgs e)
    {
      string maNV = txtTimKiem.Text.Trim();
            if (txtTimKiem.Text == " ")
            {
                MessageBox.Show("Bạn phải nhập Ma Nhan Vien", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTimKiem.Focus();
                return;

            }
           DataTable dt = DataProvider.Instance.ExecuteQuery($"Select * from NhanVien where MaNV = N'{maNV}'");

                foreach (DataRow item in dt.Rows)
                {
                    txtMaNV.Text = item["MaNV"].ToString();
                    txtTenNV.Text = item["TenNV"].ToString();
                    dtNgaySinh.Value = (DateTime)item["NgaySinhNV"];
                    txtDiaChi.Text = item["DiaChiNV"].ToString();
                    mstSDT.Text = item["LienLacNV"].ToString();


                    if (item["GioiTinh"].ToString().Equals("Nam"))
                    {
                        ckbGioi.Checked = true;
                    }
                
                btnSkip.Enabled = true;
                }
            dataGridView1.DataSource = dt;
           

            //select* from NhanVien where MaNV like N'%nv00%'
        }

        private void btnSkip_Click(object sender, EventArgs e)
    {
      LamMoi();
    }

    void LamMoi()
    {
      dtgNhanVien_load();
      Delete_Data();
      btnSkip.Enabled = true;
      btnAdd.Enabled = true;
      btnDelete.Enabled = false;
      btnUpdate.Enabled = false;
      btnSave.Enabled = false;
      txtMaNV.Enabled = false;
      ckbGioi.CheckState = CheckState.Unchecked;
    }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (mstSDT.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mstSDT.Focus();
                return;
            }
           
            if (ckbGioi.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";

            sql = "UPDATE NhanVien SET  TenNV=N'" + txtTenNV.Text.Trim().ToString() +
                    "',DiaChiNV=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',LienLacNV='" + mstSDT.Text.ToString() + "',GioiTinh=N'" + gt +
                    "',NgaySinhNV='" + dtNgaySinh.Value +
                    "' WHERE MaNV=N'" + txtMaNV.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(sql);
            LamMoi();
        }

        private void ckbGioi_CheckedChanged(object sender, EventArgs e)
        {

            ckbGioi.Checked = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            txtMaNV.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();

            string gt = dataGridView1.Rows[i].Cells[2].Value.ToString();
            if (gt.ToString() == "Nam")
            {
                ckbGioi.Checked = true;
            }
            else
            {
                ckbGioi.Checked = false;
            }


            txtTenNV.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            mstSDT.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtDiaChi.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            dataGridView1.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";
            dtNgaySinh.Value = (DateTime)dataGridView1.Rows[i].Cells[5].Value;



            btnDelete.Enabled = true;
            btnSkip.Enabled = true;
            btnUpdate.Enabled = true;
            txtMaNV.Enabled = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
