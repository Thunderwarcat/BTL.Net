using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiTaiKhoan
{
    public partial class Test : Form
    {
        string Username = "", Pass = "", MaNV = "", Quyen = "";
        public Test()
        {
            InitializeComponent();
        }
        // tao doi tuong Test de phan quyen
        public Test(string Username , string Pass , string MaNV , string Quyen)
        {
            InitializeComponent();
            this.Username = Username;
            this.Pass = Pass;
            this.MaNV = MaNV;
            this.Quyen = Quyen;
        }

        private void mnuManageStaff_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.ShowDialog();
        }

        private void mnuProfit_Click(object sender, EventArgs e)
        {
            ThongKe tk = new ThongKe();
            tk.ShowDialog();
        }

        private void taoHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            hd.ShowDialog();
        }

        private void quanLiTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taiKhoan tk = new taiKhoan();
            tk.ShowDialog();
            
        }

        private void Test_Load(object sender, EventArgs e)
        {
            PhanQuyen();
        }
        // phan quyen su dung cho nhan vien
        private void PhanQuyen()
        {
            if(Quyen.Trim() == "1" )
            {
                nhânViênToolStripMenuItem.Enabled = false;
                taiKhoanToolStripMenuItem.Enabled = false;
            }

        }
    }
}
