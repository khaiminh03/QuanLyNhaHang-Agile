using QuanLyNhaHang.QuanLyNhaHangDataSetTableAdapters;
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
    public partial class hoadon : Form
    {
        int idhientai = 0;
        public hoadon(int id)
        {
            InitializeComponent();
            idhientai = id;
        }

        private void hoadon_Load(object sender, EventArgs e)
        {
            uSP_xuatHDTableAdapter.Fill(this.quanLyNhaHangDataSet.USP_xuatHD, idhientai);
            this.reportViewer1.RefreshReport();
        }
    }
}
