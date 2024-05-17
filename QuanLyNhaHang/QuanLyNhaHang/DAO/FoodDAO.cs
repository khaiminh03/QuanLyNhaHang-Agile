using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string sql = "SELECT * FROM MonAn where idDanhMuc =" + id;
            DataTable data = DataProvider.Instance.ExecuteSQL(sql);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();
            string sql = "SELECT id, tenMonAn, giaMonAn, idDanhMuc FROM MonAn ";
            DataTable data = DataProvider.Instance.ExecuteSQL(sql);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string sql = string.Format("SELECT * FROM MonAn WHERE tenMonAn like N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExecuteSQL(sql);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        
        public bool InsertFood(string name, int id, float price)
        {
            string query = string.Format("Insert dbo.MonAn (tenMonAn, idDanhMuc, giaMonAn)values (N'{0}', {1}, {2})", name, id, price);
            int result = DataProvider.Instance.ExecuteNonSQL(query);
            return result > 0;
        }

        public bool UpdateFood(string name, int id, float price, int idFood)
        {
            string query = string.Format("Update dbo.MonAn set tenMonAn = N'{0}', idDanhMuc = {1}, giaMonAn = {2} where id = {3}", name, id, price, idFood);
            int result = DataProvider.Instance.ExecuteNonSQL(query);
            return result > 0;
        }

        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInforByFoodID(idFood);

            string query = string.Format("Delete MonAn where id = {0}",idFood);
            int result = DataProvider.Instance.ExecuteNonSQL(query);
            return result > 0;
        }

        public bool DeleteFoodByDM(int dm)
        {
            BillInfoDAO.Instance.DeleteBillInforByFoodID(dm);

            string query = string.Format("Delete MonAn where idDanhMuc = {0}", dm);
            int result = DataProvider.Instance.ExecuteNonSQL(query);
            return result > 0;
        }
    }
}
