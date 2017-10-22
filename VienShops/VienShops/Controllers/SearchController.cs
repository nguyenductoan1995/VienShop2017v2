using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VienShops.Models;
using PagedList.Mvc;
using PagedList;
namespace VienShops.Controllers
{
    public class SearchController : Controller
    {
        DBVienShopsDataContext Db = new DBVienShopsDataContext();
        // GET: Search
        [HttpPost]
        public ActionResult SearchResults(FormCollection f, int? page)
        {
            // Lấy dc giá trị mà chúng ta tìm kiếm
            string sKey = f["txtSearch"].ToString();
            ViewBag.KeyWord = sKey;
            List<SANPHAM> lstResults = Db.SANPHAMs.Where(n => n.TENSP.Contains(sKey)).ToList();
            // Phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            if (lstResults.Count == 0)
            {
                ViewBag.Notification = "Không tìm thấy sản phẩm nào. Vui lòng thử lại";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Notification = "Đã tìm thấy " + lstResults.Count() + " kết quả";
            // ViewBag.ID = id;
            return View(lstResults.OrderBy(n => n.TENSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult SearchResults(int? page, string sKeyWord)
        {
            // Lưu lại tiếp, ViewBag chỉ dc lưu 1 lần,reset lại mất
            ViewBag.KeyWord = sKeyWord;
            // Lấy dc giá trị mà chúng ta tìm kiếm
            List<SANPHAM> lstResults = Db.SANPHAMs.Where(n => n.TENSP.Contains(sKeyWord)).ToList();
            // Phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            if (lstResults.Count == 0)
            {
                ViewBag.Notification = "Không tìm thấy sản phẩm nào. Vui lòng thử lại";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Notification = "Đã tìm thấy " + lstResults.Count() + "kết quả";
            // ViewBag.ID = id;
            return View(lstResults.OrderBy(n => n.TENSP).ToPagedList(pageNumber, pageSize));
        }
    }
}