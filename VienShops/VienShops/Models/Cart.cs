using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VienShops.Models
{
    public class Cart
    {
        DBVienShopsDataContext Db = new DBVienShopsDataContext();
        public int sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sUrlHinh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
		public string cChatLieu { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        // Create funtion 
        public Cart(int MaSP)
        {
            sMaSP = MaSP;
            SANPHAM sanpham = Db.SANPHAMs.Single(n => n.MASP == sMaSP);
            sTenSP = sanpham.TENSP;
            sUrlHinh = sanpham.URL;
            dDonGia = double.Parse(sanpham.GIA.ToString());
            iSoLuong =1 ;
			cChatLieu = sanpham.CHATLIEU;
		}
    }
}