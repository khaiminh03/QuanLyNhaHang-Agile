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
            dgvFood.DataSource = foodList;
            LoadDateTimePicker();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            AddFoodBinding();
            LoadCategoryIntoCombobox(cbFoodCategory);

        }
        void AddDanhMucBinding()
        {
            
        }
        void AddTableBiding()
        {
           
        }
        void AddAccountBiding()
        {
            
        }
        void LoadDanhMuc()
        {
        }
        void LoadTable()
        {
           
        }
        void LoadAccount()
        {
           
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

            if (FoodDAO.Instance.InsertFood(name, categoryID, price) && (float)nmFoodPrice.Value > 0)
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
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đã có lỗi. Xóa món ăn không thành công!");
            }

        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
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
