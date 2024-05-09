using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhaHang.DTO;


namespace QuanLyNhaHang.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
            set { MenuDAO.instance = value; }
        }
        public MenuDAO() { }

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            string query = "SELECT f.tenMonAn, bi.soLuongMon, f.giaMonAn, f.giaMonAn*bi.soLuongMon as thanhTien FROM dbo.ThongTinHoaDon as bi, dbo.HoaDon as b, dbo.MonAn as f where bi.idHoaDon = b.id and bi.idMonAn = f.id and b.trangThai = 0 and b.idBan =" + id;
            DataTable data = DataProvider.Instance.ExecuteSQL(query);


            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
