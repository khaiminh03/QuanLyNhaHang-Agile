using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class DataProvider
    {
        private static DataProvider instance; // Ctrl + R +E
        public static DataProvider Instance 
        {
            get { if (instance == null) instance = new DataProvider();
                return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        private string chuoiketnoi = "Data Source = YOUNGTEE; Initial Catalog = QuanLyNhaHang; Integrated Security = True";
       
        // data source đỏi lại cho đúng tùy máy"
        // vinh: LAPTOP-HRCJET3B
        // minh: YOUNGTEE
        // ninh: LAPTOP-MHOHQ41L\\SQLEXPRESS
        public DataTable ExecuteSQL(string sql, object[] parameter = null) // có thể bằng null
        {
            DataTable data = new DataTable();
            using (SqlConnection ketnoi = new SqlConnection(chuoiketnoi)) // tự giải phóng bộ nhớ
            {
                ketnoi.Open();
                SqlCommand lenh = new SqlCommand(sql, ketnoi);
                if ( parameter != null)
                {
                    string[] listPara = sql.Split(' '); // split theo khoảng trắng
                    int i = 0;
                    foreach (string  item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            lenh.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(lenh);
                adapter.Fill(data);
                ketnoi.Close();
            }
            return data;
        }
        // số dòng execute thành công
        public int ExecuteNonSQL(string sql, object[] parameter = null) // có thể bằng null
        {
            int data = 0;
            using (SqlConnection ketnoi = new SqlConnection(chuoiketnoi)) // tự giải phóng bộ nhớ
            {
                ketnoi.Open();
                SqlCommand lenh = new SqlCommand(sql, ketnoi);
                if (parameter != null)
                {
                    string[] listPara = sql.Split(' '); // split theo khoảng trắng
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            lenh.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data= lenh.ExecuteNonQuery();
                ketnoi.Close();
            }
            return data;
        }

        public object ExecuteScalar(string sql, object[] parameter = null) // có thể bằng null
        {
            object data = 0;
            using (SqlConnection ketnoi = new SqlConnection(chuoiketnoi)) // tự giải phóng bộ nhớ
            {
                ketnoi.Open();
                SqlCommand lenh = new SqlCommand(sql, ketnoi);
                if (parameter != null)
                {
                    string[] listPara = sql.Split(' '); // split theo khoảng trắng
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            lenh.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = lenh.ExecuteScalar();
                ketnoi.Close();
            }
            return data;
        }
    }
}
