using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyNhaHang
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();  //create footList
        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            load_2();
            //dgvAccount.DataSource = DataProvider.Instance.ExecuteSQL("SELECT * FROM dbo.TaiKhoan WHERE tenDangNhap = N'' OR 1=1--"); nay de hien thi danh sach tai khoan
        }
        #region methods
        BindingSource listDanhMuc = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountList = new BindingSource(); 
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);
            return listFood;
        }
        new void load_2()
        {
            dgvCategory.DataSource = listDanhMuc;
            dgvTable.DataSource = tableList;
            dgvAccount.DataSource = accountList;
            dgvFood.DataSource = foodList; // value of dgvFood -> footList 
            LoadAccount();
            LoadDanhMuc();
            LoadDateTimePicker();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadTable();
            LoadCategoryIntoCombobox(cbFoodCategory);
            AddFoodBinding();
            AddAccountBiding();
            AddTableBiding();
            AddDanhMucBinding();


        }
        void AddDanhMucBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
            textBox2.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "tenDanhMuc", true, DataSourceUpdateMode.Never));
        }
        void AddTableBiding()
        {
            textBox1.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            textBox3.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "tenBAN", true, DataSourceUpdateMode.Never));
            textBox4.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "trangThai", true, DataSourceUpdateMode.Never));
        }
        void AddAccountBiding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "tenDangNhap", true, DataSourceUpdateMode.Never));
            txbDisPlayName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "tenHienThi", true, DataSourceUpdateMode.Never));
            nbupAccountType.DataBindings.Add(new Binding("Value", dgvAccount.DataSource, "loai", true, DataSourceUpdateMode.Never));

        }
        void LoadDanhMuc()
        {
            listDanhMuc.DataSource = CategoryDAO.Instance.GetListDanhMuc();
        }
        void LoadTable()
        {
            tableList.DataSource = TableDAO.Instance.GetListTable();
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccCount();
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgvBill.DataSource =  BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void AddFoodBinding()//ky thuat binding (a thay doi <-> b thay doi)
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never)); // DataSourceUpdateMode.Never: Chỉ cho phép thay đổi 1 chiều
            txbFoodID.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCaterogy();
            cb.DisplayMember = "Name";
        }
        #endregion

        #region event
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void dgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            String name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price) && (float)nmFoodPrice.Value>0)
            {
                MessageBox.Show("Thêm món thành công!");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã có lỗi. Thêm món ăn không thành công!");
            }
        }

        private void btnEditFoof_Click(object sender, EventArgs e)
        {
            String name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(name, categoryID, price, id) && (float)nmFoodPrice.Value > 0)
            {
                MessageBox.Show("Sữa món thành công!");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã có lỗi. Sữa món ăn không thành công!");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công!");
                LoadListFood();
                if(deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã có lỗi. Xóa món ăn không thành công!");
            }
        }

        private event EventHandler insertDanhMuc;
        public event EventHandler InsertDanhMuc
        {
            add { insertDanhMuc += value; }
            remove { insertDanhMuc -= value; }
        }
        private event EventHandler deleteDanhMuc;
        public event EventHandler DeleteDanhMuc
        {
            add { deleteDanhMuc += value; }
            remove { deleteDanhMuc -= value; }
        }
        private event EventHandler updateDM;
        public event EventHandler UpdateDM
        {
            add { updateDM += value; }
            remove { updateDM -= value; }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text); ;
        }

        private void txbSearchFoodName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }
        void AddAcount(string userName,string displayName, int type)
        {
            if(AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            } 
            LoadAccount();
            
        }
        void EditAcount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }
            LoadAccount();

        }
        void DeleteAcount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Không thể xóa chính bạn");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }
            LoadAccount();

        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassWord(userName))
            {
                MessageBox.Show("Đổi mật khẩu  thành công");
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu  thất bại");
            }
            
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName =txbUserName.Text;
            string displayName = txbDisPlayName.Text;
            int type = (int)nbupAccountType.Value;

            AddAcount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            DeleteAcount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisPlayName.Text;
            int type = (int)nbupAccountType.Value;

            EditAcount(userName, displayName, type);

        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            ResetPass(userName);
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadDanhMuc();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;

            if (CategoryDAO.Instance.InsertDanhMuc(name))
            {
                MessageBox.Show("Thêm danh mục thành công!");
                LoadDanhMuc();
                LoadListFood();
                LoadCategoryIntoCombobox(cbFoodCategory);
                if (insertDanhMuc!= null)
                    insertDanhMuc(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã có lỗi. Thêm danh mục không thành công!");
            }
            
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            // xoa mon
            int id = Convert.ToInt32(txbCategoryID.Text);
            if (FoodDAO.Instance.DeleteFoodByDM(id))
            {
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }

            // xoa danh muc
            if (CategoryDAO.Instance.DeleteDanhMuc(id))
            {
                MessageBox.Show("Xóa danh mục thành công!");
                LoadDanhMuc();
                LoadListFood();
                LoadCategoryIntoCombobox(cbFoodCategory);
                if (deleteDanhMuc != null)
                    deleteDanhMuc(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã có lỗi. Xóa danh mục không thành công!");
            }

        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            string name = textBox2.Text;
            if (CategoryDAO.Instance.UpdateDanhMuc(id,name))
            {
                MessageBox.Show("Cập nhật danh mục thành công");
                LoadDanhMuc();
                LoadListFood();
                LoadCategoryIntoCombobox(cbFoodCategory);
                if (updateDM != null)
                    updateDM(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật danh không thành công");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        void LoadDateTimePicker2()
        {
            DateTime today = DateTime.Now;
            dateTimePicker1.Value = new DateTime(today.Year, today.Month, 1);
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1).AddDays(-1);
        }


        // private void fAdmin_Load(object sender, EventArgs e)
        // {
        //   LoadDateTimePicker2();
        //   uSP_rpBillTableAdapter.Fill(this.quanLyNhaHangDataSet.USP_rpBill, dateTimePicker1.Value, dateTimePicker2.Value);
        //    this.reportViewer1.RefreshReport();
        ///}
        
        private void fAdmin_Load(object sender, EventArgs e)
        {
            LoadDateTimePicker2();
            // report  cái dòng cmt
            //uSP_rpBillTableAdapter.Fill(this.quanLyNhaHangDataSet.USP_rpBill, dateTimePicker1.Value, dateTimePicker2.Value);
            //this.reportViewer1.RefreshReport();
        }

        // report 
        private void button1_Click(object sender, EventArgs e)
        {
            //uSP_rpBillTableAdapter.Fill(this.quanLyNhaHangDataSet.USP_rpBill, dateTimePicker1.Value, dateTimePicker2.Value);
           // this.reportViewer1.RefreshReport();
        }

        private void cbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nmFoodPrice_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion

        // khi txbFoodID thay doi -> (thuat toan) -> cbFoodCategory thay doi

    }
}
