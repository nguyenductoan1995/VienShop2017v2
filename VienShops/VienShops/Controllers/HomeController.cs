using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VienShops.Models;
// Khai báo thư viện phân trang
using PagedList;
using PagedList.Mvc;
namespace VienShops.Controllers
{
    public class HomeController : Controller
    {
        DBVienShopsDataContext Db = new DBVienShopsDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        // sản phẩm mới 
        public ActionResult NewProduct()
        {
            var newProduct = Db.SANPHAMs.OrderBy(n => n.MASP).Take(4).ToList();
            return PartialView(newProduct);
        }
        public ActionResult FeaturedProduct()
        {
            var featuredProduct = Db.SANPHAMs.OrderBy(n => n.CHATLIEU).Take(4).ToList();
            return PartialView(featuredProduct);
        }
        public ActionResult Products()
        {
            var products = Db.LOAISANPHAMs.ToList();
            return PartialView(products);
        }
        // Show products list
        public ActionResult ShowProductList(int? page, int id)
        {
            int pageSize = 9;
            // Nếu trang bé hơn 9 trả về 1 trang
            int pageNumber = (page ?? 1);
            //var tenLoai = Db.LOAISANPHAMs.SingleOrDefault(n => n.MALOAISP == id);
            var productList = Db.SANPHAMs.Where(n => n.MALOAISP == id).ToList().OrderBy(n => n.TENSP).ToPagedList(pageNumber, pageSize);
            // Gán mã, khi load form lưu id
            ViewBag.ID = id;
            return View(productList);
        }
        // Show products list giao diện 2
        public ActionResult ShowProductListTheme2(int? page,int id)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var productList = Db.SANPHAMs.Where(n => n.MALOAISP == id).ToList().OrderBy(n => n.TENSP).ToPagedList(pageNumber, pageSize);
            ViewBag.ID = id;
            return View(productList);
        }
        // Menu Trái của Loại sản phẩm 
        public PartialViewResult MenuLeft()
        {
            return PartialView();
        }
        // Liên hệ 
        public ActionResult Contact()
        {
            return View();
        }
        // Chi tiết sản phẩm 
        public ActionResult Detail(int id)
        {
            SANPHAM sp = Db.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            if(sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
			// Truyền vào model là sản phẩm
			return View(sp);
        }
		// Sản phẩm theo loại - Sản phẩm liên quan khi xem trang chi tiết - Sản phẩm theo loại
		//public ActionResult ProductType(string MaLoaiSP = 0)
		//{
		//   LOAISANPHAM sp = Db.LOAISANPHAMs.SingleOrDefault(n=>n.MALOAISP == MaLoaiSP)
		//}
		// -> Đang làm
		public ActionResult SIZE(int id)
		{
			var ct = Db.CHITIETSANPHAMs.Where(n => n.MASP == id).ToList();
			if (ct == null)
			{
				Response.StatusCode = 404;
				return null;
			}
			// Truyền vào model là sản phẩm
			return PartialView(ct);
		}
		public ActionResult extendProduct(int maloai,int masp)
		{
			var exProduct = Db.SANPHAMs.OrderBy(n => n.MALOAISP).Where(n=>n.MALOAISP==maloai  ).Where(n=>n.MASP!=masp).Take(3).ToList();
			return PartialView(exProduct);
		}
	}
}