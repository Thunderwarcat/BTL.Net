using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlinhanvien1
{
  public class DataProvider
  {
    private static DataProvider instance;

        private string connectionSTR = @"Data Source=DESKTOP-P2CSD24\SQLEXPRESS;Initial Catalog=managementShop;Integrated Security=True";

    public static DataProvider Instance
    {
      get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
      private set { DataProvider.instance = value; }
    }
    public DataTable ExecuteQuery(String query, object[] parameter = null)
    {
      DataTable dt = new DataTable();

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

        SqlDataAdapter adapter = new SqlDataAdapter(command);

        adapter.Fill(dt);

        connection.Close();
      }

      return dt;
    }

    public int ExecuteNonQuery(string query, object[] parameter = null)
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

    public object ExecuteScalar(string query, object[] parameter = null)
    {
      object data = 0;

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
