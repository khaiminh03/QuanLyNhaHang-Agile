using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Bấm nút đăng nhập hiện lên Form fTableManager
            string tenDangNhap =txbUserName.Text;
            string matKhau = txbPassWord.Text;
            if (fLogin(tenDangNhap,matKhau)) { 

                Account loginAccount = AccountDAO.Instance.GetAccountByUserName(tenDangNhap);
                fTableManager f = new fTableManager(loginAccount);
                this.Hide();  // ẩn form Login khi bấm đăng nhập 
                f.ShowDialog(); // 
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
            }
        }
        bool fLogin (string tenDangNhap, string matKhau)
        {
            return AccountDAO.Instance.fLogin(tenDangNhap,matKhau);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Sử dụng FormClosing của Form Login để hiện lên thông báo trước khi thoát nếu chọn OK thì sẽ thoát ngược lại chọn Cancel thì sẽ không thoát
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            } 
                
        }
    }
}
