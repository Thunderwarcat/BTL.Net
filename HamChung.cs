using System;

public class Class1
{
	public Class1()
	{
        //Khai báo các biến toàn cục
        //Chuỗi kết nối đến CSDL
        static String strCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\source\repos\quanLiHoaDon\quanlinhanvien1\NhanVien.mdf;Integrated Security=True";  //Đối tượng kết nối
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

        //Hàm lấy dữ liệu vào bảng và trả về bảng
        public static DataTable GetDataToTable(String query)
        {
            //Kết nối đến CSDL
            DbConnection.OpenConnection();

            //Khơi tạo DataAdapter
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Khởi tạo bảng dữ liệu
            DataTable dtTable = new DataTable();

            //Khởi tạo đối tượng thuộc chứa câu lênh truy vấn và truyền câu lệnh truy vấn
            adapter.SelectCommand = new SqlCommand(query);

            //Kết nối adapter đến CSDL 
            adapter.SelectCommand.Connection = DbConnection.sqlCon;


            //Gửi dữ liệu được truy vấn vào bảng dữ liệu
            adapter.Fill(dtTable);

            //Trả về bảng dữ liệu
            return dtTable;
        }
}
