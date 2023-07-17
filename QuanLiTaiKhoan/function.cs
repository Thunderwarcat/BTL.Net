using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HopNhat
{
     public  class function
    {
        //Khai báo các biến toàn cục
        //Chuỗi kết nối đến CSDL
        static String strCon = @"Data Source=DESKTOP-F41N0H5;Initial Catalog=quanlishopQuanAo;Integrated Security=True";
       static SqlConnection sqlCon = null;
      

        public static void OpenConnection()
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
        public static void CloseConnection()
        {
            //Nếu chuỗi kết nối khác rỗng và đang mở thì đóng
            if (sqlCon.State == ConnectionState.Open && sqlCon != null)
            {
                sqlCon.Close();
            }
        }
      
        //Lấy dữ liệu vào bảng
        public static DataTable GetDataToTable(string sql)
        {
            
            //Kết nối đến CSDL
            function.OpenConnection();

            //Khơi tạo DataAdapter
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Khởi tạo bảng dữ liệu
            DataTable dtTable = new DataTable();

            //Khởi tạo đối tượng thuộc chứa câu lênh truy vấn và truyền câu lệnh truy vấn
            adapter.SelectCommand = new SqlCommand(sql);

            //Kết nối adapter đến CSDL 
            adapter.SelectCommand.Connection = function.sqlCon;


            //Gửi dữ liệu được truy vấn vào bảng dữ liệu
            adapter.Fill(dtTable);

            //Trả về bảng dữ liệu
            return dtTable;
        }
        public static void FillCombo(string sql, ComboBox cb, string ma, string ten)
        {
            function.OpenConnection();
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = new SqlCommand(sql);
            dap.SelectCommand.Connection = function.sqlCon;
            DataTable table = new DataTable();

            dap.Fill(table);
            cb.DataSource = table;
            cb.ValueMember = ten; //Trường giá trị
            cb.DisplayMember = ma; //Trường hiển thị
            function.CloseConnection();
        }
        public static string ChuyenSoSangChu(string sNumber)
        {
            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText;
            //Xóa các dấu "," nếu có
            sNumber = sNumber.Replace(",", "");
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            mLen = sNumber.Length - 1; // trừ 1 vì thứ tự đi từ 0
            for (int i = 0; i <= mLen; i++)
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];
                if (mLen == i) // Chữ số cuối cùng không cần xét tiếp break; 
                    switch ((mLen - i) % 9)
                    {
                        case 0:
                            mTemp = mTemp + " tỷ";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        case 6:
                            mTemp = mTemp + " triệu";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        case 3:
                            mTemp = mTemp + " nghìn";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        default:
                            switch ((mLen - i) % 3)
                            {
                                case 2:
                                    mTemp = mTemp + " trăm";
                                    break;
                                case 1:
                                    mTemp = mTemp + " mươi";
                                    break;
                            }
                            break;
                    }
            }
            //Loại bỏ trường hợp x00
            mTemp = mTemp.Replace("không mươi không ", "");
            mTemp = mTemp.Replace("không mươi không", ""); //Loại bỏ trường hợp 00x 
            mTemp = mTemp.Replace("không mươi ", "linh "); //Loại bỏ trường hợp x0, x>=2
            mTemp = mTemp.Replace("mươi không", "mươi");
            //Fix trường hợp 10
            mTemp = mTemp.Replace("một mươi", "mười");
            //Fix trường hợp x4, x>=2
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            //Fix trường hợp x04
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            //Fix trường hợp x5, x>=2
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            //Fix trường hợp x1, x>=2
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            //Fix trường hợp x15
            mTemp = mTemp.Replace("mười năm", "mười lăm");
            //Bỏ ký tự space
            mTemp = mTemp.Trim();
            //Viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + " đồng";
            return mTemp;
        }
        //Hàm thực hiện câu lệnh SQL
        public static void RunSQL(string sql)
        {
            SqlCommand cmd; //Đối tượng thuộc lớp SqlCommand
            cmd = new SqlCommand();
            cmd.Connection = sqlCon; //Gán kết nối
            cmd.CommandText = sql; //Gán lệnh SQL
            try
            {
                cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();//Giải phóng bộ nhớ
            cmd = null;
        }


      
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, sqlCon);
            SqlDataReader reader;
             reader = cmd.ExecuteReader();
         
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }
      

    }
}
