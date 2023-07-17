using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLiTaiKhoan;
using System.Data.SqlClient;

namespace QuanLiTaiKhoan
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        //tao chuoi ket noi
        string strCon = "Data Source=DESKTOP-F41N0H5;Initial Catalog=quanlishopQuanAo;Integrated Security=True";
        //tao doi tuong ket noi
        SqlConnection sqlCon = null;

        //Mo ket noi
        public void OpenConnection()
        {
            //Nếu rỗng thì tạo đối tượng kết nối và truyền chuỗi kết nối
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            //Nếu đóng thì mở
            if (sqlCon.State != ConnectionState.Open)
            {
                sqlCon.Open();
            }
        }
        //Hàm đóng kết nối
        public void CloseConnection()
        {
            //Nếu chuỗi kết nối khác rỗng và đang mở thì đóng
            if (sqlCon.State == ConnectionState.Open && sqlCon != null)
            {
                sqlCon.Close();
            }
        }
        //thoat form
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to leave ??", "Notice", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        // dang nhap click
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenConnection();
                string username = txtUser.Text;
                string pass = txtPass.Text;
                
                string sql = "select * from TaiKhoan where TenTaiKhoan = '" + username + "' and MatKhau = '" + pass + "'";
             
                DataTable dt = new DataTable();
               dt= DataProvider.Instance.ExecuteQuery(sql);
                if(dt.Rows.Count >0)
                {
                    MessageBox.Show("suscces");
                    // gan thuoc tinh trong bang voi cac bien tuong ung trong doi tuong Test
                    Test te = new Test(dt.Rows[0][0].ToString(), 
                        dt.Rows[0][1].ToString(),dt.Rows[0][2].ToString(),dt.Rows[0][3].ToString());
                    te.ShowDialog();
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("role");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi");
            }
        }
        // ham hien thi mat khau
        private void ckPass_CheckedChanged(object sender, EventArgs e)
        {
            if(ckPass.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }
       

        private void txtUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }


        private void btnLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
    }
}
