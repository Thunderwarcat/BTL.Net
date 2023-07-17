using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiTaiKhoan
{
  public class DataProvider
  {
    private static DataProvider instance;

    private string connectionSTR = @"Data Source=DESKTOP-F41N0H5;Initial Catalog=quanlishopQuanAo;Integrated Security=True";

    // su dung mau thiet ke singleton dam bao lop DaTaProvider chi co 1 the hien(instance) duy nhat
    public static DataProvider Instance
    {
      get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
      private set { DataProvider.instance = value; }
    }
    // do du lieu ra bang
    public DataTable ExecuteQuery(String query, object[] parameter = null)//mang doi tuong(object)  nhieu tham so(parameter)
    {
      DataTable dt = new DataTable();
      // du lieu trong using tu dong giai phong
      using (SqlConnection connection = new SqlConnection(connectionSTR))
      {
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);

        if (parameter != null)
        {
          string[] listPara = query.Split(' ');// tach(split ) mang(listPara) theo khoang trong ' ' 
          int i = 0;
        // dung vong lap de them dc nhieu tham so(parameter) hon
          foreach (string item in listPara)
          {
            if (item.Contains('@'))// chua tham so(parameter) boi @
            {
              command.Parameters.AddWithValue(item, parameter[i]);
              i++;
            }
          }
        }

        SqlDataAdapter adapter = new SqlDataAdapter(command);

        adapter.Fill(dt);

        connection.Close();
      }

      return dt;
    }

    // them  xoa 
    public int ExecuteNonQuery(string query, object[] parameter = null)// int  tra ve so dong thanh cong 
    {
      int data = 0;

      using (SqlConnection connection = new SqlConnection(connectionSTR))
      {
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        if (parameter != null)
        {
          string[] listPara = query.Split(' ');
          int i = 0;
          foreach (string item in listPara)
          {
            if (item.Contains('@'))
            {
              command.Parameters.AddWithValue(item, parameter[i]);
              i++;
            }
          }
        }

        data = command.ExecuteNonQuery();
        connection.Close();
      }

      return data;
    }
    // dem so luong 
    public object ExecuteScalar(string query, object[] parameter = null)
    {
      object data = 0; // doi tuong rong

      using (SqlConnection connection = new SqlConnection(connectionSTR))
      {
        connection.Open();


        SqlCommand command = new SqlCommand(query, connection);

        if (parameter != null)
        {
          string[] listPara = query.Split(' ');
          int i = 0;
          foreach (string item in listPara)
          {
            if (item.Contains('@'))
            {
              command.Parameters.AddWithValue(item, parameter[i]);
              i++;
            }
          }
        }

        data = command.ExecuteScalar();

        connection.Close();
      }

      return data;
    }

  }
}
