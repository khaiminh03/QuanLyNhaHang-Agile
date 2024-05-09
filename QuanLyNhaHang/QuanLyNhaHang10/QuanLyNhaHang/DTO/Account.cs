using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Account
    {
        public Account(string userName, string displayName, int type, string password = null)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.Type = type;
            this.Password = password;
        }
        public Account(DataRow row)
        {
            this.UserName = row["tenDangNhap"].ToString();
            this.DisplayName = row["tenHienThi"].ToString();
            this.Type = (int)row["loai"];
            this.Password = row["matKhau"].ToString();
        }
        private int type;
        private string displayName;
        private string password;
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
