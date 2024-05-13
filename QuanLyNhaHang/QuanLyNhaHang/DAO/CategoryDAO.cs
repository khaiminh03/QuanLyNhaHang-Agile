using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }
        public List<Category> GetListCaterogy()
        {
            List<Category> list = new List<Category>();
            string sql = "SELECT * FROM DanhMucMonAn";
            DataTable data = DataProvider.Instance.ExecuteSQL(sql);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }
            return list;
        }
        public Category GetCategoryByID(int id)
        {
            Category category = null;
            string query = "select * from DanhMucMonAn where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteSQL(query);
            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }

            return category;
        }
        public DataTable GetListDanhMuc()
        {
            return DataProvider.Instance.ExecuteSQL("SELECT * FROM DanhMucMonAn");
        }

        public bool InsertDanhMuc(string name)
        { 
            string query = string.Format("Insert dbo.DanhMucMonAn (tenDanhMuc) values (N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonSQL(query);
            return result > 0;
        }

        public bool UpdateDanhMuc(int id,string name)
        {
            string query = string.Format("Update dbo.DanhMucMonAn set tenDanhMuc = N'{0}' where id = {1}", name, id);
            int result = DataProvider.Instance.ExecuteNonSQL(query);
            return result > 0;
        }

        public bool DeleteDanhMuc(int idDanhMuc)
        {
            BillInfoDAO.Instance.DeleteBillInforByFoodID(idDanhMuc);
            string query = string.Format("Delete DanhMucMonAn where id = {0}", idDanhMuc);
            int result = DataProvider.Instance.ExecuteNonSQL(query);
            return result > 0;
        }
    }
}

    

