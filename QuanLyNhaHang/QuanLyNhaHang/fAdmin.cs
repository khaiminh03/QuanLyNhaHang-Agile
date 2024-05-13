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
            
        }
        private void dgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
           
        }
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
           
        }

        private void btnEditFoof_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
           
        }

        
             
       
       
        
        

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
        }

        private void txbSearchFoodName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            
        }
        void AddAcount(string userName,string displayName, int type)
        {
            
        }
        void EditAcount(string userName, string displayName, int type)
        {
           

        }
        void DeleteAcount(string userName)
        {
            

        }
        void ResetPass(string userName)
        {
          
            
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
           
        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
           
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
           
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            
            
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
          

        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        void LoadDateTimePicker2()
        {
           
        }


      
        
        private void fAdmin_Load(object sender, EventArgs e)
        {
          
        }

        // report 
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void cbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nmFoodPrice_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }


        #endregion

        private void dgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
