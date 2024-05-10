using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }

        public bool fLogin(string tenDangNhap, string matKhau)
        {
            //string sql = "SELECT * FROM TaiKhoan WHERE tenDangNhap = N'" + tenDangNhap + "' AND matKhau = N'" + matKhau + "'"; Sua bai cho nay
            string sql = "USP_DangNhap @tenDangNhap , @matKhau";
            DataTable ketqua = DataProvider.Instance.ExecuteSQL(sql, new object[] { tenDangNhap, matKhau }); //nay nua
            return ketqua.Rows.Count > 0;
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonSQL("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword ", new object[] { userName, displayName, pass, newPass });

            return result > 0;
        }
        public DataTable GetListAccCount()
        {
            return DataProvider.Instance.ExecuteSQL("SELECT tenDangNhap, tenHienThi, loai FROM TAIKHOAN");
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteSQL("SELECT * FROM TaiKhoan WHERE tenDangNhap = '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }
        public bool InsertAccount(string name, string displayName, int type)
        {
            string sql = string.Format("Insert dbo.TaiKhoan (tenDangNhap, tenHienThi, loai )values (N'{0}', N'{1}', {2})", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonSQL(sql);
            return result > 0;
        }

        public bool UpdateAccount(string name, string displayName, int type)
        {
            string sql = string.Format("Update dbo.TaiKhoan set tenHienThi = N'{1}', loai = {2} where tenDangNhap = N'{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonSQL(sql);
            return result > 0;
        }

        public bool DeleteAccount(string name)
        {

            string sql = string.Format("Delete TaiKhoan where tenDangNhap = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonSQL(sql);
            return result > 0;
        }
        public bool ResetPassWord(string name)
        {
            string sql = string.Format("update TaiKhoan set matKhau = N'03' where tenDangNhap = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonSQL(sql);
            return result > 0;
        }
    }
}
