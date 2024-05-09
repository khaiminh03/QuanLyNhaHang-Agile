using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckout, int status, int discount = 0)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckout;
            this.Status = status;
            this.DisCount = discount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["thoiGianVao"];
            var dataCheckOutTemp = row["thoiGianRa"];
            if (dataCheckOutTemp.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)row["thoiGianRa"];
            }
            this.Status = (int)row["trangThai"];
            if (row["giamGia"].ToString() != "")
                this.DisCount = (int)row["giamGia"];
        }
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime? dateCheckIn; //ngay vao
        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        private DateTime? dateCheckOut; // ngay ra
        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private int discount;
        public int DisCount
        {
            get { return discount; }
            set { discount = value; }
        }
    }
}

