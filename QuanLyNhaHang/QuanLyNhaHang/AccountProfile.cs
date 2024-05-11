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
    public partial class AccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value;}
        }

        public AccountProfile(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        private void bntExitt_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }
        public class AccountEvent:EventArgs
        {
            private Account acc;

            public Account Acc 
            { 
                get { return acc; } 
                set {  acc = value; }
            }
            public AccountEvent(Account acc)
            {
                this.Acc = acc;
            }
        }
    }
}
