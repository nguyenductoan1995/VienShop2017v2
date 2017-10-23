using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Microsoft.AspNet.Identity;
using  VienShops.Models;

namespace VienShops.Controllers
{
    public class OrderController : Controller
    {
	    DBVienShopsDataContext Db = new DBVienShopsDataContext();
	    public List<Cart> LayGioHang()
	    {
		    List<Cart> lstGiohang = Session["Cart"] as List<Cart>;
		    if (lstGiohang == null)
		    {
			    lstGiohang = new List<Cart>();
			    Session["Cart"] = lstGiohang;
		    }
		    return lstGiohang;
	    }
	    private double TongTien()
	    {
		    double iTongTien = 0;
		    List<Cart> lstGiohang = Session["Cart"] as List<Cart>;
		    if (lstGiohang != null)
		    {
			    iTongTien = lstGiohang.Sum(n => n.dThanhTien);
		    }
		    return iTongTien;
	    }
		[Authorize]
		public ActionResult DatHang(FormCollection collection)
		{
			DONHANG ddh = new DONHANG();
			List<Cart> gh = LayGioHang();
			ddh.Id = User.Identity.GetUserId();
			ddh.NGAYMUAHANG = DateTime.Now;
			ddh.TONGTIEN = Decimal.Parse(TongTien().ToString());
			ddh.TINHTRANG = "1";
			Db.DONHANGs.InsertOnSubmit(ddh);
			Db.SubmitChanges();
			foreach (var item in gh)
			{
				CHITIETDONHANG ctdh = new CHITIETDONHANG();
				ctdh.MADH = ddh.MADH;
				ctdh.MASP = item.sMaSP;
				ctdh.SOLUONG = item.iSoLuong;
				ctdh.THANHTIEN = Decimal.Parse(item.dThanhTien.ToString());
				Db.CHITIETDONHANGs.InsertOnSubmit(ctdh);
			}
			Db.SubmitChanges();
			Session["Cart"] = null;
			return RedirectToAction("ConfirmOrder", "Order");

		}

		[HttpGet]
		[Authorize]
		public ActionResult Order()
		{
			var tt = Db.AspNetUsers.SingleOrDefault(n => n.Id == User.Identity.GetUserId());
			if (tt == null)
			{
				Response.StatusCode = 404;
				return null;
			}
			return View(tt);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Order(AspNetUser user)
		{
			//Còn nhìu nghi vấn 
			var tt = Db.AspNetUsers.SingleOrDefault(n => n.Id == User.Identity.GetUserId());
			var user1 = Db.AspNetUsers.SingleOrDefault(m=>m.Id ==tt.Id);
			if (user1 == null)
			{
				Response.StatusCode = 404;
				return null;
			}
			user1.FullName = user.FullName;
			user1.PhoneNumber = user.PhoneNumber;
			user1.Adress = user.Adress;
			Db.SubmitChanges();
			return Redirect("DatHang");
		}
		[Authorize]
		public PartialViewResult InfoCart()
		{
			List<Cart> gh = LayGioHang();
			ViewBag.TongTien = TongTien();
			return PartialView(gh);
		}
		[Authorize]
		public ActionResult ConfirmOrder()
		{
			return View();
		}
		[Authorize]
	    public ActionResult ManageOrder()
	    {
		    var manager = Db.DONHANGs.Where(m => m.Id == User.Identity.GetUserId()).OrderByDescending(m=>m.NGAYMUAHANG).ToList();
		    return View(manager);
	    }
	}
}